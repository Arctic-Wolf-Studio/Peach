using UnityEngine;

public class CameraController : MonoBehaviour {

    public PrincessController princess;
    public Transform target;

    [SerializeField] float minClamp, maxClamp;

    private Vector3 distance;

    private void Start() { 
        target = princess.transform;
        distance = transform.position - target.position;
    }

    private void LateUpdate() {
        CameraFollow();
    }

    private void CameraFollow() {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minClamp, maxClamp), transform.position.z);
    }
}