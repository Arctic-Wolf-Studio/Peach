using UnityEngine;

[System.Serializable]
public class currentWeapon : MonoBehaviour {
    public SystemUpdate update;
    private WeaponManager manager;

    //public GameObject weapon;
    public Transform bandolier;
    public Transform hud;
    public Transform weaponPosition;
    public Transform PrincessHands;
    private GameObject previousWeapon;

    private int selectedIndex;
    void Awake() {
        manager = GameObject.FindGameObjectWithTag("Weapon Manager").GetComponent<WeaponManager>();
        update = GameObject.FindGameObjectWithTag("System Update").GetComponent<SystemUpdate>();
        Debug.Log(manager);
        //GameObject weapon = manager.weapons[update.princess.currentWeapon];
        update.LoadData();
        
    }

    private void Start() {
        SelectWeapon();
    }

    public void SelectWeapon() {
        int weaponIndex = update.princess.CurrentWeapon;
        int bandolierIndex = update.princess.CurrentBandolier;
        //hud.transform.rotation = Quaternion.AngleAxis(90, Vector3.down);
        /*if (weaponIndex == 0) {
            GameObject tempWeapon = manager.weapons[weaponIndex];
            GameObject tempBandolier = manager.weapons[bandolierIndex];
            Instantiate(tempWeapon, weaponPosition.position, Quaternion.identity, PrincessHands);
            Instantiate(tempBandolier, bandolier.position, Quaternion.identity);
        }*/
        if (weaponIndex != selectedIndex || weaponIndex == selectedIndex) {
            previousWeapon = GameObject.FindGameObjectWithTag("Weapon");
            GameObject tempWeapon = manager.weapons[weaponIndex];
            GameObject tempBandolier = manager.bandolier[bandolierIndex];
            Instantiate(tempWeapon, weaponPosition.position, Quaternion.identity, PrincessHands);
            Instantiate(tempBandolier, bandolier.position, tempBandolier.transform.rotation, bandolier);
            //tempBandolier.transform.rotation = Quaternion.Euler(0, 90, 0);
            selectedIndex = weaponIndex;
        }
    }
}