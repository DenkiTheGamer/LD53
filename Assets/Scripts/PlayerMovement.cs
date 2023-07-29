using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb2d;

    public Transform raycastOrigin;

    public LayerMask ground;

    public float jumpSpeed;
    public float speed;
    public float airSpeed;

    public float moveX;

    public float groundDrag;

    [SerializeField]
    private bool grounded;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Start()
    {
        playerControls.Player.Jump.performed += Jump;
    }
    private void FixedUpdate()
    {
        GroundCheck();
        ApplyDrag();
        Move();       
    }   
    public void Move()
    {
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
            
        if (grounded)
        {          
            moveX = move.x;
            move.x *= speed;
            rb2d.AddForce(move * Vector2.right, ForceMode2D.Force);
            if (moveX == 0 && rb2d.velocity.x < 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (moveX == 0 && rb2d.velocity.x > 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (rb2d.velocity.x > 20)
            {
                rb2d.velocity = new Vector2(20, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -20)
            {
                rb2d.velocity = new Vector2(-20, rb2d.velocity.y);
            }
        }
        else
        {
            moveX = move.x;
            move.x *= speed * airSpeed;
            rb2d.AddForce(move * Vector2.right, ForceMode2D.Force);
            if(rb2d.velocity.x > 20)
            {
                rb2d.velocity = new Vector2(20, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -20)
            {
                rb2d.velocity = new Vector2(-20, rb2d.velocity.y);
            }
            if (moveX == 1 && rb2d.velocity.x < 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (moveX == -1 && rb2d.velocity.x > 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }
    public void Move(float move)
    {      
        if (grounded)
        {
            moveX = move;
            move *= speed;
            rb2d.AddForce(move * Vector2.right, ForceMode2D.Force);
            if (move == 0 && rb2d.velocity.x < 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (move == 0 && rb2d.velocity.x > 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (rb2d.velocity.x > 20)
            {
                rb2d.velocity = new Vector2(20, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -20)
            {
                rb2d.velocity = new Vector2(-20, rb2d.velocity.y);
            }
        }
        else
        {
            moveX = move;
            move *= speed * airSpeed;
            rb2d.AddForce(move * Vector2.right, ForceMode2D.Force);
            if (rb2d.velocity.x > 20)
            {
                rb2d.velocity = new Vector2(20, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -20)
            {
                rb2d.velocity = new Vector2(-20, rb2d.velocity.y);
            }
            if (moveX == 1 && rb2d.velocity.x < 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (moveX == -1 && rb2d.velocity.x > 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }
    public void Jump(InputAction.CallbackContext cxt)
    {
        if (grounded)
        {
            Vector2 jumpForce;
            jumpForce.x = 0;
            jumpForce.y = jumpSpeed;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }
    private void GroundCheck()
    {
        Collider2D[] checkHit = Physics2D.OverlapCircleAll(raycastOrigin.position, 0.1f, ground);
        for (int i = 0; i < checkHit.Length; i++)
        {
            if (checkHit[i].gameObject != gameObject)
            {
                grounded = true;
            }         
        }
        if(checkHit.Length == 0)
        {
            grounded = false;
        }
    }
    private void ApplyDrag()
    {
        if (!grounded)
        {
            rb2d.drag = 0;
        }
        else
        {
            rb2d.drag = groundDrag;
        }
    }
}  
