using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentDress : MonoBehaviour { 
    public SystemUpdate update;
    private dressManager manager;

    //public GameObject weapon;
    public Transform princessPosition;
    public Transform princesss;
    private GameObject previousDress;

    private int selectedIndex;
    void Start() {
    manager = GameObject.Find("dress manager").GetComponent<dressManager>();
    GameObject weapon = manager.dresses[update.princess.CurrentDress];
    SelectDress();
}

    public void SelectDress() {
    int dressIndex = update.princess.CurrentDress;

    if (dressIndex != selectedIndex) {
        previousDress = GameObject.FindGameObjectWithTag("Dress");
        GameObject tempDress = manager.dresses[dressIndex];
        Instantiate(tempDress, princessPosition.position, Quaternion.identity, princesss);
        selectedIndex = dressIndex;
    }
}
}
