using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private List<IStat> _stats = new List<IStat>();

    public bool autoFire;
    public List<IntStat> intStats;
    public List<FloatStat> floatStats;

    private void Start() {
        for (int i = 0; i < intStats.Count; i++) {
            intStats[i] = new IntStat(intStats[i].stat);
            _stats.Add(intStats[i]);
            Debug.Log(intStats[i].stat.name + ": "+ intStats[i].Value);
        }

        for (int i = 0; i < floatStats.Count; i++) {
            floatStats[i] = new FloatStat(floatStats[i].stat);
            _stats.Add(floatStats[i]);
            Debug.Log(floatStats[i].stat.name + ": " + floatStats[i].Value);
        }
        
    }

    public IStat GetStat(Stat statName) {
        return _stats.Find(s => s.Name == statName);
    }

    public int GetIntStat(Stat statName) {
        return intStats.Find(s => s.stat.name == statName).Value;
    }

    public float GetFloatStat(Stat statName) {
        return floatStats.Find(s => s.stat.name == statName).Value;
    }
}
