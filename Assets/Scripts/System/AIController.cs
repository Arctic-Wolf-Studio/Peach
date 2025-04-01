using UnityEngine;
using TMPro;

public class AIController : MonoBehaviour {
    
    private static AIController instance;

    public static AIController GetAIController() { return instance; }
    
    //Enemy GameObjects
    [Header("Enemies")]
    public GameObject flyEye;
    public GameObject flyingMonkey;
    public GameObject thunderOoze;

    //Power-Up/Ally GameObjects
    [Header("Power Ups")]
    public GameObject bird;
    public GameObject cat;
    public GameObject chameleon;
    public GameObject monkey;
    public GameObject squirrel;
    public GameObject turtle;

    private Monkey mon;

    public float cooldownTime = 10;
    public float nextActivation;

    public Transform flyEyeSpawn;
    public Transform flyingMonkeySpawn;
    public Transform thunderOozeSpawn;
    public TextMeshProUGUI cooldownText1;
    public TextMeshProUGUI cooldownText2;
    public TextMeshProUGUI cooldownText3;
    public TextMeshProUGUI cooldownText4;
    public TextMeshProUGUI cooldownText5;
    public TextMeshProUGUI cooldownText6;
    public PrincessController princess;
    public PrincessUpdate update;
    public Bird singing;

    [Header("Max Enemy Types")]
    public int maximumEnemies;
    public int maxFlyEyes, flyEyeCounter;
    public int maxFlyingMonkeys, flyingMonkeyCounter;
    public int maxThunderOozes, thunderOozeCounter;

    [Header("Enemy Cooldowns")]
    public float cooldownleft;
    public float flyEyeCooldown;
    public float monkeyCooldown;
    public float thunderoozeCooldown;

    public float scaleX;
    public float scaleY;

    public float cooldownBird;
    public float cooldownCat;
    public float cooldownChameleon;
    public float cooldownMonkey;
    public float cooldownSquirrel;
    public float cooldownTurtle;

    public int counter;
    public int maxCounter;
    private float timer;
    private Vector3 princessOffSet;
    private Vector2 spawn;

    private Cannon cannon;
    private LevelManager level;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Cannon>();
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessController>();
        update = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessUpdate>();
        princessOffSet = flyEyeSpawn.position - princess.transform.position;

        timer = 0f;
    }

    private void Update() {
        MoveToPrincess();
        if (counter < maximumEnemies) {
            //SpawnFlyEye();
            //SpawnFlyingMonkey();
            //SpawnThunderOoze();
        }
        //Presentation, Remove after
/*        if (Input.GetKeyDown(KeyCode.Alpha5) == true)
            BirdSpawn();

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
            CatSpawn();

        if (Input.GetKeyDown(KeyCode.Alpha6) == true)
            ChameleonSpawn();

        if (Input.GetKeyDown(KeyCode.Alpha4) == true)
            MonkeySpawn();

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
            SquirrelSpawn();

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
            TurtleSpawn();

        *//*Instantiate(flyEye, flyEyeSpawn.position, Quaternion.identity);*/
    }


    private void MoveToPrincess() {
        flyEyeSpawn.position = new Vector3(princess.transform.position.x + princessOffSet.x, Mathf.Clamp(flyEyeSpawn.position.y, scaleY, 100), flyEyeSpawn.position.z);
    }

    private void SpawnFlyEye() {
        if (cannon.cannonFire && Time.time > timer && !LevelManager.GetLevelManager().gameOver && flyEyeCounter < maxFlyEyes) {
            spawn = new Vector2(Random.Range(flyEyeSpawn.position.x - scaleX, flyEyeSpawn.position.x + scaleX), Random.Range(flyEyeSpawn.position.y - scaleY, flyEyeSpawn.position.y + scaleY));
            Instantiate(flyEye, spawn, Quaternion.identity);

            flyEyeCounter++;
            counter++;
            timer = Time.time + flyEyeCooldown;
        }
    }

    private void SpawnFlyingMonkey() {
        if (cannon.cannonFire && Time.time > timer && !LevelManager.GetLevelManager().gameOver && update.isIdle && flyingMonkeyCounter <= maxFlyingMonkeys) {
            Instantiate(flyingMonkey, flyingMonkeySpawn.position, Quaternion.identity);
            flyingMonkeyCounter++;
            counter++;
            timer = Time.time + monkeyCooldown;
        }
    }

    private void SpawnThunderOoze() {
        if (cannon.cannonFire && Time.time > timer && !LevelManager.GetLevelManager().gameOver && level.GetDistance() >= 500 && thunderOozeCounter <= maxThunderOozes) {
            Instantiate(thunderOoze, thunderOozeSpawn.position, Quaternion.identity);
            thunderOozeCounter++;
            counter++;
            timer = Time.time + thunderoozeCooldown;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(flyEyeSpawn.position, flyEyeSpawn.localScale * 2);
    }


    public float Cooldown(float condition) {
        if (Time.time > condition + nextActivation) {
            print("ability used, cooldown started");
            nextActivation = Time.time + cooldownTime;
        }

        return condition;
    }

    private void CooldownSketchySystem() {
        cooldownleft = Mathf.Clamp(nextActivation - Time.time, 0, 30);
        cooldownText1.text = cooldownleft.ToString("00");
        cooldownText2.text = cooldownleft.ToString("00");
        cooldownText3.text = cooldownleft.ToString("00");
        cooldownText4.text = cooldownleft.ToString("00");
        cooldownText5.text = cooldownleft.ToString("00");
        cooldownText6.text = cooldownleft.ToString("00");

        if (Time.time > nextActivation) {
            cooldownText1.enabled = false;
            cooldownText2.enabled = false;
            cooldownText3.enabled = false;
            cooldownText4.enabled = false;
            cooldownText5.enabled = false;
            cooldownText6.enabled = false;
        } else {
            cooldownText1.enabled = true;
            cooldownText2.enabled = true;
            cooldownText3.enabled = true;
            cooldownText4.enabled = true;
            cooldownText5.enabled = true;
            cooldownText6.enabled = true;
        }
    }

    //Power Up Spawning
    public void BirdSpawn() {
        if (Time.time > nextActivation) {
            Instantiate(bird, princess.transform.position + bird.transform.position, Quaternion.identity);
            nextActivation = Time.time + cooldownBird;
        }
    }

    public void CatSpawn() {
        if (Time.time > nextActivation) {
            Instantiate(cat, princess.transform.position + cat.transform.position, Quaternion.identity);
            nextActivation = Time.time + cooldownCat;
        }
    }

    public void ChameleonSpawn() {
        if (Time.time > nextActivation) {
            Instantiate(chameleon, princess.transform.position + chameleon.transform.position, Quaternion.identity);
            nextActivation = Time.time + cooldownChameleon;
        }
    }

    public void MonkeySpawn() {
        if (Time.time > nextActivation) {
            Instantiate(monkey, princess.transform.position + monkey.transform.position, Quaternion.identity);
            nextActivation = Time.time + cooldownMonkey;
        }
    }

    public void SquirrelSpawn() {
        if (Time.time > nextActivation) {
            Instantiate(squirrel, princess.transform.position + squirrel.transform.position, Quaternion.identity);
            nextActivation = Time.time + cooldownSquirrel;
        }
    }

    public void TurtleSpawn() {
        if (Time.time > nextActivation) {
            Instantiate(turtle, princess.transform.position + turtle.transform.position, Quaternion.identity);
            nextActivation = Time.time + cooldownTurtle;
        }
    }
}