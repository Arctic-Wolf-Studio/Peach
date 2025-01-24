using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

    [Header("Components")]
    private Cannon cannon;
    public WeaponManager manager;
    private PrincessController princess;
    private WeaponBlueprint weapon;

    public static Action<float, float> shotMovement;

    public int damage;
    public float shootForce, upwardForce;

    public float fireRate, spread, range, reload, timeBetweeenShots;
    public int magazineSize, bullectsPerTap;
    public bool allowButtonHold, autoFire, isEnemy, isDown, isForward;

    public int bulletsLeft, bulletsShot;

    public bool shooting, readyToShoot, reloading;

    public float offset;
    private float rotZ;
    Vector3 difference;

    public Transform barrel;
    //public GameObject muzzleFlash;
    public GameObject projectile;

    public bool allowInvoke = true;

    private void Awake() {
        cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Cannon>();
        manager = GameObject.FindGameObjectWithTag("Weapon Manager").GetComponent<WeaponManager>();
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessController>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    protected virtual void Start() {
        shooting = Input.GetKey(KeyCode.Mouse0);
    }

    protected virtual void Update() {
        Fire();
    }

    protected virtual void Fire() {

        if (allowButtonHold) shooting = Input.GetKeyUp(KeyCode.Mouse0);
        else shooting = Input.GetKeyUp(KeyCode.Mouse0);

        if (bulletsLeft == 0 && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && cannon.cannonFire) {
            bulletsShot = bullectsPerTap;
            Shoot();
        }
        if (readyToShoot && autoFire && !allowButtonHold && cannon.cannonFire) {
            bulletsShot = bullectsPerTap;
            Shoot(); 
        }
    }

    protected virtual void Shoot() {
        readyToShoot = false;

        shotMovement?.Invoke(shootForce, upwardForce);

        float y = UnityEngine.Random.Range(-spread, spread);

        Vector2 shootDirection = -transform.right + new Vector3(0, y, 0);
        shootDirection.Normalize();

        GameObject bullet = Instantiate(projectile, barrel.position, barrel.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * shootForce;

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