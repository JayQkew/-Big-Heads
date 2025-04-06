using System;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Header("Deck")]
    public List<Card> deck = new List<Card>();

    [Header("Stats & Modifiers")]
    public bool autoFire;

    [Space(15)]
    private List<IStat> _stats = new List<IStat>();

    public List<IntStat> intStats = new List<IntStat>();
    public List<FloatStat> floatStats = new List<FloatStat>();

    [Space(15)]
    public List<IntModifier> intModifiers = new List<IntModifier>();

    public List<FloatModifier> floatModifiers = new List<FloatModifier>();

    [Header("Testing")]
    public Card card1;

    public Card card2;
    public Card card3;

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

    public void AddCard(Card card) {
        deck.Add(card);
        card.TransferModifiers();
        foreach (IModifier mod in card.modifiers) {
            if (mod is IntModifier intMod) {
                intModifiers.Add(intMod);
                GetIntStat(intMod.Name).ApplyModifier(intMod);
                Debug.Log(intModifiers.Count);
            }
            else if (mod is FloatModifier floatMod) {
                floatModifiers.Add(floatMod);
                GetFloatStat(floatMod.Name).ApplyModifier(floatMod);
                Debug.Log(floatModifiers.Count);
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            AddCard(card1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            AddCard(card2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            AddCard(card3);
        }
    }

    public IStat GetStat(Stat statName) {
        return _stats.Find(s => s.Name == statName);
    }

    public int IntStatValue(Stat statName) {
        return intStats.Find(s => s.stat.name == statName).Value;
    }

    public IntStat GetIntStat(Stat statName) {
        return intStats.Find(s => s.Name == statName);
    }

    public float FloatStatValue(Stat statName) {
        return floatStats.Find(s => s.stat.name == statName).Value;
    }

    public FloatStat GetFloatStat(Stat statName) {
        return floatStats.Find(s => s.Name == statName);
    }
}