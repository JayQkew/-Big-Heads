using UnityEngine;

public class PhysicalCard : MonoBehaviour
{
    public Card card;

    private void Awake() {
        card.TransferModifiers();
    }
}