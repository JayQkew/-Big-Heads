using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public StatInt health;
    public StatFloat speed;
}