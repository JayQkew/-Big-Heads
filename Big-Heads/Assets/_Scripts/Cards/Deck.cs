using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> deck = new List<Card>();
    private List<IModifier> _totalModifiers = new List<IModifier>();

    public List<IntModifier> intModifiers = new List<IntModifier>();
    public List<FloatModifier> floatModifiers = new List<FloatModifier>();
    
    public void AddCard(Card card) {
        deck.Add(card);
        foreach (IModifier mod in card.modifiers) {
            _totalModifiers.Add(mod);
            if (mod.GetType() == typeof(IntModifier)) {
                intModifiers.Add((IntModifier)mod);
            }
            else if (mod.GetType() == typeof(FloatModifier)) {
                floatModifiers.Add((FloatModifier)mod);
            }
        }
    }
}