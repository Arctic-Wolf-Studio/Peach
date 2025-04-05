using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    private PrincessController princess;

    [SerializeField] private Vector2 offset;

    private void Start() {
        princess = PrincessController.GetPrincessController();
    }

    private void Update() {
        MoveBackground();
    }

    private void MoveBackground() {
        transform.position = new Vector2(princess.transform.position.x + offset.x, transform.position.y);
    }
}