using System.Collections.Generic;
using UnityEngine;

public class new_Player : MonoBehaviour
{
    public bool headAttached = true;
    
    [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    
    public List<ICard> deck = new List<ICard>();
}
