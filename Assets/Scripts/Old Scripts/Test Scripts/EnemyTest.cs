using System;
using System.Collections;
using UnityEngine;

public class EnemyTest : MonoBehaviour {
    public float armor, speed, latSpeed, princessOffset;
    public int spawnCounter;

    
    public SpriteRenderer sprite;
    public Rigidbody2D rb2D;
    public Transform princess;

    //public LevelUpdate lu;
    //public PrincessObject princessObject;
    //public PrincessUpdate princess;
    //public WeaponController weapon;
    public TestEnemyWeaponFire fire;
    private TestHealthManager testHealthManager;



    public bool active, isDead, isDown, isLeft, isMonkeyPath, isThunderOoze, isRight, isUp, isSplit;

    void Start() {
        
        princess = GameObject.FindGameObjectWithTag("Player").transform;
        testHealthManager = GetComponent<TestHealthManager>();
        fire = GetComponent<TestEnemyWeaponFire>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        //weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>();
        //lu = GameObject.FindGameObjectWithTag("LevelUpdate").GetComponent<LevelUpdate>();
        /*if (isThunderOoze) {
            if (health == 2 && spawnCounter < 1) {
                spawnCounter++;
                ThunderOoze();
            }
        }*/

        /*switch (gameObject.name) {
            case "Test Thunder Ooze":
                testHealthManager.OnDamaged += TestHealthManager_ThunderOoze_OnDamaged;
                testHealthManager.OnDeath += TestHealthManager_ThunderOoze_OnDeath;
                break;
            case "Test Thunder Ooze Medium":
                testHealthManager.OnDamaged += TestHealthManager_ThunderOoze_OnDamaged;
                testHealthManager.OnDeath += TestHealthManager_ThunderOoze_OnDeath;
                break;
            case "Test Thunder Ooze Small":
                testHealthManager.OnDamaged += TestHealthManager_ThunderOoze_OnDamaged;
                testHealthManager.OnDeath += TestHealthManager_ThunderOoze_OnDeath;
                break;
        }*/


        //testHealthManager.OnDamaged += TestHealthManager_OnDamaged;
        //testHealthManager.OnDeath += TestHealthManager_OnDeath;
    }

    void Update() {
        

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
    public void MonkeyPath() {
        if (!isDown && !isUp)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x - princessOffset, princess.transform.position.y), Time.fixedDeltaTime * (/*princess.princessRB.velocity.magnitude + */speed));

        if(isUp)
            //monkey.Translate(0, latSpeed * Time.fixedDeltaTime, 0);
            transform.Translate(Vector2.one * latSpeed * Time.fixedDeltaTime);

        if(isDown)
            //monkey.Translate(0, -latSpeed * Time.fixedDeltaTime, 0);
            transform.Translate(new Vector2(1, -1) * latSpeed * Time.fixedDeltaTime);

        if (active == false) {
            active = true;
            StartCoroutine(UpDownPath());
        }
    }

    public void ThunderOozePath() {
        if(!isLeft && !isRight)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x, princess.transform.position.y + princessOffset), speed * Time.fixedDeltaTime);

        if (isLeft)
            transform.Translate(Vector2.left * latSpeed * Time.fixedDeltaTime);

        if (isRight)
            transform.Translate(Vector2.right * latSpeed * Time.fixedDeltaTime);

        if (active == false) {
            active = true;
            StartCoroutine(LeftRightPath());
        }
    }

    public void FlipRotation() {
        if (transform.position.x > princess.transform.position.x) {
            //transform.rotation = Quaternion.Euler(transform.position.x, 0, 0);
            sprite.flipX = true;
            fire.isForward = true;
        } else {
            sprite.flipX = false;
            fire.isForward = false;
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

    private void TestHealthManager_OnDamaged(object sender, System.EventArgs e) {
       
    }

    private void TestHealthManager_OnDeath(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }
}