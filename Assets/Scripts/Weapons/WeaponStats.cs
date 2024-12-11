using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour {
    
    public Transform barrel;
    public GameObject projectile;

    public int ammo;
    public float fireRate;
    public int magazine;
    public float reload;
    public float speed;
    public float punch;

    [HideInInspector] public float nextShot;
}