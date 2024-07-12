using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    //public GameObject explosion;
    protected Animator animator;
    protected float livindTime = 3f;

    protected Rigidbody2D rb;

    protected SpriteRenderer _renderer;

    public int damage = 1;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, livindTime);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Movement(); 
    }

    private void Movement()
    {
        Vector2 movement = direction.normalized * speed;

        rb.velocity = movement;
    }

    public void Explode() 
    {
        speed = 0f;
        _renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        Destroy(gameObject, 1.5f);
    }
}
