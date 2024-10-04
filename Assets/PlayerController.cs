using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    [SerializeField] int speed = 0;
    private int point = 0; 
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // rb.AddForce(new Vector2(0,100));
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("Ground");
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("point"))
        {
            point++;
            Debug.Log("Point: " + point);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // rb.velocity = movementVector;
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        Debug.Log(movementVector);
    }

    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0,200));
            Debug.Log("jump");
            isGrounded = false;
        }
    }
}
