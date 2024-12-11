using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] private Transform floor;
    [SerializeField] private float nextPosition;
    private Vector3 target;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) { 
            target = new Vector3(floor.position.x + nextPosition, floor.position.y, floor.position.z);

            floor.position = target;
        }
    }
}