using UnityEngine;
using UnityEngine.UI;


public class Bandelier : MonoBehaviour
{
    public WeaponFire fire;

    public Sprite emptyAmmo;
    public Sprite loadedAmmo;
    public Image[] ammo;

    void Start()
    {
        fire = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
    }

    // Update is called once per frame
    void Update() {
        BandelierSize();
    }

    public void BandelierSize() {
        for (int i = 0; i < ammo.Length; i++) {
            if (i < fire.bulletsLeft)
                ammo[i].sprite = loadedAmmo;
            else {
                ammo[i].sprite = emptyAmmo;
            }

       /*     if (i < fire.bulletsLeft)
                ammo[i].enabled = true;
            else
                ammo[i].enabled = false;*/
        }
    }
}