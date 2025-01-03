using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class ProjectileController : MonoBehaviour {

    private EnemyPath enemyPath;
    private PrincessUpdate princess;
    private WeaponFire weaponFire;

    private GameObject enemy;

    private float enemySpeed, enemyLatSpeed;

    public float lifeTime;
    public bool fromPlayer, fromEnemy, isFlyEye, isScorBear, isThunderOoze, isHoming;

    public void Awake() {
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessUpdate>();
        weaponFire = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
        DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Support Enemy")) {
            if (fromEnemy) {
                enemy = other.gameObject;
                enemyPath = enemy.GetComponent<EnemyPath>();

                if (isThunderOoze && enemyPath) {
                    Debug.Log("lightning hit");
                    enemyPath.speed += 5;
                    enemyPath.latSpeed += 5;
                    
                    //more health? | more armor? | more hit power/power?
                }
                if (other.gameObject.CompareTag("Player")) {
                    weaponFire.bulletsLeft--;
                    princess.GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
                }                
            }
            if (fromPlayer) {
                Destroy(other.gameObject);
            }
        } 
        Destroy(gameObject);
    }


    void FixedUpdate() {
        if (isHoming)
            gameObject.transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Enemy").transform.position, Time.fixedDeltaTime * 50);
    }

    void DestroyProjectile() {
        Destroy(gameObject, lifeTime);
    }
}