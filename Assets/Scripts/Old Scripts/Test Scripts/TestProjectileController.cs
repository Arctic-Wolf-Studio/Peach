using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectileController : MonoBehaviour {

    private EnemyTest enemyTest;

    public float lifeTime;
    public bool fromPlayer, fromEnemy, isThunderOoze, isHoming;

    public void Awake() {
        DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        enemyTest = other.GetComponent<EnemyTest>();

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Support Enemy")) {
            if (fromEnemy) {
                if (isThunderOoze) {
                    enemyTest.latSpeed += 5;
                    enemyTest.speed += 5;

                    //more health? | more armor? | more hit power/power?
                }
            }
            if (fromPlayer) {
                enemyTest.GetComponent<TestHealthManager>().Damage(TestWeaponFire.Instance.damage);
                Debug.Log("damage enemy");
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
