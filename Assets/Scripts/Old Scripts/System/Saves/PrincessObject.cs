using UnityEngine;
[System.Serializable]
public class PrincessObject : ScriptableObject {
    public int coins { get; set; }
    public int Crowns { get; set; }
    public int Score { get; set; }
    public int Distance { get; set; }
    public int CurrentBandolier { get; set; }
    public int CurrentDress { get; set; }
    public int CurrentWeapon { get; set;}
}