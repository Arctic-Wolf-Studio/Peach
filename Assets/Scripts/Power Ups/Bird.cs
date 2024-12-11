using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public PrincessController princess;
    public GameObject spawner;
    public float speed;
    public float duration;

    public bool isActive;
    public bool isDone;

    private SpriteRenderer flip;

    private void Start() {
        flip = GetComponent<SpriteRenderer>();
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessController>();
        spawner = GameObject.FindGameObjectWithTag("Spawn");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //if (collision.gameObject.CompareTag("Enemy") == true)
            //collision.gameObject.GetComponent<FlyEye_OLD>().isStunned = true;
    }

    private void FixedUpdate() {
        OnActivation();
    }

    private void OnActivation() {
        if (isActive == false) {
            StartCoroutine(Sing());
            isActive = true;
        }

        if (isActive == true && isDone == false) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x + 1, princess.transform.position.y + 7), Time.deltaTime * (speed + princess.rb.velocity.magnitude));
        }

        if (isDone == true) {
            transform.Translate(speed * Time.deltaTime * Vector2.one);
        }
    }

    private IEnumerator Sing() {
        yield return new WaitForSeconds(3);

        spawner.SetActive(false);
        yield return new WaitForSeconds(duration);

        flip.flipX = false;
        isDone = true;
        spawner.SetActive(true);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
