using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    
    private static WeaponManager instance;
    public static WeaponManager GetWeaponManager() { return instance; }

    private void Awake() => instance = this;

    public GameObject[] weapons;
    public GameObject[] bandolier;
    public GameObject[] wand;
}