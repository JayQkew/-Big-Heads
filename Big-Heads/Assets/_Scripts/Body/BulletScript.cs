using UnityEngine;

public class BulletScript : MonoBehaviour
{
   [SerializeField] private int Damage = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<IDamageable>() != null)
        {
            collision.transform.GetComponent<IDamageable>().Damage(Damage);
            Debug.Log("sdlkfalsdf");
        }
        
        Destroy(gameObject);
    }
}
