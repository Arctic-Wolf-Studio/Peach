using UnityEngine;
using TMPro;

[System.Serializable]
public class TestWeaponFire : MonoBehaviour {

    public static TestWeaponFire Instance;
    public TestFollowCursor followCursor;
    
    public int damage;
    public float shootForce, upwardForce;

    public float fireRate, spread, range, reload, timeBetweeenShots;
    public int magazineSize, bullectsPerTap;
    public bool allowButtonHold, autoFire, isEnemy, isDown, isForward;

    public int bulletsLeft, bulletsShot;

    public bool shooting, readyToShoot, reloading, compareFire;
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
        Instance = this;
        if (!isEnemy) {
            followCursor = GetComponent<TestFollowCursor>();
        }
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    void Start() {
        
        shooting = Input.GetKey(KeyCode.Mouse0);
    }

    private void FixedUpdate() {
        /*if (!isEnemy) {
            Cursor();
        }*/
    }

    private void Update() {
        if (!compareFire) {
            Fire();
        }
        Fire2();
        //transform.rotation = Quaternion.Euler(0f, 0f, princess.crosshair.transform.rotation.z);
    }

    private void Fire() {

        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyUp(KeyCode.Mouse0);

        //if (readyToShoot && shooting && !reloading && bulletsLeft <=0) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = 0;
            Shoot();
        }
        if (readyToShoot && autoFire && !allowButtonHold) { Shoot(); }
        if (readyToShoot && shooting && !reloading && isWandOfRandom && bulletsLeft > 0) {
            SpawnObjects();
        }
    }

    private void Fire2() {

        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyUp(KeyCode.Mouse0);

        //if (readyToShoot && shooting && !reloading && bulletsLeft <=0) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = 0;
            Shoot2();
        }
    }

    public void SpawnObjects() {
        readyToShoot = false;

        float Distance = difference.magnitude;
        Vector2 Direction = difference / Distance;
        Direction.Normalize();

        GameObject bullet = Instantiate(projectile, barrel.position, transform.rotation);
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
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

        //Spread (not implemented)
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        if (muzzleFlash != null) {
            Instantiate(muzzleFlash, barrel.position, transform.rotation);
            DestroyFlash();
        }

        float Distance = followCursor.difference.magnitude;
        Vector2 Direction = followCursor.difference / Distance;
        Direction.Normalize();

        if (!isEnemy) {
            GameObject currentBullet = Instantiate(projectile, barrel.position, Quaternion.Euler(0,0, followCursor.rotationZ));
            currentBullet.GetComponent<Rigidbody2D>().velocity = Direction * -shootForce;
            //currentBullet.GetComponent<Rigidbody2D>().AddForce(Direction * -shootForce, ForceMode2D.Impulse);
            Debug.Log(Direction + " : " + shootForce);
        } else {
            GameObject bullet = Instantiate(projectile, barrel.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(shootForce, 0f, 0f);
            bullet.GetComponent<SpriteRenderer>().flipX = true;

            if (isForward)
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * -shootForce;
            if (isDown)
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -shootForce, 0f);
        }
        //princess.FlyAndShoot();

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

    public void Shoot2() {

        readyToShoot = false;
        //shotMovement?.Invoke(shootForce, upwardForce);

        float y = UnityEngine.Random.Range(-spread, spread);

        float Distance = followCursor.difference.magnitude;
        Vector2 shootDirection = followCursor.difference / Distance;
        shootDirection.Normalize();

        GameObject bullet = Instantiate(projectile, barrel.position, Quaternion.Euler(0, 0, followCursor.rotationZ));
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * -shootForce;

        bulletsLeft--;
        bulletsShot--;

        if (allowInvoke) {
            Invoke("ResetShot", fireRate);
            allowInvoke = false;
        }

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweeenShots);
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