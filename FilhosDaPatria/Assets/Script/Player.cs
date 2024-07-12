using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum GroundType
{
    None,
    Soft,
    Hard
}

public class Player : MonoBehaviour
{
    [Header("Moviment Properties")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Properties")]
    public LayerMask groundLayer;
    public float groundDistance;
    public bool isGrounded;
    public Vector3[] footOffset;

    [Header("Audio")]
    [SerializeField] AudioCharacter audioPlayer = null;

    private Vector2 movement;
    private float xVelocity;

    private int direction = 1;
    private float originalXScale;

    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;

    private bool isFire;

    private LayerMask softGround;
    private LayerMask hardGround;
    private GroundType groundType;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    private Weapon weapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        originalXScale = transform.localScale.x;

        softGround = LayerMask.GetMask("Ground");
        hardGround = LayerMask.GetMask("GroundHard");
        col = GetComponent<Collider2D>();

        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (isFire == false)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontal, 0);

            if (xVelocity * direction < 0)
            {
                Flip();
            }
        }

        PhysicsCheck();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Fire1") && isFire == false && isGrounded)
        {
            movement = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Fire");
        }
    }

    private void FixedUpdate()
    {
        if (isFire == false) 
        {
            xVelocity = movement.normalized.x * speed;
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        }

        UpdateGround();

        if(isGrounded == false)
            audioPlayer.PlaySterps(groundType, Mathf.Abs(xVelocity));
    }

    private void LateUpdate()
    {
        if (GameManager.isGameOver)
        {
            animator.SetTrigger("Die");
            Invoke("LoadGameOver", 1f);
            return;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(xVelocity));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);

        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Fire"))
        {
            isFire = true;
        }
        else 
        { 
            isFire = false; 
        }
    }

    public void Shoot()
    {
        if(weapon != null)
        {
            weapon.Shoot();
        }
    }

    private void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;

        scale.x = originalXScale * direction;
        transform.localScale = scale;
    }

    private void PhysicsCheck()
    {
        isGrounded = false;
        leftCheck = Raycast(new Vector2(footOffset[0].x , footOffset[0].y), 
            Vector2.down,groundDistance, groundLayer);
        rightCheck = Raycast(new Vector2(footOffset[1].x , footOffset[1].y),
            Vector2.down,groundDistance, groundLayer);

        if (leftCheck || rightCheck)
        {
            isGrounded = true;
        }
    }

    private void UpdateGround()
    {
        if (col.IsTouchingLayers(softGround))
            groundType = GroundType.Soft;
        else if (col.IsTouchingLayers(hardGround))
            groundType = GroundType.Hard;
        else
            groundType = GroundType.None;
    }

    private RaycastHit2D Raycast(Vector3 origin, Vector2 rayDirection, float Length, LayerMask mask)
    {
        Vector3 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + origin, rayDirection, Length, mask);

        //Color color = hit ? Color.red : Color.green;
        //Debug.DrawRay(pos + origin, rayDirection * Length, color);

        return hit;
    }

    void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
