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

    public Transform crosshair; 
    SpriteRenderer crossHairColor;

    [SerializeField] private LayerMask ground;
    [SerializeField] public Transform groundCheck;
    public Rigidbody2D rb;

    [SerializeField] float maxHeight;
    float rotation;

    Vector3 mousePosition, difference;

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
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").transform;

        Cursor.visible = false;
    }

    private void Update() {
        if (!Cannon.GetCannon().isActive) {
            PrincessRotation();
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

        mousePosition = UtilsClass.GetMouseWorld2DPosition();

        crosshair.position = new Vector2(mousePosition.x, mousePosition.y);
        crossHairColor = crosshair.GetComponent<SpriteRenderer>();
        crossHairColor.color = Color.red;

        difference = transform.position - crosshair.position;
        difference.Normalize();

        rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
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