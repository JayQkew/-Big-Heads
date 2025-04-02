using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private int playerCount;
    [SerializeField] private Color[] playerColors;
    
    public Transform[] _spawnPoints;
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.name = "Player" + (playerCount + 1);
        playerInput.transform.SetParent(transform);

        playerInput.transform.position = _spawnPoints[playerCount].position;
        foreach (SpriteRenderer sr in playerInput.GetComponentsInChildren<SpriteRenderer>()) {
            sr.color = playerColors[playerCount];
        }

        playerInput.GetComponent<InputHandler>().gamepad = playerInput.GetDevice<Gamepad>();
        
        Debug.Log("Joined");
        playerCount++;
    }
}
