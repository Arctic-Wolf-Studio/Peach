using System;
using UnityEngine;

public class PrincessController : MonoBehaviour {

    private static PrincessController instance;

    public static PrincessController GetPrincessController() { return instance; }

    public static Action EnterAirCollision;
    public static Action EnterGroundCollision;
    public static Action EnterIdleCollision;

    public PrincessStats stats;
    public PrincessUpdate update;

    [SerializeField] private LayerMask ground;
    [SerializeField] public Transform groundCheck;
    public Rigidbody2D rb;

    [SerializeField] float maxHeight;

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
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PrincessStats>();
        update = GetComponent<PrincessUpdate>();
    }

    private void Update() {
        SuperHeroSlam();
            
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("PrincessController: enter col");
        //EnterGroundCollision?.Invoke();
        if (OnGround()) {
            Debug.Log(OnGround());
            //EnterGroundCollision?.Invoke();
            PrincessUpdate.Instance.OnGroundCollision();
        } else if (collision.gameObject.CompareTag("Enemy")) {
            EnterAirCollision?.Invoke();
        } else {
            EnterIdleCollision?.Invoke();
        }
    }

    private bool OnGround() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
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