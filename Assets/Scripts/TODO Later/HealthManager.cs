using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public event EventHandler OnDamaged;
    public event EventHandler OnDeath;
    [SerializeField] private int maxHealth;
    private int health;
    

    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage) {
        health -= damage;

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (HasDied()) {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HasDied() {
        return health == 0;
    }
}
