using System;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public List<IStat> stats;
    
    public IntStat health;
    public FloatStat speed;
    

    private void Start() {
        health =  new IntStat(health.stat);
        stats.Add(health);
        speed = new FloatStat(speed.stat);

        health.Value += 11;
        
        Debug.Log(health.Value);
        Debug.Log(speed.Value);
    }
}