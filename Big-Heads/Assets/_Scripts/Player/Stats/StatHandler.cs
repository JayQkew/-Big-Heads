using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private List<IStat> _stats = new List<IStat>();

    public bool autoFire;
    public List<IntStat> intStats = new List<IntStat>();
    public List<FloatStat> floatStats = new List<FloatStat>();

    public List<IntModifier> intModifiers = new List<IntModifier>();
    public List<FloatModifier> floatModifiers = new List<FloatModifier>();

    private void Start() {
        for (int i = 0; i < intStats.Count; i++) {
            intStats[i] = new IntStat(intStats[i].stat);
            _stats.Add(intStats[i]);
            for (int j = 0; j < intModifiers.Count; j++) {
                intModifiers[j] = new IntModifier(intModifiers[j].modifier);
                if (intModifiers[j].Name == intStats[i].stat.name) {
                    intStats[i].ApplyModifier(intModifiers[j]);
                }
            }

            Debug.Log(intStats[i].stat.name + ": " + intStats[i].Value);
        }

        for (int i = 0; i < floatStats.Count; i++) {
            floatStats[i] = new FloatStat(floatStats[i].stat);
            _stats.Add(floatStats[i]); 
            
            for (int j = 0; j < floatModifiers.Count; j++) {
                floatModifiers[j] = new FloatModifier(floatModifiers[j].modifier);
                if (floatModifiers[j].Name == floatStats[i].stat.name) {
                    floatStats[i].ApplyModifier(floatModifiers[j]);
                }
            }

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