using UnityEngine;

public class CameraController : MonoBehaviour {

    public PrincessController princess;

    [SerializeField] float minClamp, maxClamp;

    private Vector3 distance;

    private void Start() {
        princess = PrincessController.GetPrincessController();
        distance = transform.position - princess.transform.position;
    }

    private void LateUpdate() {
        CameraFollow();
    }

    private void CameraFollow() {
        transform.position = princess.transform.position + distance;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minClamp, maxClamp), transform.position.z);
    }
}