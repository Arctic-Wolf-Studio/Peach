using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class Enemy : ScriptableObject {
    public float speed;
    public float distance;

    public bool isDead;
    public bool isDetecting;
}