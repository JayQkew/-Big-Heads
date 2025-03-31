using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BodyCardFactory", menuName = "Card Factories/ Body")]
public class BodyCardFactory : CardFactory
{
    public BodyCardType bodyCard;

    public override ICard CreateCard()
    {
        switch (bodyCard)
        {
            case BodyCardType.SpeedBoost:
                return new SpeedBoost();
            case BodyCardType.SpeedReduction:
                return new SpeedReduction();
        }
        
        return null;
    }
}

public enum BodyCardType
{
    SpeedBoost,
    SpeedReduction
}

public class SpeedBoost : ICard
{
    public void AddCard()
    {
        Debug.Log("SpeedBoost *Added*");
    }

    public void RemoveCard()
    {
        Debug.Log("SpeedBoost *Removed*");
    }
}

public class SpeedReduction : ICard
{
    public void AddCard()
    {
        Debug.Log("SpeedReduction *Added*");
    }

    public void RemoveCard()
    {
        Debug.Log("SpeedReduction *Removed*");
    }
}