using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    /*public Transform Airspace_One, Airspace_Two, Dirt, Dirt_00;
    public float scrollSpeed;

    private float bgWidth;
    private float bgWidth_Two;*/

    [SerializeField]
    private Vector2 parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    private Rigidbody2D princessRB;

    // Start is called before the first frame update
    void Start()
    {
        /*bgWidth = Airspace_One.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        bgWidth_Two = Dirt.GetComponent<SpriteRenderer>().sprite.bounds.size.x;*/
        princessRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*Airspace_One.position = new Vector3(Airspace_One.position.x - (scrollSpeed * Time.deltaTime), Airspace_One.position.y, Airspace_One.position.z);
        Airspace_Two.position = new Vector3(Airspace_Two.position.x - (scrollSpeed * Time.deltaTime), Airspace_Two.position.y, Airspace_Two.position.z);

        if(Airspace_One.position.x < -bgWidth -1)
        {
            Airspace_One.position += new Vector3(Mathf.Abs(bgWidth * 2), 0f, 0f);
        }
        if (Airspace_Two.position.x < -bgWidth -1)
        {
            Airspace_Two.position += new Vector3(bgWidth * 2, 0f, 0f);
        }*/

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x * princessRB.velocity.x, deltaMovement.y * parallaxEffectMultiplier.y * princessRB.velocity.y);
        lastCameraPosition = cameraTransform.position;
    }
}