using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HeadCardFactory", menuName = "Card Factories/ Head")]
public class HeadCardFactory : CardFactory
{
    public HeadCardType headCard;

    public override ICard CreateCard()
    {
        switch (headCard)
        {
            case HeadCardType.BigHead:
                return new BigHead();
            case HeadCardType.SmallHead:
                return new SmallHead();
        }
        
        return null;
    }
}

public enum HeadCardType
{
    BigHead,
    SmallHead
}

public class BigHead : ICard
{
    public void AddCard()
    {
        Debug.Log("BigHead *Added*");
    }

    public void RemoveCard()
    {
        Debug.Log("BigHead *Removed*");
    }
}

public class SmallHead : ICard
{
    public void AddCard()
    {
        Debug.Log("SmallHead *Added*");
    }

    public void RemoveCard()
    {
        Debug.Log("SmallHead *Removed*");
    }
}
