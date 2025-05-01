using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMove : MonoBehaviour {

    private PrincessController princess;
    private void Start() {
        princess = PrincessController.GetPrincessController();
    }

    private void Update() {
        MoveTower();
    }

    private void MoveTower() {
        transform.position = new Vector2(-princess.transform.position.x, transform.position.y);

    }

/*    private IEnumerator TowerMovement() { 
    
        yield return new WaitForSeconds(1);

        MoveTower();
    }*/
}