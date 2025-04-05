using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 
/// The Princess needs to be implemented back into to the game. Bailey needs to update the princess spine from 4.1.23 to 4.2.
/// Therefore PrincessUpdate that controls the Spine logic is removed for now.
/// 
/// </summary>

public class LevelManager : MonoBehaviour {

    private static LevelManager instance;
    public static LevelManager GetLevelManager() { return instance; }

    [Header("Componenets")]
    public PrincessController princess;
    public PrincessObject stats;
    //public PrincessUpdate update;
    public ScorBear scorBear;
    public WeaponFire weapon;

    [Header("UI")]
    public RectTransform marker;
    public Transform startPoint;
    public Transform scorebear;
    //public GameObject winImage;
    //public GameObject victoryMenu;
    public GameObject _marker;
    public GameObject gameOverMenu;
    public GameObject infoPanel;
    public GameObject powerUPHUD;
    public TextMeshProUGUI purse;
    public TextMeshProUGUI coinsCollected;
    public TextMeshProUGUI distanceCounter;
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI markerText;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI totalScore;

    [Header("Audio")]
    public Slider mainVolume;
    public string musicFile;
    [SerializeField] private float fileVolume;

    [Header("Scorbear Variables")]
    private float scorbearDistance;
    private float scorbearY;

    [Header("Player Level Stats")]
    public float distance;
    public int score;
    public int kills;

    [Header("End Game Conditions")]
    [SerializeField] public bool gameOver;
    [SerializeField] public bool victory;

    private void Awake() {
        instance = this;

        stats = Resources.Load<PrincessObject>("Princess");
        princess = PrincessController.GetPrincessController();
        //update = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessUpdate>();       
    }

    private void Start() {

        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
        scorBear = ScorBear.GetScorBear();
        gameOver = false;
        victory = false;
        startPoint.position = princess.transform.position;
    }

    private void Update() {
        CoinUpdate();
        DistanceUpdate();
        ScorebearDetector();
        ScoreUpdate();

        //&& Weapon.magazine == 0
        /*if (update.collision_ground && weapon.bulletsLeft == 0 && princess.rb.velocity.magnitude <= 5 && !gameOver) { //implement after Bailey has completed the princess.
            StartCoroutine(GameOver());
            gameOver = true;
            Debug.Log("game over" + gameOver);
        }*/
        if (gameOver) {
            StartCoroutine(GameOver());
            gameOver = false;
        }    
    }

    /* Victory State of the game
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(WinState());
        }
    } */

    public void CoinUpdate() {
        purse.text = $"{stats.coins}";
        coinsCollected.text = $"{stats.coins}";
        stats.coins = Mathf.RoundToInt(distance / 1000);
    }

    public void DistanceUpdate() {
        distance = princess.transform.position.x - startPoint.position.x;
        if (distance <= 1000) {
            distanceCounter.text = $"Distance {distance.ToString("F0")} m";
            distanceTraveled.text = $"{distance.ToString("F0")} m";
        } else {
            float kmDist = Mathf.Round(distance / 1000);
            distanceCounter.text = $"Distance {kmDist.ToString("F0")} km";
            distanceTraveled.text = $"{distance.ToString("F0")} m";
        }
    }

    public int GetDistance() {
        distance = princess.transform.position.x - startPoint.position.x;
        int totalDistance = Mathf.RoundToInt(distance);

        return totalDistance;
    }

    public void ScoreUpdate() {
        score = (Mathf.RoundToInt(distance) / 100) + kills;
        scoreCounter.text = $"Score {score}";
        totalScore.text = $"Score {score}";
    }

    public void ScorebearDetector() {
        scorbearDistance = princess.transform.position.x - ScorBear.GetScorBear().transform.position.x;
        scorbearY = ScorBear.GetScorBear().transform.position.y;
        marker.position = new Vector2(marker.position.x, scorbearY);
        markerText.text = $"{scorbearDistance.ToString("F0")} m";
    }

    public int CooldownManager(int time) {
        return time;
    }

    public IEnumerator GameOver() {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        Cursor.visible = true;
        princess.GetComponentInChildren<WeaponFire>().enabled = false;
        gameOverMenu.SetActive(true);
        _marker.SetActive(false);
        infoPanel.SetActive(false);
        powerUPHUD.SetActive(false);
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