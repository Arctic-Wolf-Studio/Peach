using UnityEngine;

public class WandProjectiles : MonoBehaviour {

    public float lifeTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    void DestroyProjectile() {
        Destroy(gameObject, lifeTime);
    }
}
