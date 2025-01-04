using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandRandom : WeaponFire {

    public float reloadTime;

    public int ammoIndex;

    private float rotZ;
    Vector3 difference;

    public GameObject[] wandProjectiles;

    protected override void Start() {
        base.Start();
        int ammoIndex = UnityEngine.Random.Range(0, manager.wand.Length);
        projectile = manager.wand[ammoIndex];
    }

    protected override void Update() {
        base.Update();
    }

    protected override void Fire() {
        base.Fire();
    }


    protected override void Shoot() {

        readyToShoot = false;

        shotMovement?.Invoke(shootForce, upwardForce);

        float Distance = difference.magnitude;
        Vector2 Direction = difference / Distance;
        Direction.Normalize();

        ammoIndex = Random.Range(0, wandProjectiles.Length);

        GameObject bullet = Instantiate(wandProjectiles[ammoIndex], barrel.position, transform.rotation);
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        bullet.GetComponent<Rigidbody2D>().velocity = Direction * shootForce;

        bulletsLeft--;
        bulletsShot--;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }
    }

    protected override void ResetShot() {
        base.ResetShot();
    }

    protected override void Reload() {
        base.Reload();
    }

    protected override void ReloadFinished() {
        base.ReloadFinished();
    }

    public override void LostAmmo() {
        base.LostAmmo();
    }
}