using System;
using System.Collections.Generic;
using UnityEngine;

public interface IStat
{
    void ApplyModifier(IStatModifier modifier);
}

public interface IStatModifier{}

[Serializable]
public abstract class Stat<T> where T : struct, IComparable<T>
{
    public T BaseValue { get; private set; }
    public T MinValue { get; private set; }
    public T MaxValue { get; private set; }
    
    private T _value;

    public virtual T Value
    {
        get => _value;
        set => _value = Clamp(value);
    }

    protected Stat(T baseValue, T minValue, T maxValue)
    {
        BaseValue = baseValue;
        MinValue = minValue;
        MaxValue = maxValue;
        Value = baseValue; // Initialize value properly
    }

    protected virtual T Clamp(T newValue)
    {
        if (Comparer<T>.Default.Compare(newValue, MinValue) < 0) return MinValue;
        if (Comparer<T>.Default.Compare(newValue, MaxValue) > 0) return MaxValue;
        return newValue;
    }
}

[Serializable]
public class IntStat : Stat<int> , IStat
{
    public StatInt stat;

    public override int Value
    {
        get => stat.val;
        set
        {
            stat.val = Clamp(value);
        }
    }
    public IntStat(StatInt statInt) : base(statInt.val, statInt.min, statInt.max) {
        stat.val = Value;
        stat.min = MinValue;
        stat.max = MaxValue;
    }

    public void ApplyModifier(IStatModifier modifier) {
        throw new NotImplementedException();
    }
}

[Serializable]
public class FloatStat : Stat<float> , IStat
{
    public StatFloat stat;

    public override float Value
    {
        get => stat.val;
        set
        {
            stat.val = Clamp(value);
        }
    }
    public FloatStat(StatFloat statFloat) : base(statFloat.val, statFloat.min, statFloat.max) {
        stat.val = Value;
        stat.min = MinValue;
        stat.max = MaxValue;
    }

    public void ApplyModifier(IStatModifier modifier) {
        throw new NotImplementedException();
    }
}

[Serializable]
public struct StatInt
{
    public int val;
    [Space(10)]
    public int min;
    public int max;
}

[Serializable]
public struct StatFloat
{
    public float val;
    [Space(10)]
    public float min;
    public float max;
}

public enum Stat
{
    
}