using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    private bool active;
    private float cooldown = 5;
    private float duration = 5;
    private int speed = 10, acceleration = 10;

    public Transform princess;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        princess = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        OnUse();
    }

    public void OnUse() {
        if (!active) {
            active = true;
            StartCoroutine(AbilityUsed());
        }
        StartCoroutine(FlyToPrincess());

    }

    private IEnumerator AbilityUsed() {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < duration; i++) {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            Debug.Log(i);
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.None;
        transform.parent = null;
        yield return new WaitForSeconds(cooldown);
        Destroy(gameObject);
    }

    private IEnumerator FlyToPrincess() {
        transform.position = Vector2.Lerp(transform.position, princess.transform.position, Time.deltaTime * (speed * acceleration));
        yield return new WaitForSeconds(1);
        transform.position = princess.position;
        transform.parent = princess;
    }
}