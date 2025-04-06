using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    private int playerCount;
    [FormerlySerializedAs("playerColors")] [SerializeField] private Material[] playerMaterials;
    [SerializeField] private PhysicsMaterial2D[] bulletBounceMats;

    public Transform[] _spawnPoints;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.name = "Player" + (playerCount + 1);
        playerInput.transform.SetParent(transform);

        playerInput.transform.position = _spawnPoints[playerCount].position;
        foreach (SpriteRenderer sr in playerInput.GetComponentsInChildren<SpriteRenderer>()) {
            sr.material = playerMaterials[playerCount];
        }

        playerInput.GetComponent<InputHandler>().gamepad = playerInput.GetDevice<Gamepad>();
        playerInput.GetComponent<PlayerActions>().bulletBounceMat = bulletBounceMats[playerCount];
        
        Debug.Log("Joined");
        playerCount++;
    }
}
