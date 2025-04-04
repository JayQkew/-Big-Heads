using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerActions : MonoBehaviour
{
    private InputHandler _inputHandler;
    private StatHandler _statHandler;
    private Movement _movement;

    public bool headAttached = true;

    [Space(10)]
    [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    private Head _head;
    
    [Header("Teleport")]
    [SerializeField] private float currTeleportTime;

    [Header("Shooting")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    public PhysicsMaterial2D bulletBounceMat;

    [Space(10)]
    [SerializeField] private int currAmmo;

    [Space(10)]
    [SerializeField] private float currFireRate;

    private void Awake() {
        _inputHandler = GetComponent<InputHandler>();
        _statHandler = GetComponent<StatHandler>();
        _movement = GetComponent<Movement>();
        _head = head.GetComponent<Head>();
    }

    private void OnEnable() {
        _statHandler = GetComponent<StatHandler>();
    }

    private void Start() {
        currAmmo = _statHandler.GetIntStat(Stat.Ammo);
        currFireRate = _statHandler.GetFloatStat(Stat.FireRate);

        if (_statHandler.autoFire) {
            _inputHandler.onShoot.AddListener(Shoot);
            _inputHandler.onShootStart.RemoveListener(Shoot);
        }
        else {
            _inputHandler.onShootStart.AddListener(Shoot);
            _inputHandler.onShoot.RemoveListener(Shoot);
        }
    }

    private void Update() {
        GunDirection();
        currTeleportTime += Time.deltaTime;
        currFireRate += Time.deltaTime;
    }

    public void TeleportThrow() {
        if (headAttached) Throw();
        else Teleport();
    }

    private void Teleport() {
        if (currTeleportTime >= _statHandler.GetFloatStat(Stat.Teleport)) {
            body.position = head.position;
            head.position = body.position + Vector3.up;
            _movement._rb.linearVelocity = head.GetComponent<Rigidbody2D>().linearVelocity;

            _head.AttachHead(true);
            headAttached = true;
            currTeleportTime = 0;
            currAmmo = _statHandler.GetIntStat(Stat.Ammo);
        }
    }

    private void Throw() {
        Vector2 force = _inputHandler.aim * _statHandler.GetFloatStat(Stat.Throw);
        _head.rb.linearVelocity = Vector2.zero;
        _head.rb.AddForce(force, ForceMode2D.Impulse);

        _head.AttachHead(false);
        headAttached = false;
    }

    public void Shoot() {
        if (currAmmo > 0 && currFireRate >= _statHandler.GetFloatStat(Stat.FireRate)) {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetModifiers(
                _statHandler.GetIntStat(Stat.BulletBounces),
                _statHandler.GetFloatStat(Stat.BulletSize),
                _statHandler.GetFloatStat(Stat.BulletMass),
                _statHandler.GetFloatStat(Stat.BulletGravity),
                _statHandler.GetFloatStat(Stat.Damage));
            bullet.GetComponent<Bullet>().rb.AddForce(_inputHandler.aim * _statHandler.GetFloatStat(Stat.FireForce),
                ForceMode2D.Impulse);
            bullet.GetComponent<CircleCollider2D>().sharedMaterial = bulletBounceMat;
            bullet.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = _statHandler.GetFloatStat(Stat.BulletBounciness);
            currAmmo--;
            currFireRate = 0;
        }
    }

    private void GunDirection() {
        float deg = Mathf.Atan2(_inputHandler.aim.y, _inputHandler.aim.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, deg - 90);
    }
}