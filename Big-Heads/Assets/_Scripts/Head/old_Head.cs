using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;
using UnityEngine.SceneManagement;

public class old_Head : MonoBehaviour
{
    [Header("Health")]
    public int health;

    [SerializeField] private int maxHealth;

    [Header("Head")]
    [Space(10)]
    [SerializeField] private float maxSize;

    [SerializeField] private float minSize;

    [Space(10)]
    [SerializeField] private float radiusChangeSpeed;

    [Header("Thow")]
    [SerializeField] private float thowForceMult;

    private SoftBody softBody;
    private Player _player;

    private void Awake() {
        softBody = gameObject.GetComponentInChildren<SoftBody>();
        _player = GetComponentInParent<Player>();
    }

    private void Start() {
        minSize = softBody.radius;
    }

    public void Expand(InputAction.CallbackContext ctx) {
        if (ctx.performed) softBody.radius = maxSize;
        else if (ctx.canceled) softBody.radius = minSize;
    }

    public void Throw(InputAction.CallbackContext ctx) {
        if (ctx.performed && _player.headAttached && _player.aim != Vector2.zero) {
            softBody.AddForce(_player.aim * thowForceMult, ForceMode2D.Impulse);
            _player.headAttached = false;
        }
    }

    public void Damage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}