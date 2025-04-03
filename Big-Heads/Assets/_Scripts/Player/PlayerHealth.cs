using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private StatModifiers _statModifiers;
    
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    
    public UnityEvent onDeath;

    private void Awake() {
        _statModifiers = GetComponent<StatModifiers>();
    }

    private void Start() {
        health = maxHealth * _statModifiers.HealthMult;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            onDeath?.Invoke();
        }
    }

    public void Heal(float heal) {
        health += heal;
        if (health > maxHealth * _statModifiers.HealthMult) {
            health = maxHealth * _statModifiers.HealthMult;
        }
    }
}
