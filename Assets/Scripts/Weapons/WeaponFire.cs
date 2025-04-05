using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

    public static Action<float, float> shotMovement;
    public static Action<Vector2> OnMovement;

    [Header("Components")]
    private Cannon cannon;
    public WeaponManager manager;
    private PrincessController princess;
    private WeaponBlueprint weapon;

    public Transform weaponPivotPoint;

    public int damage;
    public float shootForce, upwardForce;

    public float fireRate, range, reload, timeBetweeenShots;
    [Range(-1, 1)] public float spread;
    public int magazineSize, bullectsPerTap;
    public bool allowButtonHold, autoFire, isEnemy, isDown, isForward;

    public int bulletsLeft, bulletsShot;

    public bool shooting, readyToShoot, reloading;

    public float offset;
    private float rotZ;
    [HideInInspector] public Vector2 movemewnt;
    Vector3 difference;

    public Transform barrel;
    //public GameObject muzzleFlash;
    public GameObject projectile;

    public bool allowInvoke = true;

    private void Awake() {
        cannon = Cannon.GetCannon();
        manager = WeaponManager.GetWeaponManager();
        princess = PrincessController.GetPrincessController();
        weaponPivotPoint = GameObject.FindGameObjectWithTag("Weapon Pivot Point").GetComponent<Transform>();
        bulletsLeft = magazineSize;
        movemewnt = new Vector2(shootForce, upwardForce);
        readyToShoot = false;
    }
    protected virtual void Start() {
        shooting = Input.GetKeyUp(KeyCode.Mouse0);
    }

    protected virtual void Update() {
        if (!Cannon.GetCannon().cannonFire && Cannon.GetCannon().IsUI()) {
            return;
        }
        Fire();
    }

    protected virtual void Fire() {

        readyToShoot = true;

        if (allowButtonHold) shooting = Input.GetKeyUp(KeyCode.Mouse0);
        else shooting = Input.GetKeyUp(KeyCode.Mouse0);

        //if (bulletsLeft == 0 && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = bullectsPerTap;
            Shoot();
        }
        if (readyToShoot && autoFire && !allowButtonHold) {
            bulletsShot = bullectsPerTap;
            Shoot(); 
        }
    }

    protected virtual void Shoot() {

        readyToShoot = false;
        shotMovement?.Invoke(shootForce, upwardForce);
        OnMovement?.Invoke(movemewnt);

        float y = UnityEngine.Random.Range(-spread, spread);

        float Distance = princess.difference.magnitude;
        Vector2 shootDirection = princess.difference / Distance;
        shootDirection.Normalize();

        GameObject bullet = Instantiate(projectile, barrel.position, Quaternion.Euler(0,0,princess.rotation));
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * -shootForce * princess.rb.velocity;

        bulletsLeft--;
        bulletsShot--;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweeenShots);     
    }
    protected virtual void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }
    protected virtual void Reload() {
        reloading = true;
        Invoke("ReloadFinished", reload);
    }
    protected virtual void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public virtual void LostAmmo() {
        bulletsLeft--;
    }
}