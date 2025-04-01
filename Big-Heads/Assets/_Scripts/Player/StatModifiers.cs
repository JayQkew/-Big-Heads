using UnityEngine;

public class StatModifiers : MonoBehaviour
{
    [Header("Body Multipliers")]
    [SerializeField] private float speedMult;
    [SerializeField] private float jumpMult;

    [Header("Head Multipliers")] 
    [SerializeField] private float teleportReloadMult;
    [SerializeField] private float throwForceMult;
    [SerializeField] private float headBounceMult;
    [SerializeField] private float headSizeMult;
    [SerializeField] private float headWeightMult;

    [Header("Gun Multipliers")] 
    [SerializeField] private bool autoFire;
    [SerializeField] private float extraAmmo;
    [SerializeField] private float fireRateMult;
    [SerializeField] private float fireForceMult;

    [Header("Bullet Multipliers")]
    [SerializeField] private int extraBounces;
    [SerializeField] private float bulletSizeMult;
    
    //Getters & Setters
    public float SpeedMult
    {
        get => speedMult;
        set
        {
            if (value < 0)
            {
                Debug.Log("SpeedMult cannot < 0");
                speedMult = 0;
                return;
            }
            speedMult = value;
        }
    }

    public float JumpMult
    {
        get => jumpMult;
        set
        { 
            if (value < 0)
            {
                Debug.Log("JumpMult cannot be < 0");
                jumpMult = 0;
                return;
            }
            jumpMult = value;
        }
    }

    public float TeleportReloadMult
    {
        get => teleportReloadMult;
        set
        {
            if (value < 0)
            {
                Debug.Log("TeleportReloadMult cannot be < 0");
                teleportReloadMult = 0;
                return;
            }
            teleportReloadMult = value;
        }
    }

    //can go negative, player will just have to aim opposite
    public float ThrowForceMult
    {
        get => throwForceMult;
        set => throwForceMult = value;
    }

    public float HeadBounceMult
    {
        get => headBounceMult;
        set
        {
            if (value < 1)
            {
                Debug.Log("HeadBounceMult cannot be < 1");
                headBounceMult = 1;
                return;
            }
            headBounceMult = value;
        }
    }

    public float HeadSizeMult
    {
        get => headSizeMult;
        set
        {
            if (value < 0.25f)
            {
                Debug.Log("HeadSizeMult cannot be < 0.25");
                headSizeMult = 0.25f;
                return;
            }
            headSizeMult = value;
        }
    }

    //can be negative, so the head floats up
    public float HeadWeightMult
    {
        get => headWeightMult;
        set => headWeightMult = value;
    }

    public bool AutoFire
    {
        get => autoFire;
        set => autoFire = value;
    }

    //can be negative, but cannot be less than 1
    public float ExtraAmmo
    {
        get => extraAmmo;
        set
        {
            if (value < -6)
            {
                Debug.Log("ExtraAmmo cannot be < -6");
                extraAmmo = -6;
                return;
            }
            extraAmmo = value;
        }
    }

    public float FireRateMult
    {
        get => fireRateMult;
        set
        {
            if (value < 0)
            {
                Debug.Log("FireRateMult cannot be < 0");
                fireRateMult = 0;
                return;
            }
            fireRateMult = value;
        }
    }

    //can be negative, player will just have to aim opposite
    public float FireForceMult
    {
        get => fireForceMult;
        set => fireForceMult = value;
    }

    public int ExtraBounces
    {
        get => extraBounces;
        set
        {
            if (value < 0)
            {
                Debug.Log("ExtraBounces cannot be < 0");
                extraBounces = 0;
                return;
            }
            extraBounces = value;
        }
    }

    public float BulletSizeMult
    {
        get => bulletSizeMult;
        set
        {
            if (value < 0.25f)
            {
                Debug.Log("BulletSizeMult cannot be < 0.25");
                bulletSizeMult = 0.25f;
                return;
            }
            bulletSizeMult = value;
        }
    }
}
