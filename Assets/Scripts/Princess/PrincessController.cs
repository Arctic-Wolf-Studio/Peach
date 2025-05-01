using System;
using UnityEngine;

public class PrincessController : MonoBehaviour {

    private static PrincessController instance;
    public static PrincessController GetPrincessController() { return instance; }

    public static Action<bool> EnterAirCollision;
    public static Action<bool> EnterGroundCollision;
    public static Action<bool> EnterIdleCollision;

    public static Action OnCollision;

    [SerializeField] private PrincessObject stats;
    [SerializeField] public PrincessUpdate update;
    [SerializeField] private WeaponFire weapon; 

    public GameObject crosshair; 
    SpriteRenderer crossHairColor;
    [SerializeField] Transform weaponPivot;

    [SerializeField] private LayerMask ground;
    [SerializeField] public Transform groundCheck;
    public Rigidbody2D rb;

    [SerializeField] float rotationSpeed;
    [SerializeField] float maxHeight;
    public float rotation;

    [HideInInspector] public Vector3 mousePosition, difference;

    [SerializeField] private bool testMovement;

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
        weapon = GetComponentInChildren<WeaponFire>();
        weaponPivot = weapon.weaponPivotPoint;
        Cursor.visible = false;
    }

    private void Update() {

        if (Cannon.GetCannon().cannonFire && testMovement) {
            transform.position += Vector3.right;
            rb.gravityScale = 0f;
        }
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
            EnterGroundCollision?.Invoke(update.collisionGround);
            OnCollision?.Invoke();
        } else if (collision.gameObject.CompareTag("Enemy")) {
            EnterAirCollision?.Invoke(update.collisionAir);
        } else {
            EnterIdleCollision?.Invoke(true);
        }
    }

    public bool OnGround() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    private void PrincessRotation() {

        mousePosition = UtilsClass.GetMouseWorldPosition();

        crosshair.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        Cursor.visible = false;
        crossHairColor.color = Color.red;

        difference = weaponPivot.position - crosshair.transform.position;
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