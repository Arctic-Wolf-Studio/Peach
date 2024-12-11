public class Weapon
{
    public int damage { get; set; }

    public float shootForce, upwardForce;

    public float fireRate, spread, range, reload, timeBetweeenShots;
    public int magazineSize, bullectsPerTap;
    public bool allowButtonHold, autoFire, isEnemy, isDown, isForward;

    public int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Weapon() { }

    public Weapon(int index) {
        WeaponLibrary.UpdateWeapon(index, this);
    }
}
