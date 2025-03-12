using System;
using UnityEngine;

public class PrincessController : MonoBehaviour {

    private static PrincessController instance;

    public static PrincessController GetPrincessController() { return instance; }

    public static Action EnterAirCollision;
    public static Action EnterGroundCollision;
    public static Action EnterIdleCollision;

    private PrincessObject stats;
    public PrincessUpdate update;

    public GameObject crosshair; 
    SpriteRenderer crossHairColor;

    [SerializeField] private LayerMask ground;
    [SerializeField] public Transform groundCheck;
    public Rigidbody2D rb;

    [SerializeField] float rotationSpeed;
    [SerializeField] float maxHeight;
    public float rotation;

    [HideInInspector] public Vector3 mousePosition, worldPosition, difference;

    private void OnEnable() {
        WeaponFire.shotMovement += WeaponMovement;
    }

    private void OnDisable() {
        WeaponFire.shotMovement -= WeaponMovement;
    }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        stats = Resources.Load<PrincessObject>("Princess");

        rb = GetComponent<Rigidbody2D>();
        update = GetComponent<PrincessUpdate>();
        crossHairColor = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    private void LateUpdate() {
        if (Cannon.GetCannon().cannonFire) {
            PrincessRotation();
        }          
    }

    private void FixedUpdate() {
        if (!Cannon.GetCannon().isActive) {
            SuperHeroSlam();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (OnGround()) {
            Debug.Log("hit the ground");
            PrincessUpdate.Instance.OnGroundCollision();
        } else if (collision.gameObject.CompareTag("Enemy")) {
            PrincessUpdate.Instance.OnAirCollision();
        } else {
            EnterIdleCollision?.Invoke();
        }
    }

    private bool OnGround() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    private void PrincessRotation() {

        mousePosition = UtilsClass.GetMouseWorldPosition();

        crosshair.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        Cursor.visible = false;
        crossHairColor.color = Color.red;

        difference = transform.position - crosshair.transform.position;
        difference.Normalize();

        rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float smoothRotate = Mathf.LerpAngle(transform.eulerAngles.z, rotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0, 0, smoothRotate);
    }

    public void WeaponMovement(float force, float altitude) {
        rb.AddForce(new Vector2(force, altitude), ForceMode2D.Impulse);
    }

    public void SuperHeroSlam() {
        if (transform.position.y > maxHeight) {
            rb.AddForce(new Vector2(0, -100f), ForceMode2D.Impulse);
        }
    }
}