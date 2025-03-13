using UnityEngine;

public class Cannon : MonoBehaviour {

    private static Cannon instance;

    public static Cannon GetCannon() { return instance; }

    [SerializeField] private LayerMask cannonInteraction;
    [SerializeField] private Rigidbody2D rb;
    public GameObject fireCannon;

    public Vector2 buttonBoxSize;

    public bool cannonFire, isActive;

    public float cannonPower, jumpForce;

    void Awake()
    {
        instance = this;
        
        fireCannon.SetActive(true);
        cannonFire = false;
        isActive = true;
    }

    private void Start() {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && !cannonFire && isActive && !IsUI()) {
            cannonFire = true;
        }
    }

    void FixedUpdate() {
        if (cannonFire && isActive) {
            FireCannon();
        }
    }

    public bool IsUI() {
        return Physics2D.OverlapBox(transform.position, buttonBoxSize, cannonInteraction);
    }

    public void FireCannon() {
        if (IsUI()) {
            rb.WakeUp();
            rb.gravityScale = 1;
            rb.AddForce(new Vector2(cannonPower, jumpForce), ForceMode2D.Impulse);

            cannonFire = true;
            isActive = false;
            fireCannon.SetActive(false);
            IsUI();
        } 
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(buttonBoxSize.x, buttonBoxSize.y, 0f));
    }
}