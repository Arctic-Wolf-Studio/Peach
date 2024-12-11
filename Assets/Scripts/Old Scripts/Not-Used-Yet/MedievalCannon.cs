using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedievalCannon : CannonTemplateScript
{

    public int[] upgradeCosts = new int[] { 5, 6, 7, 8, 9, 0 }; //These are placeholder values. The 0 is for when the cannon cannot be further upgraded.
    public int Level { get; set; }
    public bool canEvolve { get; set; } // does not mean "can it evolve NOW" but "is there a tier for it to evolve to"
    public bool canUpgrade { get; set; } // does mean "can it be upgraded NOW"

    public int basePower { get; set; }
    public int upgradeAmt { get; set; }
    public string cannonName{ get; set; }
    public int evolutionTier{ get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cannon has arrived (start method called)");
        //nextEvolution = new MedievalCannon(); //may need code stuff thingy do to it
    }

    public MedievalCannon()
    {
        Level = 0;
        basePower = 50;
        upgradeAmt = 5;
        canEvolve = false;
        canUpgrade = true;
        cannonName = "Medieval Cannon";
        evolutionTier = 1;
        Debug.Log("Cannon Initialized");

    }

    //Returns the cost to upgrade. Returns 0 if the cannon cannot be upgraded.
    public int getUpgradeCost()
    {
        return upgradeCosts[Level];
    }

    //Returns the cost to evolve to the next tier of cannon. Returns 0 if the cannon is at maximum tier and cannot evolve past its current tier.
    public int getEvolutionCost()
    {
        return 20;
    }

    //When called, increases the cannon's upgrade level by one.
    public void LvlUp()
    {
        Level++;
        Debug.Log("Leveled up to " + Level);
        if (Level >= 5)
        {
            canUpgrade = false;
            Debug.Log("No more upgrades lewl");
        }
        basePower += upgradeAmt;
    }

    //When called, evolves the cannon
    public CannonTemplateScript Evolve()
    {
        return this;

    }

    public int getCannonPower()
    {
        return basePower + (upgradeAmt * Level);
    }
}
