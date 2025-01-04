using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {

    private AchievementListSO achievementTypeList;
    private Dictionary<AchievementSO, Transform> transformAchievementDictionary;

    private void Awake() {
        achievementTypeList = Resources.Load<AchievementListSO>(nameof(AchievementListSO));

        transformAchievementDictionary = new Dictionary<AchievementSO, Transform>();

        Transform achievementUI = transform.Find("achievementUI");
        achievementUI.gameObject.SetActive(false);

        int index = 0;
        foreach (AchievementSO achievement in achievementTypeList.achievementList) {
            Transform achievementTransform = Instantiate(achievementUI, transform);
            achievementUI.gameObject.SetActive(true);

            float offsetAmount = -200f;
            achievementTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetAmount * index);

            achievementTransform.Find("icon").GetComponent<Image>().sprite = achievement.sprite;

            transformAchievementDictionary[achievement] = achievementTransform;

            index++;
        }
        
    }
}
