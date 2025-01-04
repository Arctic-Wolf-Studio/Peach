using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Princess", fileName = "New Princess Save Data", order = 0)]
public class PrincessObject : ScriptableObject {
    public int coins;
    public int Crowns;
    public int Score;
    public int Distance;
    public int CurrentBandolier;
    public int CurrentDress;
    public int CurrentWeapon;
}