using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {
    public PrincessUpdate princess;
    public bool isActive;
    public bool isRotating;
    public bool isDone;
    public float duration;
    public float cooldown;
    public float speed;

    private BoxCollider2D fan;
    private SpriteRenderer flip;

    private void Start() {
        fan = GetComponent<BoxCollider2D>();
        flip = GetComponent<SpriteRenderer>();
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessUpdate>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy") == true)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        OnActivation();
    }

    public void OnActivation() {
        Quaternion rotate = Quaternion.Euler(0, 0, 30);
        Quaternion stable = Quaternion.Euler(0, 0, 0);
        if (isActive == false) {
            StartCoroutine(Shield());
            isActive = true;
        }

        if (isActive == true && isDone == false) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x - 5, princess.transform.position.y), Time.deltaTime * (1 + princess.rb.velocity.magnitude));
        }

        if (isRotating == true) {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime);
        }

        if (isDone == true) {
            transform.Translate(Vector2.one * Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, stable, Time.deltaTime);
        }
    }

    private IEnumerator Shield() {
        yield return new WaitForSeconds(3);

        //Do some fancy steel/titanium shiny effect
        flip.flipX = true;
        isRotating = true;
        fan.enabled = true;
        yield return new WaitForSeconds(duration);

        isDone = true;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}