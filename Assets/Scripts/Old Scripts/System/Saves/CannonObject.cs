using UnityEngine;
[System.Serializable]
public class CannonObject {
    public float jumpForce { get; set; }
    public float cannonPower { get; set; }
    public int nextCannonPower { get; set; }
    public int cannonLevel { get; set; }
    public int cannonCost { get; set; }
}