using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour {
    public Transform princess;
    public Rigidbody2D princessRB;
    public AIController aic;
    public WeaponStats weapon;
    private bool active;
    private bool fireState;
    public float speed = 20;
    public float monkeyRest = 10f;

    private void Start() {
        princess = GameObject.FindGameObjectWithTag("Player").transform;
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        aic = GameObject.FindGameObjectWithTag("Spawn").GetComponent<AIController>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponStats>();
    }

    private void Update() {
        StartCoroutine(AbilityUsed());
    }

    private void FixedUpdate() {
        Move();

        if (fireState == true) {
            //pc.SpreadFire();
        }
    }

    public void Move() {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(princess.transform.position.x, princess.transform.position.y), Time.deltaTime * (speed + princessRB.velocity.magnitude));
    }

    public void Fire() {
        if (weapon.ammo < weapon.magazine && Time.time > weapon.nextShot && LevelManager.gameOver == false) {
            GameObject bullet = Instantiate(weapon.projectile, transform.position, transform.rotation);
            weapon.nextShot = Time.time + weapon.fireRate;
            weapon.ammo++;
        }
    }

    private IEnumerator AbilityUsed() {
        yield return new WaitForSeconds(5);
        fireState = true;
        Fire();
        yield return new WaitForSeconds(15);
        fireState = false;
        aic.Cooldown(monkeyRest);
        Destroy(gameObject);
    }
}
