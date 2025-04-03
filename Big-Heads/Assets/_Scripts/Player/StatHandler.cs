using System;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public IntStat health;
    public FloatStat speed;

    private void Start() {
        health = new IntStat(health.stat);
        speed = new FloatStat(speed.stat);
        
        Debug.Log(health.Value);
        Debug.Log(speed.Value);
    }
}