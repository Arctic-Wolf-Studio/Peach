using System.Collections;
using UnityEngine;

public class Squirrel : MonoBehaviour {
    private WeaponFire weapon;

    public Transform princess;
    public Rigidbody2D princessRB;

    private bool active;
    private int speed = 40;
    private float cooldown = 5;
    private int giveAmmo = 0;
    private int ammoBox = 10;

    private void Start() {
        princess = GameObject.FindGameObjectWithTag("Player").transform;
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponFire>();
    }

    void FixedUpdate() {
        OnUse();
    }

    //ability effect
    public void OnUse() {
        if (active != true) {
            active = true;
            if (giveAmmo < ammoBox) {
                weapon.bulletsLeft++;
                giveAmmo++;
                active = false;
                Debug.Log("gave ammo");
            } else {
                StartCoroutine(AbilityUsed());
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x - 1, princess.transform.position.y + 1), Time.deltaTime * (speed + princessRB.velocity.magnitude));
    }

    //cooldown
    private IEnumerator AbilityUsed() {
        yield return new WaitForSeconds(cooldown);
        active = false;
        Destroy(gameObject);
    }
}