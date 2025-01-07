using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    public LevelManager level;
    public PrincessObject princessObject;
    public PrincessController princess;
    public WeaponFire fire;
    public EnemyWeaponFire enemyFire;

    public float acceleration, armor, speed, latSpeed, princessOffset;
    public int spawnCounter;

    public SpriteRenderer sprite;
    public Rigidbody2D rb2D;
    public Transform princessTransform;  

    public bool active, isDead, isDown, isLeft, isMonkeyPath, isThunderOoze, isRight, isUp, isSplit;

    void Start() {
        
        enemyFire = GetComponent<EnemyWeaponFire>();
        fire = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessController>();
        princessTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponentInChildren<SpriteRenderer>();


        //testHealthManager.OnDamaged += TestHealthManager_OnDamaged;
        //testHealthManager.OnDeath += TestHealthManager_OnDeath;
    }

    private void OnEnable() {
        
    }

    private void OnDisable() {
        
    }

    void FixedUpdate() {
        if (isMonkeyPath) {
            MonkeyPath();
            //FlipRotation();
        }

        if (isThunderOoze) {
            ThunderOozePath();
            FlipRotation();
        }
    }

    private bool ShouldAccelerate(float offset) {
       float distance = princessTransform.position.x - transform.position.x;

        if (distance <= princessTransform.position.x - offset) {
            return false;
        } else {
            return true;
        }
    }

    public void MonkeyPath() {

        //Transform movementType = GameObject.FindGameObjectWithTag("Monkey Body").transform;

        if (ShouldAccelerate(40f)) {
            transform.Translate(acceleration * Time.deltaTime * Vector2.right);
        }
        if (!isDown && !isUp)
            
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x - princessOffset, princess.transform.position.y), Time.fixedDeltaTime * (princess.rb.velocity.magnitude * speed));

        if (isUp)
            //monkey.Translate(0, latSpeed * Time.fixedDeltaTime, 0);
            transform.Translate(Vector2.one * latSpeed * Time.fixedDeltaTime);

        if (isDown)
            //monkey.Translate(0, -latSpeed * Time.fixedDeltaTime, 0);
            transform.Translate(new Vector2(1, -1) * latSpeed * Time.fixedDeltaTime);


        if (active == false) {
            active = true;
            StartCoroutine(UpDownPath());
        }

        if (isDead) {
            Destroy(gameObject, 3);
        }
    }

    public void ThunderOozePath() {

        if (ShouldAccelerate(40f)) {
            transform.Translate(acceleration * Time.deltaTime * Vector2.right);
        }

        if (!isLeft && !isRight)
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector2(princess.transform.position.x, princess.transform.position.y + princessOffset), 
                Time.fixedDeltaTime * (princess.rb.velocity.magnitude * speed));

        if (isLeft)
            transform.Translate(Vector2.left * (princess.rb.velocity.magnitude * latSpeed) * Time.fixedDeltaTime);

        if (isRight)
            transform.Translate(Vector2.right * (princess.rb.velocity.magnitude * latSpeed) * Time.fixedDeltaTime);

        if (active == false) {
            active = true;
            StartCoroutine(LeftRightPath());
        }

        if (isDead) {
            Destroy(gameObject, 3);
        }
    }

    public void FlipRotation() {
        if (transform.position.x > princess.transform.position.x) {
            //transform.rotation = Quaternion.Euler(transform.position.x, 0, 0);
            sprite.flipX = true;
            enemyFire.isForward = true;
        } else {
            sprite.flipX = false;
            enemyFire.isForward = false;
        }
    }

    public IEnumerator LeftRightPath() {

        yield return new WaitForSeconds(5);
        isLeft = true;
        yield return new WaitForSeconds(3);
        isLeft = false;
        isRight = true;
        yield return new WaitForSeconds(3);
        isRight = false;
        active = false;
    }

    public IEnumerator UpDownPath() {

        yield return new WaitForSeconds(5);
        isUp = true;
        yield return new WaitForSeconds(3);
        isUp = false;
        isDown = true;
        yield return new WaitForSeconds(3);
        isDown = false;
        active = false;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            if (!isDead) {
                fire.bulletsLeft += 2;
                level.kills++;
                level.stats.coins += 2;
                level.score += 10;
                //princess.FlyAndShoot();
                isDead = true;
            }
        }
    }
    /*private void Event_OnDamaged(object sender, System.EventArgs e) {

    }

    private void Event_OnDeath(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }*/
}