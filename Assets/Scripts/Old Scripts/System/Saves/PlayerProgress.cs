[System.Serializable]
public class PlayerProgress {

    //Achievement Data
    public int bulletsFired;
    public int enemiesStunned;
    public int coinsEarned;
    public int distanceTraveled;
    public int JewelsEarned;

    //Princess Data
    public int coins;
    public int crowns;
    public int score;
    public int distance;

    //worry about player profile name/id, skins, and weapon loadout later

    //Cannon Data
    public float cannonPower;
    public float jumpForce;
    public int next_cannon_power;
    public int cannon_level;
    public int cannon_cost;

    //Weapon
    //weapon slot and properties respectively
    public int currentWeapon;
    public int currentDress;
    public int currentBandolier;
    #region UPGRADES
    public float armsSnuggling;
    public float artisanSlugs;
    public float betterGunpowder;
    public float lightSilks;
    public float reinforcedCorset;
    public float rubBloomers;
    public float scorpionTraps;
    public float shotPractice;
    public float widerDress;

    public int armsSnugglingCost;
    public int artisanRoundsCost;
    public int betterGunpowderCost;
    public int lightSilksCost;
    public int reinforcedCorsetCost;
    public int rubbloomersCost;
    public int scorpionTrapsCost;
    public int shotPracticeCost;
    public int widerDressCost;

    public int armsSnugglingLvl;
    public int artisanRoundsLvl;
    public int betterGunpowderLvl;
    public int lightSilksLvl;
    public int packedPowderLvl;
    public int reinforcedCorsetLvl;
    public int rubbloomersLvl;
    public int scorpionTrapsLvl;
    public int shotPracticeLvl;
    public int widerDressLvl;
    #endregion

    public PlayerProgress(SystemUpdate variables) {
        #region PRINCESS SAVES
        coins = variables.princess.coins;
        crowns = variables.princess.Crowns;
        score = variables.princess.Score;
        distance = variables.princess.Distance;
        currentWeapon = variables.princess.CurrentWeapon;
        currentDress = variables.princess.CurrentDress;
        currentBandolier = variables.princess.CurrentBandolier;
        #endregion
        #region ACHIEVEMENT SAVES
        bulletsFired = variables.achievement.bullets_fired;
        enemiesStunned = variables.achievement.enemies_stunned;
        coinsEarned = variables.achievement.coins_earned;
        distanceTraveled = variables.achievement.distance_traveled;
        JewelsEarned = variables.achievement.jewels_earned;
    #endregion
        #region CANNON SAVES
    cannonPower = variables.cannon.cannonPower;
        jumpForce = variables.cannon.jumpForce;
        next_cannon_power = variables.cannon.nextCannonPower;
        cannon_level = variables.cannon.cannonLevel;
        cannon_cost = variables.cannon.cannonCost;
        #endregion
        #region UPGRADE SAVES
        armsSnuggling = variables.upgrade.armsSnuggling;
        artisanSlugs = variables.upgrade.artisanSlugs;
        shotPractice = variables.upgrade.shotPractice;
        betterGunpowder = variables.upgrade.betterGunpowder;
        lightSilks = variables.upgrade.lighterSilks;
        reinforcedCorset = variables.upgrade.reinforcedCorset;
        rubBloomers = variables.upgrade.rubBloomers;
        scorpionTraps = variables.upgrade.scorpionTraps;
        widerDress = variables.upgrade.widerDress;
        armsSnugglingCost = variables.upgrade.armsSnugglingCost;
        artisanRoundsCost = variables.upgrade.artisanRoundsCost;
        shotPracticeCost = variables.upgrade.shotPracticeCost;
        betterGunpowderCost = variables.upgrade.betterGunpowderCost;
        lightSilksCost = variables.upgrade.lighterSilksCost;
        reinforcedCorsetCost = variables.upgrade.reinforcedCorsetCost;
        rubbloomersCost = variables.upgrade.rubbloomersCost;
        scorpionTrapsCost = variables.upgrade.scorpionTrapsCost;
        widerDressCost = variables.upgrade.widerDressCost;
        armsSnugglingLvl = variables.upgrade.armsSnugglingLvl;
        artisanRoundsLvl = variables.upgrade.artisanRoundsLvl;
        shotPracticeLvl = variables.upgrade.shotPracticeLvl;
        betterGunpowderLvl = variables.upgrade.betterGunpowderLvl;
        lightSilksLvl = variables.upgrade.lighterSilksLvl;
        packedPowderLvl = variables.upgrade.powderPackingLvl;
        reinforcedCorsetLvl = variables.upgrade.reinforcedCorestLvl;
        rubbloomersLvl = variables.upgrade.rubbloomersLvl;
        scorpionTrapsLvl = variables.upgrade.scorpionTrapsLvl;
        widerDressLvl = variables.upgrade.widerDressLvl;
        #endregion
    }
}