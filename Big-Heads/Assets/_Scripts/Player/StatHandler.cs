using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private List<IStat> _stats = new List<IStat>();

    public List<IntStat> intStats;
    public List<FloatStat> floatStats;

    private void Start() {
        for (int i = 0; i < intStats.Count; i++) {
            intStats[i] = new IntStat(intStats[i].stat);
            _stats.Add(intStats[i]);
            Debug.Log(intStats[i].stat.stat + ": "+ intStats[i].Value);
        }

        for (int i = 0; i < floatStats.Count; i++) {
            floatStats[i] = new FloatStat(floatStats[i].stat);
            _stats.Add(floatStats[i]);
            Debug.Log(floatStats[i].stat.stat + ": " + floatStats[i].Value);
        }
        
    }

    public IStat GetStat(Stat stat) {
        return _stats.Find(s => s.Name == stat);
    }
}
