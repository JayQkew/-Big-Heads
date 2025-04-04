using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public interface IStat
{
    Stat Name { get; }
}

[Serializable]
public abstract class Stat<T> : IStat where T : struct, IComparable<T>
{
    public Stat Name { get; private set; }
    public abstract void ApplyModifier(Modifier<T> modifier);

    public T BaseValue { get; private set; }
    public T MinValue { get; private set; }
    public T MaxValue { get; private set; }

    private T _value;

    public virtual T Value
    {
        get => _value;
        set => _value = Clamp(value);
    }

    protected Stat(T baseValue, T minValue, T maxValue, Stat name) {
        BaseValue = baseValue;
        MinValue = minValue;
        MaxValue = maxValue;
        Value = baseValue; // Initialize value properly
        Name = name;
    }

    protected virtual T Clamp(T newValue) {
        if (Comparer<T>.Default.Compare(newValue, MinValue) < 0) return MinValue;
        if (Comparer<T>.Default.Compare(newValue, MaxValue) > 0) return MaxValue;
        return newValue;
    }
}

[Serializable]
public class IntStat : Stat<int>
{
    public StatInt stat;

    public override void ApplyModifier(Modifier<int> modifier) {
        throw new NotImplementedException();
    }

    public override int Value
    {
        get => stat.val;
        set { stat.val = Clamp(value); }
    }

    public IntStat(StatInt statInt) : base(statInt.val, statInt.min, statInt.max, statInt.name) {
        stat.val = Value;
        stat.min = MinValue;
        stat.max = MaxValue;
        stat.name = Name;
    }
}

[Serializable]
public class FloatStat : Stat<float>
{
    public StatFloat stat;

    public override void ApplyModifier(Modifier<float> modifier) {
        throw new NotImplementedException();
    }

    public override float Value
    {
        get => stat.val;
        set { stat.val = Clamp(value); }
    }

    public FloatStat(StatFloat statFloat) : base(statFloat.val, statFloat.min, statFloat.max, statFloat.name) {
        stat.val = Value;
        stat.min = MinValue;
        stat.max = MaxValue;
        stat.name = Name;
    }
    
}

[Serializable]
public struct StatInt
{
    public Stat name;
    public int val;

    [Space(10)]
    public int min;

    public int max;
}

[Serializable]
public struct StatFloat
{
    public Stat name;
    public float val;

    [Space(10)]
    public float min;

    public float max;
}

public enum Stat
{
    Health,
    Damage,
    Speed,
    Acceleration,
    Jump,
    Teleport,
    Throw,
    HeadBounce,
    HeadSize,
    HeadMass,
    HeadGravity,
    Ammo,
    FireRate,
    FireForce,
    BulletBounces,
    BulletBounciness,
    BulletSize,
    BulletMass,
    BulletGravity
}