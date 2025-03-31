using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunCardFactory", menuName = "Card Factories/ Gun")]
public class GunCardFactory : CardFactory
{
    public GunCardType gunCard;
    public override ICard CreateCard()
    {
        switch (gunCard)
        {
            case GunCardType.BigGun:
                return new BigGun();
            case GunCardType.SmallGun:
                return new SmallGun();
        }
        return null;
    }
}

public enum GunCardType
{
    BigGun,
    SmallGun
}

public class BigGun : ICard
{
    public void AddCard()
    {
        Debug.Log("BigGun *Added*");
    }

    public void RemoveCard()
    {
        Debug.Log("BigGun *Removed*");
    }
}

public class SmallGun : ICard
{
    public void AddCard()
    {
        Debug.Log("SmallGun *Added*");
    }

    public void RemoveCard()
    {
        Debug.Log("SmallGun *Removed*");
    }
}


