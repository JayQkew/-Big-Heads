using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Stat<T> where T : struct
{
    protected abstract string Name { get; }
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
    public IntStat() {
        value = BaseValue;
    }

    protected override int Clamp(int newValue) {
        return Math.Clamp(newValue, MinValue, MaxValue);
    }
}

public abstract class FloatStat : Stat<float>
{
    public FloatStat() {
        value = BaseValue;
    }

    protected override float Clamp(float newValue) {
        return Math.Clamp(newValue, MinValue, MaxValue);
    }
}

[Serializable]
public class HealthStat : IntStat
{
    [SerializeField] private int value;
    
    protected override string Name => "Health";
    protected override int BaseValue => 100;
    protected override int MinValue => 0;
    protected override int MaxValue => 200;
}

[Serializable]
public class SpeedStat : FloatStat
{
    [SerializeField] private float value;
    
    protected override string Name => "Speed";
    protected override float BaseValue => 10f;
    protected override float MinValue => 0f;
    protected override float MaxValue => 20f; 
}