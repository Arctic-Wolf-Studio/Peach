using UnityEngine;

public class FlyEye_OLD : MonoBehaviour {
    private AIController Controller;
    private WeaponFire Weapon;
    private LevelManager levelManager;
    //public UpgradeObject upgrade;

    private PrincessController p_controller;
    private PrincessUpdate p_update;

    [HideInInspector] public Rigidbody2D flyEyeRB;
    //public Animator flapping;

    public float speed;
    public float acceleration;
    public bool isDead;
    public bool isStunned;
    public bool isGrabbed;
    public bool isDetected;

    public float distance;

    private void Start() {
        flyEyeRB = GetComponent<Rigidbody2D>();

        Controller = GameObject.FindGameObjectWithTag("Spawn").GetComponent<AIController>();
        Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        p_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessController>();
        p_update = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessUpdate>();
        //flapping = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        FollowPrincess();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Projectile") == true) {
            flyEyeRB.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            

            if (isDead != true) {
                levelManager.kills++;
                //princess.coins += 2;
                Weapon.bulletsLeft++;
                isDead = true;
                Controller.counter--;
                //Debug.Log("coins updated " + princess.coins);
            }
        }

        if (collision.gameObject.CompareTag("Bird") == true) {
            isStunned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bird") == true)
            isStunned = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Controller.princess.GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);

            Controller.counter--;
            Destroy(gameObject);
        }
    }

    private void FollowPrincess() {
        flyEyeRB.velocity = Vector2.ClampMagnitude(flyEyeRB.velocity, 100);
        distance = Controller.princess.transform.position.x - transform.position.x;

        //Fly Eye will always follow the Princess no matter where she is until she starts losing momentum and has no ammo left.
        if (LevelManager.victory == false && LevelManager.gameOver == false && isDead != true) {
            if (isStunned == false && isGrabbed == false) {
                if (distance > 45) {
                    transform.Translate(acceleration * Time.deltaTime * Vector2.right);
                    //Ask if we want gravity or flapping effects on the Fly Eyes
                    acceleration += 0.1f;
                } else {
                    isDetected = true;
                    if (!p_update.collisionAir || !p_update.collisionGround)
                        flyEyeRB.position = Vector2.MoveTowards(flyEyeRB.position, Controller.princess.transform.position, Time.deltaTime * (p_controller.rb.velocity.magnitude + speed));
                    else
                        flyEyeRB.position = Vector2.MoveTowards(flyEyeRB.position, Controller.princess.transform.position, Time.deltaTime * p_controller.rb.velocity.magnitude);

                    if (p_controller.rb.velocity.magnitude == 0)
                        flyEyeRB.velocity = Vector2.zero;
                }

                if (isDetected)
                    transform.right = Controller.princess.transform.position - transform.position;
            } else {
                flyEyeRB.velocity = Vector2.zero;
                flyEyeRB.position = Vector2.MoveTowards(flyEyeRB.position, Controller.princess.transform.position, Time.deltaTime * p_controller.rb.velocity.magnitude);
            }
        } else if (isDead) {
            //Sprite change to dead
            //flapping.enabled = false;
            flyEyeRB.IsAwake();
            flyEyeRB.gravityScale = 1;
            flyEyeRB.freezeRotation = false;
            Destroy(gameObject, 5);
        }
    }
}