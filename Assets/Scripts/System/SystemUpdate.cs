using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SystemUpdate : MonoBehaviour {

    public static SystemUpdate instance;

    public AchievementObject achievement;
    public CannonObject cannon;
    public PrincessObject princess;
    public UpgradeObject upgrade;
    public PlayerProgress progress;

    public GameObject loadingScreen;
    public Slider bar;
    public TextMeshProUGUI textField;
    public TextMeshProUGUI tipsText;
    public Sprite[] backgrounds;
    public Image backgroundImage;
    public string[] tips;

    private void Awake() {

        cannon = Resources.Load<CannonObject>("Cannon");
        princess = Resources.Load<PrincessObject>("Princess");
        upgrade = Resources.Load<UpgradeObject>("Princess Upgrades");

        SaveSystem.SavePlayer(this);
        SaveSystem.LoadPlayer();
    }

    public void LoadHome() {
        Loading("Menu");
        StartCoroutine(GenerateTip());
        Time.timeScale = 1f;
    }

    public void LoadLevel() {
        Loading("Game");
        StartCoroutine(GenerateTip());
        Time.timeScale = 1f;
    }
    
    #region Loading Screen
    public void Loading(string scene) {
        StartCoroutine(TotalProgress(scene));
    }

    public IEnumerator TotalProgress(System.String scene) {
        loadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone) {
            bar.value = async.progress / 0.9f;
            textField.text = string.Format("Loading Environments: {0}%", bar.value);
            yield return null;
        }
    }

    public int tipCount;
    public IEnumerator GenerateTip() {
        tipCount = Random.Range(0, tips.Length);
        tipsText.text = tips[tipCount];
        while (loadingScreen.activeInHierarchy) {
            yield return new WaitForSeconds(5f);

            tipCount++;
            if (tipCount >= tips.Length) {
                tipCount = 0;
            }
            tipsText.text = tips[tipCount];
        }
    }
    #endregion

    public void NewGame() { //This will start a new game for a player. Any existing data excepts crowns will be reset.
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Application.Quit();
    }

    #region SAVES
    public void SaveData() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadData() {
        PlayerProgress variables = SaveSystem.LoadPlayer();

        princess.coins = variables.coins;
        princess.Crowns = variables.crowns;
        princess.Distance = variables.distance;
        princess.Score = variables.score;
        princess.CurrentDress = variables.currentDress;
        princess.CurrentWeapon = variables.currentWeapon;
        princess.CurrentBandolier = variables.currentBandolier;

        achievement.bullets_fired = variables.bulletsFired;
        achievement.coins_earned = variables.coinsEarned;
        achievement.distance_traveled = variables.distanceTraveled;
        achievement.enemies_stunned = variables.enemiesStunned;
        achievement.jewels_earned = variables.JewelsEarned;

        cannon.cannonPower = variables.cannonPower;
        cannon.jumpForce = variables.jumpForce;
        cannon.nextCannonPower = variables.next_cannon_power;
        cannon.cannonLevel = variables.cannon_level;
        cannon.cannonCost = variables.cannon_cost;

        upgrade.armsSnuggling = variables.armsSnuggling;
        upgrade.artisanSlugs = variables.artisanSlugs;
        upgrade.shotPractice = variables.shotPractice;
        upgrade.reinforcedCorset = variables.reinforcedCorset;
        upgrade.betterGunpowder = variables.betterGunpowder;
        upgrade.lighterSilks = variables.lightSilks;
        upgrade.rubBloomers = variables.rubBloomers;
        upgrade.scorpionTraps = variables.scorpionTraps;
        upgrade.widerDress = variables.widerDress;
        upgrade.armsSnugglingCost = variables.armsSnugglingCost;
        upgrade.artisanRoundsCost = variables.artisanRoundsCost;
        upgrade.shotPracticeCost = variables.shotPracticeCost;
        upgrade.reinforcedCorsetCost = variables.reinforcedCorsetCost;
        upgrade.betterGunpowderCost = variables.betterGunpowderCost;
        upgrade.lighterSilksCost = variables.lightSilksCost;
        upgrade.rubbloomersCost = variables.rubbloomersCost;
        upgrade.scorpionTrapsCost = variables.scorpionTrapsCost;
        upgrade.widerDressCost = variables.widerDressCost;
        upgrade.armsSnugglingLvl = variables.armsSnugglingLvl;
        upgrade.artisanRoundsLvl = variables.artisanRoundsLvl;
        upgrade.shotPracticeLvl = variables.shotPracticeLvl;
        upgrade.reinforcedCorsetLvl = variables.reinforcedCorsetLvl;
        upgrade.betterGunpowderLvl = variables.betterGunpowderLvl;
        upgrade.lighterSilksLvl = variables.lightSilksLvl;
        upgrade.powderPackingLvl = variables.packedPowderLvl;
        upgrade.rubbloomersLvl = variables.rubbloomersLvl;
        upgrade.scorpionTrapsLvl = variables.scorpionTrapsLvl;
        upgrade.widerDressLvl = variables.widerDressLvl;
    }
    #endregion
}