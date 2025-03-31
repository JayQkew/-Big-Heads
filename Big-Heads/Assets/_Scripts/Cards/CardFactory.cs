using UnityEngine;

public abstract class CardFactory : ScriptableObject
{
    public abstract ICard CreateCard();
}

public interface ICard
{
    /// <summary>
    /// when a card is added to the players deck
    /// </summary>
    void AddCard();
    
    /// <summary>
    /// when a card is removed from the players deck
    /// </summary>
    void RemoveCard();
}
