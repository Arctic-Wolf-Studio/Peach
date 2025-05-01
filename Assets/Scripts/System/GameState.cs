using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public LevelManager level;
    public static bool GameIsPaused = false;
    public GameObject Pause_Menu;
    public GameObject powerUps;
    public Text purse;
    public Text distanceCounter;

    private void Awake()
    {
        level = LevelManager.GetLevelManager();
        GameIsPaused = false;
        Time.timeScale = 1f;
        DontDestroyOnLoad(level);
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
        distanceCounter.text = $"Distance {level.distance.ToString("F2")}m";
        purse.text = $"{SystemUpdate.instance.princess.coins}";
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
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }
}