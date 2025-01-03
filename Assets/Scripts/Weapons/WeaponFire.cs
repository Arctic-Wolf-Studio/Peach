using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

    [Header("Components")]
    private Cannon cannon;
    public WeaponManager manager;
    private PrincessController princess;

    public static Action<float, float> shotMovement;

    public int damage;
    public float shootForce, upwardForce;

    public float fireRate, spread, range, reload, timeBetweeenShots;
    public int magazineSize, bullectsPerTap;
    public bool allowButtonHold, autoFire, isEnemy, isDown, isForward;

    public int bulletsLeft, bulletsShot;

    public bool shooting, readyToShoot, reloading;
    public bool isWandOfRandom;

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
    void Start() {
        if (isWandOfRandom) {
            int ammoIndex = UnityEngine.Random.Range(0, manager.wand.Length);
            projectile = manager.wand[ammoIndex];
        }
        shooting = Input.GetKey(KeyCode.Mouse0);
    }

    private void Update() {
        Fire();
    }

    private void Fire() {

        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyUp(KeyCode.Mouse0);

        if (bulletsLeft == 0 && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && cannon.cannonFire) {
            bulletsShot = bullectsPerTap;
            Shoot();
            Debug.Log("fire");
        }
        if (readyToShoot && autoFire && !allowButtonHold && cannon.cannonFire) {
            bulletsShot = bullectsPerTap;
            Shoot(); 
        }
        if (readyToShoot && shooting && !reloading && isWandOfRandom && bulletsLeft > 0) {
            SpawnObjects();
        }
    }

    public void SpawnObjects() {
        readyToShoot = false;

        float Distance = difference.magnitude;
        Vector2 Direction = difference / Distance;
        Direction.Normalize();

        GameObject bullet = Instantiate(projectile, barrel.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Direction * shootForce;

        //princess.FlyAndShoot();

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }
    }

    private void Shoot() {
        readyToShoot = false;

        shotMovement?.Invoke(shootForce, upwardForce);

        //Spread (not implemented)

        float y = UnityEngine.Random.Range(-spread, spread);


        /*if (!muzzleFlash.activeInHierarchy) {
            Instantiate(muzzleFlash, barrel.position, barrel.rotation);
        }*/

        Vector2 shootDirection = transform.forward + new Vector3(0, y, 0);
        shootDirection.Normalize();

        GameObject bullet = Instantiate(projectile, barrel.position, barrel.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * shootForce;


        bulletsLeft--;
        bulletsShot--;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }

        if (bulletsShot > 0 && bulletsLeft > 0) {
            Invoke("Shoot", timeBetweeenShots);
        }
    }
    private void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload() {
        reloading = true;
        Invoke("ReloadFinished", reload);
    }
    private void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void LostAmmo() {
        bulletsLeft--;
    }
}