using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BodyMovement : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private float moveSpeed = 5f, jumpForce = 100f, rayDist = 0.5f, OffsetX, OffsetY;
   [SerializeField] private float gravity = 9.81f;
   [SerializeField] private bool isGrounded, canJump, hitLeft, hitRight;
   private Vector2 direction, Movement;
   [SerializeField] private InputActionAsset actionAsset;
   
   [SerializeField] private int theLayer = 3;
   private int targetLayerJump;
   

   void Start()
   {
       rb.gravityScale = gravity;
       targetLayerJump = 1 << theLayer;
   }

    // Update is called once per frame
    void Update()
    { 
        Movement = new Vector2(direction.x* moveSpeed, -rb.gravityScale);
        
        Vector2 BottomLeft = new Vector2(transform.position.x - OffsetX, transform.position.y- OffsetY);
        Vector2 BottomRight = new Vector2(transform.position.x + OffsetX, transform.position.y  - OffsetY);
       
        Debug.DrawRay(BottomLeft, Vector2.down * rayDist, Color.magenta);
        Debug.DrawRay(BottomRight, Vector2.down * rayDist, Color.magenta);
        hitLeft = Physics2D.Raycast(BottomLeft, Vector2.down, rayDist, targetLayerJump);
        hitRight = Physics2D.Raycast(BottomRight, Vector2.down, rayDist, targetLayerJump);
        if (hitLeft || hitRight)
        {
            canJump = true;
            isGrounded = true;
            rb.gravityScale = 0f;
        }
        else if (!hitLeft && !hitRight)
        {
            canJump = false;
            isGrounded = false;
            rb.gravityScale = gravity;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement* Time.deltaTime);
    }

    public void MoveCube(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 PlayerInput = context.ReadValue<Vector2>();
            direction.x = PlayerInput.x;
            direction.y = PlayerInput.y;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
           Debug.Log("Hello");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
