using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

    private AchievementObject achieve;
    private PrincessObject princess;

    private Dictionary<AchievementSO, string> achievementList;
    private Dictionary<AchievementSO, int> achievementLevelList;

    private int achievementLevel;
    private int goalMet;
    private int maxLevel;
    private int rewardMultiplier;

    public GameObject prefab;

    private Image image;
    private TextMeshProUGUI text;

    private void Awake() {
        achievementLevelList = new Dictionary<AchievementSO, int>();

        AchievementListSO achievementTypeList = Resources.Load<AchievementListSO>(nameof(AchievementListSO));

        foreach (AchievementSO achievement in achievementTypeList.achievementList) {
            achievementLevelList[achievement] = 0;
        }
        TestLogAchievementLevel();
    }


    private void Start() {
        achievementList = new Dictionary<AchievementSO, string>();

        text.text = prefab.GetComponentInChildren<TextMeshProUGUI>().text;
        image = transform.Find("icon").GetComponent<Image>();
    }

    private void TestLogAchievementLevel() {
        foreach (AchievementSO achievementType in achievementLevelList.Keys) {
            Debug.Log(achievementType.achievementLevel + ": " + achievementLevelList[achievementType]);
        }
    }

    private void BulletFiredAchievement() {
        goalMet *= 5 + achievementLevel;
        maxLevel = 999;
        rewardMultiplier *= achievementLevel;
        if (achieve.bullets_fired >= goalMet && achieve.bullets_fired <= maxLevel) {
            achievementLevel++;
            princess.coins += (10 * rewardMultiplier);
            DisplayAchievement(achieve.bullets_fired, achievementLevel);
        }
    }

    public void DisplayAchievement(int achieveType, int achievementLevel) {
        /*  GameObject or prefab appears at the game over
            Achievement/(s) that unlocks is presented with their own achievement panel
            Each achievement has an individual max lvl and rewards.
        */

        prefab.SetActive(true);
        text.text = $"{achieveType}\n{achievementLevel}";
        //image.sprite = sprite; 
    }
}