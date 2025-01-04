using UnityEngine;

public class ScorBear : MonoBehaviour {
    private Cannon cannon;
    private AIController Controller;
    public LevelManager level;
    public WeaponFire weapon;

    public float speed = 50;
    private Transform princess;
    public Transform scorTail;
    private Rigidbody2D scoreRB;
    private Rigidbody2D princessRB;
    private SpriteRenderer sprite;
    public int MaxDist = 50;
    public int knockback =2;
    public int captureSpeed = 10;

    private void Start() {
        cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Cannon>();
        Controller = GameObject.FindGameObjectWithTag("Spawn").GetComponent<AIController>();
        princess = GameObject.FindGameObjectWithTag("Player").transform;
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        scoreRB = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
    }

    private void Update() {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, 10, 10000));
    }

    private void FixedUpdate() {
        CapturePrincess();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Projectile") == true)
        {
            transform.position = new Vector2(transform.position.x - knockback, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") == true) {
            weapon.bulletsLeft = 0;
        }
    }

    private void CapturePrincess() {   
        if (cannon.cannonFire && princess.position.x > MaxDist && LevelManager.gameOver == false) {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x - 4, princess.transform.position.y + 1), Time.deltaTime * speed);    
        }
        transform.right = Controller.princess.transform.position - transform.position;
        if(LevelManager.gameOver == true && princess.transform.position.x <= MaxDist) {
            sprite.flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, level.startPoint.position, Time.deltaTime * captureSpeed);
            princess.transform.position = Vector2.MoveTowards(princess.transform.position, scorTail.position, Time.deltaTime * captureSpeed);
            princessRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}