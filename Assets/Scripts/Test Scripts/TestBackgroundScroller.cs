using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBackgroundScroller : MonoBehaviour {

    private PrincessController princess;
    public Transform backgroundOne, backgroundTwo;
    public Vector2 acceleration, scrollSpeed, speed;

    private float backgroundWidth;

    private void OnEnable() {
        WeaponFire.OnMovement += AccelerateBackground;
    }
    private void OnDisable() {
        
    }

    private void Start() {
        princess = PrincessController.GetPrincessController();
        backgroundWidth = backgroundOne.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    private void Update() {
        GetPrincessSpeed(princess.rb.velocity);
        Scroll();
    }

    public void Scroll() {
        backgroundOne.position = new Vector3(backgroundOne.position.x - (scrollSpeed.x * speed.x * Time.deltaTime), backgroundOne.position.y, backgroundTwo.position.z);
        backgroundTwo.position -= new Vector3(scrollSpeed.x * speed.x * Time.deltaTime, 0f, 0f);

        if (backgroundOne.position.x < -backgroundWidth - 10)
            backgroundOne.position += new Vector3(backgroundWidth * 2f, 0f, 0f);

        if (backgroundTwo.position.x < -backgroundWidth - 10)
            backgroundTwo.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
    }

    private Vector2 GetPrincessSpeed(Vector2 velocity) { 
        return scrollSpeed = velocity;
    }
    private void AccelerateBackground(Vector2 acceleration) { 
        scrollSpeed *= acceleration;
    }
}