using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AchievementList")]
public class AchievementListSO : ScriptableObject {
    public List<AchievementSO> achievementList;
}