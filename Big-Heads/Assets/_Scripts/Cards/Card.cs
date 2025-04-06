using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    public string name;
    public List<IModifier> modifiers = new List<IModifier>();
    
    [SerializeField] private List<IntModifier> intModifiers = new List<IntModifier>();
    [SerializeField] private List<FloatModifier> floatModifiers = new List<FloatModifier>();

    public void TransferModifiers() {
        modifiers.Clear();
        
        foreach (IntModifier mod in intModifiers) {
            modifiers.Add(mod);
        }

        foreach (FloatModifier mod in floatModifiers) {
            modifiers.Add(mod);
        }
    }
}