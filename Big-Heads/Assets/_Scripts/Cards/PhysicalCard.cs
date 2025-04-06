using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalCard : MonoBehaviour
{
    public Card card;

    private void Awake() {
        card.TransferModifiers();
    }
}

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
