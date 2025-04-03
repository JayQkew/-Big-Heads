using System;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public HealthStat health;
    public SpeedStat speed;

    private void Start() {
        health = playerStats.health;
        speed = playerStats.speed;
        
        Debug.Log(health.Value);
        Debug.Log(speed.Value);
    }
}