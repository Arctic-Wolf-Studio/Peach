using UnityEngine;

[System.Serializable]
public class PrincessStats : MonoBehaviour {

    public static PrincessStats Instance;

    private void Awake() {
        Instance = this;
    }

    [SerializeField] public int Coins { get; set; }
    [SerializeField] public int Crowns { get; set; }
    [SerializeField] public int Score { get; set; }
    [SerializeField] public int Distance { get; set; }
    [SerializeField] public int CurrentBandolier { get; set; }
    [SerializeField] public int CurrentDress { get; set; }
    [SerializeField] public int CurrentWeapon { get; set; }
}