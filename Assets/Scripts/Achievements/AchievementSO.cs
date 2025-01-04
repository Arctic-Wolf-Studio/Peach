using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "ScriptableObject/Achievement")]
public class AchievementSO : ScriptableObject {

    public string achievementName;
    public int achievementLevel;
    public int achievementType;
    public string achievementDescribtion;

    public Sprite sprite;
}