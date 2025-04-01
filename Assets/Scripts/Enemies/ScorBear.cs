using UnityEngine;

public class ScorBear : MonoBehaviour {
    
    private static ScorBear instance;

    public static ScorBear GetScorBear() { return instance; }
    
    private Cannon cannon;
    public LevelManager level;

    public float speed = 50;
    private Transform princess;
    public Transform scorTail;
    private Rigidbody2D rb;
    private Rigidbody2D princessRB;
    private SpriteRenderer sprite;
    public int MaxDist = 50;
    public int knockback = 2;
    public int captureSpeed = 10;

    [SerializeField] private bool toCapture, toMove;

    private void Awake() {
        instance = this;

        //weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
    }

    private void Start() {
        cannon = Cannon.GetCannon();
        level = LevelManager.GetLevelManager();
        princess = PrincessController.GetPrincessController().transform;
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        toMove = true;
    }

    private void Update() {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, 10, 10000));

        if (toMove) MoveTowardPrincess();
        if (toCapture) CapturePrincess();
    }

    private void OnTriggerEnter2D(Collider2D other) {    

        if (other.CompareTag("Player")) {
            toMove = false;
            toCapture = true;
        }
    }

    private void CapturePrincess() {
        StartCoroutine(level.GameOver());
        princess.position = scorTail.position;
        princessRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        princess.GetComponent<CapsuleCollider2D>().enabled = false;
        sprite.flipX = true;
        transform.position = Vector2.MoveTowards(transform.position, level.startPoint.position, Time.deltaTime * captureSpeed);
    }

    private void MoveTowardPrincess() {
        if (cannon.cannonFire && princess.position.x > MaxDist && !level.gameOver) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.position.x - 4, princess.position.y + 1), Time.deltaTime * speed);
        }
        transform.right = princess.position - transform.position;
    }
}