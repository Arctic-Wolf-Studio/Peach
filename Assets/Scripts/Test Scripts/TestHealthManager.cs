using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthManager : MonoBehaviour {

    public TestHealthManager Instance;

    public event EventHandler OnDamaged;
    public event EventHandler OnDeath;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    public int Health { get { return health; } }


    void Awake() {
        Instance = this;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update() {

    }

    public void Damage(int damage) {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        Debug.Log("has completed");
        OnDamaged?.Invoke(this, EventArgs.Empty);
        Debug.Log(OnDamaged + "Y");

        if (HasDied()) {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HasDied() {
        return health == 0;
    }

    public int GetCurrentHealth() { 
        return health;
    }
}