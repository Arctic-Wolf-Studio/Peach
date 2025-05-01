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

    [SerializeField] private bool toCapture;

    private void Awake() {
        instance = this;

        //weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
    }

    private void Start() {
        cannon = Cannon.GetCannon();
        level = LevelManager.GetLevelManager();
        princess = PrincessController.GetPrincessController().transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, 10, 10000));

        if (cannon.cannonFire) Move();
        if (level.gameOver) CapturePrincess();
    }

    private void OnTriggerEnter2D(Collider2D other) {    

        if (other.CompareTag("Player")) {
            toCapture = true;
        }
    }

    private void Move() {
        if (princess.position.x > MaxDist) MoveTowardPrincess();
        if (toCapture) CapturePrincess();
    }

    private void CapturePrincess() {
        StartCoroutine(level.GameOver());
        princess.position = scorTail.position;
        princessRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        sprite.flipX = true;
        transform.position = Vector2.MoveTowards(transform.position, level.startPoint.position, Time.deltaTime * captureSpeed);
    }

    private void MoveTowardPrincess() {

        rb.velocity = new Vector2(1, 1) * speed;
        transform.position = Vector2.MoveTowards(transform.position, princess.position, 1) * Time.deltaTime;
        if (Vector2.Distance(transform.position, princess.transform.position) < 1) CapturePrincess();
    }
}