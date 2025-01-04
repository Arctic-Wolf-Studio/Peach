using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour {
    public CannonObject cannon;
    public PrincessObject princess;
    public UpgradeObject upgrade;

    public GameObject[] stars;
    public Sprite coinImage;
    public Image photos;
    public Sprite[] upgradeImage;
    public int id;
    public int max_lvl = 10;

    public TextMeshProUGUI cost;
    public TextMeshProUGUI nextPower;
    public TextMeshProUGUI power;
    public TextMeshProUGUI coinDisplay;

    private void Start() {
        cannon = Resources.Load<CannonObject>("Cannon");
        princess = Resources.Load<PrincessObject>("Princess");
        upgrade = Resources.Load<UpgradeObject>("Princess Upgrades");
    }

    private void Update() {
        ChooseUpgrade();
        coinDisplay.text = "" + princess.coins; 
    }
    public void UpgradePowderPacking() {
        if (cannon.cannonLevel < max_lvl) {
            if (princess.coins >= cannon.cannonCost) {
                princess.coins -= cannon.cannonCost;
                cannon.cannonPower += 5;
                cannon.cannonCost *= 2;
                cannon.cannonLevel++;
            }
        }

    } //increase launch power from all cannons
    public void UpgradeArmsSnuggling() { 
        if (princess.coins >= upgrade.armsSnugglingCost) {
            princess.coins -= upgrade.armsSnugglingCost;
            upgrade.armsSnuggling += 5;
            upgrade.armsSnugglingCost *= 2;
            upgrade.armsSnugglingLvl++;
        }
    }  //free rounds faster
    public void UpgradeArtisanRounds() {
        if (princess.coins >= upgrade.artisanRoundsCost) {
            princess.coins -= upgrade.artisanRoundsCost;
            upgrade.artisanSlugs += 5;
            upgrade.artisanRoundsCost *= 2;
            upgrade.artisanRoundsLvl++;
        }
    } //upgrades the recoil momentum from of all weapons
    public void UpgradeBetterGunpowder() { 
        if (princess.coins >= upgrade.betterGunpowderCost) {
            princess.coins -= upgrade.betterGunpowderCost;
            upgrade.betterGunpowder += 5;
            upgrade.betterGunpowderCost *= 2;
            upgrade.betterGunpowderLvl++;
        }
    } //increase the weapon's projectile velocity
    public void UpgradeLighterSilks() { 
        if(princess.coins >= upgrade.lighterSilksCost) {
            princess.coins -= upgrade.lighterSilksCost;
            upgrade.lighterSilks += 5;
            upgrade.lighterSilksCost *= 2;
            upgrade.lighterSilksLvl++;
        }
    } // reduce drag of the princess (lose less horizontal speed as player moves forward)
    public void UpgradeRubbloomers() {
        if (princess.coins >= upgrade.rubbloomersCost) {
            princess.coins -= upgrade.rubbloomersCost;
            upgrade.rubBloomers += 5;
            upgrade.rubbloomersCost *= 2;
            upgrade.rubbloomersLvl++;
        }
    } // princess bounces higher after initial hit of ground
    public void UpgradeScorpionTraps() {
        if (princess.coins >= upgrade.scorpionTrapsCost) {
            princess.coins -= upgrade.scorpionTrapsCost;
            upgrade.scorpionTraps += 5;
            upgrade.scorpionTrapsCost *= 2;
            upgrade.scorpionTrapsLvl++;
        }
    } // ScorBear will appear at a later distance
    public void UpgradeShotPractice()
    {
        if (princess.coins >= upgrade.shotPracticeCost)
        {
            princess.coins -= upgrade.shotPracticeCost;
            upgrade.shotPractice += 5;
            upgrade.shotPracticeCost *= 2;
            upgrade.shotPracticeLvl++;
        }
    } // reload all weapons faster
    public void UpgradeReinforcedCorset()
    {
        if (princess.coins >= upgrade.reinforcedCorsetCost)
        {
            princess.coins -= upgrade.reinforcedCorsetCost;
            upgrade.reinforcedCorset += 5;
            upgrade.reinforcedCorsetCost *= 2;
            upgrade.reinforcedCorsetLvl++;
            Debug.Log($"corset power {upgrade.reinforcedCorset}");
            Debug.Log($"corset cost {upgrade.reinforcedCorsetCost}");
            Debug.Log($"corset level {upgrade.reinforcedCorsetLvl}");
        }
    } //lose less momentum from enemies' hits
    public void UpgradeWiderDress() { 
        if (princess.coins >= upgrade.widerDressCost) {
            princess.coins -= upgrade.widerDressCost;
            upgrade.widerDress += 5;
            upgrade.widerDressCost *= 2;
            upgrade.widerDressLvl++;
        }
    } //lose less vertical speed (does not apply to enemy hits)
    public void SelectedUpgrade(int getid) {
        Debug.Log("ID number = " + getid);
        id = getid;
    }
    public void ImageChange() { 
        for(int i = 0; i < upgradeImage.Length; i++) { 
            if(id == i + 1) {
                photos.sprite = upgradeImage[i];
                Debug.Log("Image Active");
            }
        }
    }
    public void ChooseUpgrade() {
        
        switch (id)
        {
            case 1:
                Debug.Log("ID has worked");
                StarLvlUpdate(cannon.cannonLevel);
                cost.text = "$" + cannon.cannonCost;
                nextPower.text = "+ " + 5;
                power.text = "" + cannon.cannonPower;
                ImageChange();
                break;

            case 2:
                StarLvlUpdate(upgrade.reinforcedCorsetLvl);
                cost.text = "$" + upgrade.reinforcedCorsetCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.reinforcedCorset;
                ImageChange();
                break;

            case 3:
                StarLvlUpdate(upgrade.betterGunpowderLvl);
                cost.text = "$" + upgrade.betterGunpowderCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.betterGunpowder;
                ImageChange();
                break;

            case 4:
                StarLvlUpdate(upgrade.widerDressLvl);
                cost.text = "$" + upgrade.widerDressCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.widerDress;
                break;

            case 5:
                StarLvlUpdate(upgrade.artisanRoundsLvl);
                cost.text = "$" + upgrade.artisanRoundsCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.artisanSlugs;
                break;

            case 6:
                StarLvlUpdate(upgrade.lighterSilksLvl);
                cost.text = "$" + upgrade.lighterSilksCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.lighterSilks;
                break;

            case 7:
                StarLvlUpdate(upgrade.armsSnugglingLvl);
                cost.text = "$" + upgrade.armsSnugglingCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.armsSnuggling;
                break;

            case 8:
                StarLvlUpdate(upgrade.shotPracticeLvl);
                cost.text = "$" + upgrade.shotPracticeCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.shotPractice;
                break;

            case 9:
                StarLvlUpdate(upgrade.rubbloomersLvl);
                cost.text = "$" + upgrade.rubbloomersCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.rubBloomers;
                break;

            case 10:
                StarLvlUpdate(upgrade.scorpionTrapsLvl);
                cost.text = "$" + upgrade.scorpionTrapsCost;
                nextPower.text = "+ " + 5;
                power.text = "" + upgrade.scorpionTraps;
                break;
        }
    }
    public int StarLvlUpdate(int value) {
        for (int i = 0; i < stars.Length; i++) {
            if (i < value) {
                stars[i].SetActive(true);
            } else {
                stars[i].SetActive(false);
            }
        }
        return value;
    }
}