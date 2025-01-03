using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CannonTemplateScript
{
    int Level{get;}
    int basePower{get;}
    bool canEvolve{ get; }
    bool canUpgrade{get;}
    int upgradeAmt{get;}
    string cannonName{get;}
    int evolutionTier{ get; set; }

    //Returns the cost to upgrade. Returns 0 if the cannon cannot be upgraded.
    int getUpgradeCost();

    //Returns the cost to evolve to the next tier of cannon. Returns 0 if the cannon is at maximum tier and cannot evolve past its current tier.
    int getEvolutionCost();

    //When called, increases the cannon's upgrade level by one.
    void LvlUp();

    //When called, evolves the cannon
    CannonTemplateScript Evolve();

    int getCannonPower();


}
