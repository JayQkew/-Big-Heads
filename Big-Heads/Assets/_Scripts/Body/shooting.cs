using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] private Player playerScript;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float adjust, ShootForce, fireRate;
    [SerializeField] private bool CanFire = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float degree = Mathf.Atan2(playerScript.aim.y, playerScript.aim.x) * Mathf.Rad2Deg;
        rb.rotation = degree + adjust;
        transform.localPosition = Vector2.zero;
    }


    public void ShotsFired()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(playerScript.aim * ShootForce, ForceMode2D.Impulse);
        
    }
}
