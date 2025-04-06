using System;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public List<IModifier> modifiers = new List<IModifier>();
    
    [SerializeField] private List<IntModifier> intModifiers = new List<IntModifier>();
    [SerializeField] private List<FloatModifier> floatModifiers = new List<FloatModifier>();

    private void Awake() {
        TransferModifiers();
    }

    private void TransferModifiers() {
        modifiers.Clear();
        
        foreach (IntModifier mod in intModifiers) {
            modifiers.Add(mod);
        }

        foreach (FloatModifier mod in floatModifiers) {
            modifiers.Add(mod);
        }
    }
}
