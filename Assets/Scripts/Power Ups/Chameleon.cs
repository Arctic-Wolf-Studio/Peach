using System.Collections;
using UnityEngine;

public class Chameleon : MonoBehaviour {
    public PrincessUpdate princess;
    public SpriteRenderer sprite;
    public GameObject prey;
    public BoxCollider2D detectionArea;
    public Animation colorChange;

    public bool isActive;
    public bool isDone;
    public bool isGrabbing;

    public float duration;
    public float speed;
    public float tongueSpeed;
    public float eatTime;
    public float eatCooldown;

    public float visible;

    private void Start() {
        princess = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessUpdate>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy") == true && Time.time > eatTime) {
            prey = collision.gameObject;
            prey.GetComponent<FlyEye_OLD>().isGrabbed = true;

            isGrabbing = true;
            eatTime = Time.time + eatCooldown;
            Destroy(prey, 3);
        }
    }

    private void FixedUpdate() {
        OnActivation();
    }

    private void OnActivation() {
        if (isActive == false) {
            StartCoroutine(TongueGrab());
            isActive = true;
        }

        if (isActive == true)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x - 5, princess.transform.position.y + 7.5f), Time.deltaTime * (speed + princess.rb.velocity.magnitude));

        if (isActive == true && isDone == false && visible < 1)
            sprite.color = new Color(255, 255, 255, visible += 0.01f);

        if (isDone == true & visible > 0)
            sprite.color = new Color(255, 255, 255, visible -= 0.01f);

        if (isGrabbing == true && prey != null)
            prey.transform.position = Vector2.MoveTowards(prey.transform.position, transform.position, Time.deltaTime * (tongueSpeed + princess.rb.velocity.magnitude));
        else
            prey = null;

        Debug.Log(visible);
        Debug.Log(sprite.color);
    }

    private IEnumerator TongueGrab() {
        //colorChange.Play();
        yield return new WaitForSeconds(3);
        detectionArea.enabled = true;

        yield return new WaitForSeconds(duration);
        detectionArea.enabled = false;
        isDone = true;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}