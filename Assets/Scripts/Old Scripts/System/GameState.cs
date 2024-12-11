using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public LevelManager level;
    private WeaponFire weapon;
    public PrincessObject princess;
    public static bool GameIsPaused = false;
    public GameObject Pause_Menu;
    public GameObject powerUps;
    public Text purse;
    public Text distanceCounter;

    private void Awake()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        //DontDestroyOnLoad(lu);
    }

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void RetryLevel() {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void Pause() {

        Pause_Menu.SetActive(true);
        powerUps.SetActive(false);
        Time.timeScale = 0f;       
        GameIsPaused = true;
        Cursor.visible = true;
        distanceCounter.text = "Distance: " + level.distance.ToString("F2") + " m";
        purse.text = "" + princess.coins;
    }

    public void Resume() {

        powerUps.SetActive(true);
        Pause_Menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    public void Quit() {
        SceneManager.LoadScene("Menu");
    }
}