using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

[Serializable]
public abstract class Stat<T> where T : struct
{
    protected abstract T BaseValue { get; }
    protected abstract T MinValue { get; }
    protected abstract T MaxValue { get; }

    protected T value;
    
    public virtual T Value
    {
        get => value;
        set => this.value = Clamp(value);
    }

    protected abstract T Clamp(T newValue);
}

public abstract class IntStat : Stat<int>
{
    protected IntStat() {
        Value = BaseValue;
    }

    protected override int Clamp(int newValue) {
        return Math.Clamp(newValue, MinValue, MaxValue);
    }
}

public abstract class FloatStat : Stat<float>
{
    protected FloatStat() {
        Value = BaseValue;
    }

    protected override float Clamp(float newValue) {
        return Math.Clamp(newValue, MinValue, MaxValue);
    }
}

[Serializable]
public class HealthStat : IntStat
{
    public int value;
    public int minValue;
    public int maxValue;
    protected override int BaseValue => value;
    protected override int MinValue => minValue;
    protected override int MaxValue => maxValue;
}

[Serializable]
public class SpeedStat : FloatStat
{
    public float value;
    public float minValue;
    public float maxValue;
    protected override float BaseValue => value;
    protected override float MinValue => minValue;
    protected override float MaxValue => maxValue; 
}