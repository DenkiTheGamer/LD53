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

    private Vector2 newPos;

    public float jumpSpeed;
    public float speed;
    public float airSpeed;

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
    private void FixedUpdate()
    {
        GroundCheck();
        ApplyDrag();
        Move();       
    }
    private void Start()
    {
        playerControls.Player.Jump.performed += Jump;
    }
    private void Move()
    {
        if (grounded)
        {
            Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
            move.x *= speed;
            rb2d.AddForce(move * Vector2.right, ForceMode2D.Force);
        }
        else
        {
            Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
            move.x *= speed * airSpeed;
            rb2d.AddForce(move * Vector2.right, ForceMode2D.Force);
        }
    }
    private void Jump(InputAction.CallbackContext cxt)
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
            rb2d.drag = 5;
        }
    }
}  
