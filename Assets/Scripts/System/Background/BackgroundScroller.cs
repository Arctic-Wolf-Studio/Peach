using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    private PrincessController princess;
    
    [SerializeField] private Renderer bg;

    [SerializeField] private Vector2 speed;

    [SerializeField] private float distanceSpeed;

    private void Start() {
        princess = PrincessController.GetPrincessController();
    }

    private void Update() {
        speed = princess.rb.velocity;
        if (speed.x > 0f) {
            MoveBackground();
        }
        
    }

    private void MoveBackground() {

        float movement = speed.x * distanceSpeed;

        bg.material.mainTextureOffset += new Vector2(movement * Time.deltaTime, 0f);
    }
}