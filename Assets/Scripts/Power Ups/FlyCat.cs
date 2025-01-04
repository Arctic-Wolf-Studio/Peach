using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyCat : MonoBehaviour
{
    private bool active;
    private float cooldown = 5;
    private float duration = 5;
    private int speed = 10;

    public Transform princess;
    public Rigidbody2D princessRB;
    public AIController aic;

    // Start is called before the first frame update
    void Start()
    {
        princess = GameObject.FindGameObjectWithTag("Player").transform;
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnUse();
    }

    public void OnUse()
    {
        if (active != true)
        {
            active = true;
            StartCoroutine(AbilityUsed());
        }
        transform.position = Vector2.MoveTowards(transform.position, princess.transform.position, Time.deltaTime * (speed + princessRB.velocity.magnitude));
    }

    private IEnumerator AbilityUsed()
    {
        yield return new WaitForSeconds(5);
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
}
