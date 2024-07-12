using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    public float speed;
    public float attackDistance;
    public int direction;

    [Header("RayCast Properties")]
    public LayerMask layerGround;
    public float lengthGround;
    public float lengthWall;
    public Transform rayPointGround;
    public Transform rayPointWall;
    public RaycastHit2D hitGround;
    public RaycastHit2D hitWall;

    protected Animator animator;
    protected Rigidbody2D rb;

    protected Transform player;
    protected float playerDistance;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        direction = (int)transform.localScale.x;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetDistancePlayer();
    }

    protected virtual void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }
    
    //protected virtual RaycastHit2D RaycastGround()
    //{
    //    hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lengthGround, layerGround);

    //    Color color = hitGround ? Color.red : Color.green;

    //    Debug.DrawRay(rayPointGround.position, Vector2.down * lengthGround, color);

    //    return hitGround;
    //}

    protected virtual RaycastHit2D RaycastWall()
    {
        hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right * direction, lengthWall, layerGround);

        //Color color = hitWall ? Color.yellow : Color.blue;

        //Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lengthWall, color);

        return hitWall;
    }
    
    protected void GetDistancePlayer()
    {
        playerDistance = player.position.x - transform.position.x;
    }

    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnDestroyed;

    void OnDestroy()
    {
        // Notifica que este inimigo foi destruído
        if (OnDestroyed != null)
        {
            OnDestroyed.Invoke();
        }
    }

    // Exemplo de função que destrói o inimigo (pode ser chamada quando o inimigo morre)
    public void Die()
    {
        Destroy(gameObject);
    }
    

}
