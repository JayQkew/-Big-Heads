using System;
using UnityEngine;

[Serializable]
public class HealthStat : IntStat
{
    public HealthStat(StatInt statInt) : base(statInt) {}
}

[Serializable]
public class SpeedStat : FloatStat
{
    public SpeedStat(StatFloat statFloat) : base(statFloat) {}
}