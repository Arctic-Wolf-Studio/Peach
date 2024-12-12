using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour {

    [Header("Componenets")]
    public PrincessController p_controller;
    public PrincessStats stats;
    public PrincessUpdate update;
    public WeaponFire Weapon;

    public RectTransform marker;
    public Transform startPoint;
    public Transform scorebear;
    //public GameObject winImage;
    //public GameObject victoryMenu;
    public GameObject _marker;
    public GameObject defeatImage;
    public GameObject gameOverMenu;
    public GameObject infoPanel;
    public GameObject powerUPHUD;
    public Text purse;
    public Text coinsCollected;
    public Text distanceCounter;
    public Text distanceTraveled;
    public TextMeshProUGUI markerText;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI totalScore;

    public Slider mainVolume;
    public string musicFile;
    [SerializeField] private float fileVolume;

    

    private float scorbearDistance;
    private float scorbearY;
    public float distance;
    public int score;
    public int kills;

    public static bool gameOver;
    public static bool victory;

    private void Start() {
        p_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessController>();
        Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
        gameOver = false;
        victory = false;
        startPoint.position = p_controller.transform.position;
    }

    private void Update() {
        CoinUpdate();
        DistanceUpdate();
        ScorebearDetector();
        ScoreUpdate();

        //&& Weapon.magazine == 0
        if (update.collision_ground == true && Weapon.bulletsLeft == 0 && p_controller.rb.velocity.magnitude <= 5 && gameOver == false) {
            StartCoroutine(GameOver());
            gameOver = true;
            Debug.Log("game over" + gameOver);
        }

        
    }

    /* Victory State of the game
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(WinState());
        }
    } */

    public void CoinUpdate() {
        purse.text = $"{stats.Coins}";
        coinsCollected.text = $"{stats.Coins}";
        stats.Coins = Mathf.RoundToInt(distance / 1000);
    }

    public void DistanceUpdate() {
        distance = update.transform.position.x - startPoint.position.x;
        if (distance <= 1000) {
            distanceCounter.text = "Distance " + distance.ToString("F0") + " m";
            distanceTraveled.text = distance.ToString("F0") + " m";
        } else {
            float kmDist = Mathf.Round(distance / 1000);
            distanceCounter.text = "Distance " + kmDist.ToString("F0") + " km";
            distanceTraveled.text = distance.ToString("F0") + " m";
        }
    }

    public int GetDistance() {
        distance = update.transform.position.x - startPoint.position.x;
        int totalDistance = Mathf.RoundToInt(distance);

        return totalDistance;
    }

    public void ScoreUpdate() {
        score = (Mathf.RoundToInt(distance) / 100) + kills;
        scoreCounter.text = "Score " + score;
        totalScore.text = "Score " + score;
    }

    public void ScorebearDetector() {
        scorbearDistance = update.transform.position.x - scorebear.position.x;
        scorbearY = scorebear.position.y;
        marker.position = new Vector2(marker.position.x, scorbearY);
        markerText.text = "" + scorbearDistance.ToString("F0") + " m";
    }

    public int CooldownManager(int time) {
        return time;
    }

    public IEnumerator GameOver() {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        defeatImage.SetActive(true);
        gameOverMenu.SetActive(true);
        _marker.SetActive(false);
        infoPanel.SetActive(false);
        powerUPHUD.SetActive(false);
        Cursor.visible = true;

    }

    /* Victory state of the game
    private IEnumerator WinState() {
        victory = true;

        winImage.SetActive(true);
        victoryMenu.SetActive(true);
        Cursor.visible = true;
        //WinSound.Sound.Play(); Replace with FMOD

        yield return new WaitForSeconds(20);
        SceneManager.LoadSceneAsync("Game");
    } */
}