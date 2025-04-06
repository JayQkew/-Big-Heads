using System;
using UnityEngine;

public interface IModifier
{
    Stat Name { get; }
    Modifier Type { get; }
}

[Serializable]
public abstract class Modifier<T> : IModifier where T : struct
{
    public Stat Name { get; private set; }
    public Modifier Type { get; private set; }
    public T Amount { get; private set; }

    protected Modifier(Stat name, Modifier type, T amount) {
        Name = name;
        Type = type;
        Amount = amount;
    }

    public abstract T Apply(T amount);
}

[Serializable]
public class IntModifier : Modifier<int>
{
    public ModifierInt modifier;

    public IntModifier(ModifierInt modifierInt) : base(modifierInt.name, modifierInt.type, modifierInt.amount) {
        modifier.name = Name;
        modifier.type = Type;
        modifier.amount = Amount;
    }

    public override int Apply(int value) {
        if (Type == Modifier.Additive) {
            Debug.Log("Add Int");
            return value + modifier.amount;
        }
        Debug.Log("Multiply Int");
        return value * modifier.amount;
    }
}

[Serializable]
public class FloatModifier : Modifier<float>
{
    public ModifierFloat modifier;

    public FloatModifier(ModifierFloat modifierFloat) : base(modifierFloat.name, modifierFloat.type, modifierFloat.amount) {
        modifier.name = Name;
        modifier.type = Type;
        modifier.amount = Amount;
    }

    public override float Apply(float value) {
        if (Type == Modifier.Additive) {
            Debug.Log("Add Float");
            return value + modifier.amount;
        }
        Debug.Log("Multiply Float");
        return value * modifier.amount;
    }
}

[Serializable]
public struct ModifierInt
{
    public Stat name;
    public Modifier type;
    public int amount;
}

[Serializable]
public struct ModifierFloat
{
    public Stat name;
    public Modifier type;
    public float amount;
}

public enum Modifier
{
    Additive,
    Multiplicative
}