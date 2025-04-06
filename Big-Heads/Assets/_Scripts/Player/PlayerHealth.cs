using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    private StatHandler _statHandler;
    
    [SerializeField] private float currHealth;
    
    public UnityEvent onDeath;

    private void Awake() {
        _statHandler = GetComponent<StatHandler>();
    }

    private void Start() {
        currHealth = _statHandler.IntStatValue(Stat.Health);
    }

    public void TakeDamage(float damage) {
        currHealth -= damage;
        if (currHealth <= 0) {
            onDeath?.Invoke();
        }
    }

    public void Heal(float heal) {
        currHealth += heal;
        if (currHealth > _statHandler.IntStatValue(Stat.Health)) {
            currHealth = _statHandler.IntStatValue(Stat.Health);
        }
    }
}
