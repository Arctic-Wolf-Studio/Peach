using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWeaponFire : MonoBehaviour {

    public int damage;
    public float shootForce, upwardForce;

    public float fireRate, spread, range, reload, timeBetweeenShots;
    public int magazineSize, bullectsPerTap;
    public bool allowButtonHold, autoFire, isDown, isForward;

    public int bulletsLeft, bulletsShot;

    public bool shooting, readyToShoot, reloading;
    public bool isWandOfRandom;

    public float offset;
    private float rotZ;
    Vector3 difference;

    public Transform barrel;
    public GameObject muzzleFlash;
    public GameObject projectile;
    public RaycastHit2D raycastHit2D;
    public LayerMask enemy;

    public bool allowInvoke = true;

    public TextMeshProUGUI ammunitionDisplay;

    private void Awake() {

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update() {
        Fire();
        //transform.rotation = Quaternion.Euler(0f, 0f, princess.crosshair.transform.rotation.z);
    }

    private void Fire() {
        if (readyToShoot && autoFire && !allowButtonHold) { Shoot(); }

    }

    private void Shoot() {
        readyToShoot = false;

        //Spread (not implemented)
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        if (muzzleFlash != null) {
            Instantiate(muzzleFlash, barrel.position, transform.rotation);
            DestroyFlash();
        }

        GameObject bullet = Instantiate(projectile, barrel.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(shootForce, 0f, 0f);
        bullet.GetComponent<SpriteRenderer>().flipX = true;

        if (isForward)
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * -shootForce;
        if (isDown)
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -shootForce, 0f);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }

        if (bulletsShot < bullectsPerTap && bulletsLeft > 0) {
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

    private void DestroyFlash() {
        Destroy(muzzleFlash, 0.5f);
    }
}