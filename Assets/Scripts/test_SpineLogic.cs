using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_SpineLogic : MonoBehaviour {
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I)) { PrincessUpdate.Instance.isIdle = true; }
    }
}