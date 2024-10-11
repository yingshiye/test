using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private int point = 0; 
    private bool isGrounded = false;
    private bool isSpeedUp = false; 
    private int score = 0;
    private SpriteRenderer sr; 

    // [SerializeField] allows you to modify the value of speed in the Unity Editor
    [SerializeField] Animator animator;
    [SerializeField] int speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
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
            isGrounded = true;
            Debug.Log("Point: " + point);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // rb.velocity = movementVector;
        rb.velocity = new Vector2(speed * movementVector.x, speed * movementVector.y);
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);

        animator.SetBool("Walk_Right", !Mathf.Approximately(movementVector.x, 0));
        if(!Mathf.Approximately(movementVector.x, 0))
        {
            sr.flipX = movementVector.x < 0;
        }
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

    void OnChangeSpeed(InputValue value)
    {
        if (isSpeedUp == false)
        {
            speed = 15; 
            isSpeedUp = true;
        }
        else
        {
            speed = 5;
            isSpeedUp = false;
        }
        Debug.Log(speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("point")){
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("My score is: " + score);
        }
    }


}
