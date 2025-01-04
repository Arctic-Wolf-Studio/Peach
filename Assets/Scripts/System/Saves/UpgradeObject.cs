using UnityEngine;
[System.Serializable]

[CreateAssetMenu(menuName = "Upgrades", fileName = "New Upgrade Save Data")]
public class UpgradeObject : ScriptableObject {

    [Header("Arms Snuggling")]
    public float armsSnuggling;
    public int armsSnugglingCost;
    public int armsSnugglingLvl;

    [Header("Artisans Slugs")]
    public float artisanSlugs;
    public int artisanRoundsCost;
    public int artisanRoundsLvl;

    [Header("Better Gunpowder")]
    public float betterGunpowder;
    public int betterGunpowderCost;
    public int betterGunpowderLvl;

    [Header("Lighter Silks")]
    public float lighterSilks;
    public int lighterSilksCost;
    public int lighterSilksLvl;

    [Header("Powder Packing")]
    public float powderPacking;
    public int powderPackingCost;
    public int powderPackingLvl;

    [Header("Reinforced Corset")]
    public float reinforcedCorset;
    public float reinforcedCorsetPower;
    public int reinforcedCorsetCost;
    public int reinforcedCorsetLvl;

    [Header("Rubbloomers")]
    public float rubBloomers;
    public int rubbloomersCost;
    public int rubbloomersLvl;

    [Header("Scorpion Traps")]
    public float scorpionTraps;
    public int scorpionTrapsCost;
    public int scorpionTrapsLvl;

    [Header("Shot Practice")]
    public float shotPractice;
    public int shotPracticeCost;
    public int shotPracticeLvl;

    [Header("Wider Dress")]
    public float widerDress;
    public int widerDressCost;
    public int widerDressLvl;
}