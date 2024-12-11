public abstract class WeaponLibrary
{
    public static void UpdateWeapon(int index, Weapon w) {
        switch (index) {
            case 1: shotgun(w); break;
        }
    }

    public static void shotgun(Weapon data) {
        data.damage = 1;
        data.shootForce = 30;
        data.fireRate = 1.2f;
        data.range = 50;
        data.reload = 1.2f;
        data.bullectsPerTap = 1;
        data.bulletsLeft = 6;
        data.bulletsShot = 0;
    }


    public static Weapon Shotgun() {
        Weapon data = new Weapon();
        shotgun(data);
        return data;
    }
}