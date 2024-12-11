using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandRandom : MonoBehaviour {

    public int damage;
    public float shootForce;

    public float fireRate, range, reloadTime, timeBetweeenShots;
    public int magazineSize;

    public int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public int ammoIndex;

    public float offset;
    private float rotZ;
    Vector3 difference;

    public GameObject[] wandProjectiles;
    public Transform barrel;

    public bool allowInvoke = true;

    private void Awake() {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void FixedUpdate() {
        Cursor();
    }

    void Update() {
        Fire();
    }

    public void Fire() {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = 0;
            SpawnObjects();
        }
    }

    public void Cursor() {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        difference.Normalize();
    }


    public void SpawnObjects() {

        readyToShoot = false;

        float Distance = difference.magnitude;
        Vector2 Direction = difference / Distance;
        Direction.Normalize();

        ammoIndex = Random.Range(0, wandProjectiles.Length);

        GameObject bullet = Instantiate(wandProjectiles[ammoIndex], barrel.position, transform.rotation);
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        bullet.GetComponent<Rigidbody2D>().velocity = Direction * shootForce;

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }
    }

    private void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload() {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
