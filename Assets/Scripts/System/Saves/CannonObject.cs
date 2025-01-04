using UnityEngine;
[System.Serializable]

[CreateAssetMenu(menuName = "Cannon", fileName = "New Cannon Save Data")]
public class CannonObject : ScriptableObject {
    
    public float jumpForce;
    public float cannonPower;
    public int nextCannonPower;
    public int cannonLevel;
    public int cannonCost;
}