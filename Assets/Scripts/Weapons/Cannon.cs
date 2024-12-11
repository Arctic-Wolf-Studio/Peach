using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour {

    [SerializeField] private LayerMask cannonInteraction;
    [SerializeField] private Rigidbody2D princessRB;
    public GameObject fireCannon;

    public Vector2 buttonBoxSize;

    public bool cannonFire, isActive;

    public float cannonPower, jumpForce;

    // Start is called before the first frame update
    void Awake()
    {
        
        fireCannon.SetActive(true);
        cannonFire = false;
        isActive = true;
    }

    private void Start() {
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        princessRB.gravityScale = 0;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !cannonFire && isActive && !IsUI()) {
            cannonFire = true;
        }
    }


    void FixedUpdate() {
        if (cannonFire) {
            FireCannon();
        }
    }

    private bool IsUI() {
        return Physics2D.OverlapBox(transform.position, buttonBoxSize, cannonInteraction);
    }

    public void FireCannon() {
        if (IsUI()) {
            princessRB.WakeUp();
            princessRB.gravityScale = 1;
            princessRB.AddForce(new Vector2(cannonPower, jumpForce), ForceMode2D.Impulse);

            cannonFire = false;
            isActive = false;
            fireCannon.SetActive(false);
        } 
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(buttonBoxSize.x, buttonBoxSize.y, 0f));
    }
}
