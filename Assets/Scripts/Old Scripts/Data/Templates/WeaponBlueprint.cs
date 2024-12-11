using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponBlueprint : ScriptableObject
{
    public Sprite sprite;
    public GameObject projectile;

    public string weaponName;
    public int weaponID;

    public int ammunition;
    public int mag;

    public float fireRate;
    public float reload;
    public float speed;
    public float punch;
}