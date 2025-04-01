using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyCat : MonoBehaviour {

    private bool active;
    private float cooldown = 5;
    private float duration = 5;
    private int acceleration = 10, speed = 10;

    public Transform princessTransform;
    public Rigidbody2D princessRB;

    // Start is called before the first frame update
    void Start() {
        princessTransform = GameObject.FindGameObjectWithTag("Player").transform;
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update() {

        princessTransform = PrincessController.GetPrincessController().transform;
    }

    private void FixedUpdate() {
        OnUse();
    }

    public void OnUse() {

        if (active != true)
        {
            active = true;
            StartCoroutine(AbilityUsed());
        }
        StartCoroutine(FlyToPrincess());
    }

    private IEnumerator AbilityUsed() {

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < duration; i++)
        {
            princessRB.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            Debug.Log(i);
        }
        princessRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(duration);
        princessRB.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(cooldown);
        Destroy(gameObject);
    }
    private IEnumerator FlyToPrincess() {

        transform.position = Vector2.Lerp(transform.position, princessTransform.transform.position, Time.deltaTime * (speed * acceleration));
        yield return new WaitForSeconds(1);
        transform.position = princessTransform.position;
        transform.parent = princessTransform;
    }
}