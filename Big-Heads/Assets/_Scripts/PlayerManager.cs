using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    public Material[] materials;
    private int playerCount;
    
    public String[] _layers;
    
    public Transform[] _spawnPoints;

    public GameObject[] playerHealth;

    private void Awake()
    {
        instance = this;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        GameObject softbody = playerInput.transform.GetChild(0).GetChild(0).gameObject;

        playerInput.gameObject.name = "Player" + (playerCount + 1);
        playerInput.transform.SetParent(transform);
        softbody.layer = LayerMask.NameToLayer(_layers[playerCount]);
        softbody.transform.position = Vector3.zero;
        softbody.GetComponent<SoftBody>().meshMaterial = materials[playerCount];
        
        playerInput.transform.position = _spawnPoints[playerCount].position;
        
        Debug.Log("Joined");
        playerCount++;
    }
}
