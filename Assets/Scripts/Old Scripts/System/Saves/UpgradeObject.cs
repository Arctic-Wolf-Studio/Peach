using UnityEngine;
[System.Serializable]

[CreateAssetMenu(menuName = "Upgrades", fileName = "New Upgrade Save Data")]
public class UpgradeObject : ScriptableObject {

    public float armsSnuggling;
    public int armsSnugglingCost;
    public int armsSnugglingLvl;

    public float artisanSlugs;
    public int artisanRoundsCost;
    public int artisanRoundsLvl;

    public float betterGunpowder;
    public int betterGunpowderCost;
    public int betterGunpowderLvl;

    public float lighterSilks;
    public int lighterSilksCost;
    public int lighterSilksLvl;

    public float powderPacking;
    public int powderPackingCost;
    public int powderPackingLvl;

    public float reinforcedCorset;
    public float reinforcedCorsetPower;
    public int reinforcedCorsetCost;
    public int reinforcedCorsetLvl;

    public float rubBloomers;
    public int rubbloomersCost;
    public int rubbloomersLvl;

    public float scorpionTraps;
    public int scorpionTrapsCost;
    public int scorpionTrapsLvl;

    public float shotPractice;
    public int shotPracticeCost;
    public int shotPracticeLvl;

    public float widerDress;
    public int widerDressCost;
    public int widerDressLvl;
}