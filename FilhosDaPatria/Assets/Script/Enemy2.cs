using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    [Header("Attack Properties")]
    public float timerWaitAttack;
    public float timerShootAttack;

    private bool idle;
    private bool shoot;

    private Weapon weapon;

    private bool die;
    private float horizontalVelocity;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    protected override void Update()
    {
        base.Update();

        if (//!RaycastGround().collider || 
            RaycastWall().collider)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if(CanAttack())
        {
            Attack();
        }
        else 
        {
            Movement();
        }

    }

    private void LateUpdate()
    {
        animator.SetBool("Idle", idle);
        animator.SetFloat("Run", Mathf.Abs(rb.velocity.x));
    }

    private void Movement()
    {
        horizontalVelocity = speed;
        horizontalVelocity = horizontalVelocity * direction;
        rb.velocity = new Vector2 (horizontalVelocity, rb.velocity.y);
        idle = false;
    }

    private bool CanAttack()
    {
        return ((int)Mathf.Abs(playerDistance)) <= attackDistance;
    }

    private void Attack()
    {
        StopMovement();
        DistanceFlipPlayer();
        CanShoot();
    }

    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
        idle = true;
    }

    private void DistanceFlipPlayer()
    {
        if(playerDistance >= 0 && direction == -1)
        {
            Flip();
        }
        else if (playerDistance < 0 && direction == 1)
        {
            Flip();
        }
    }

    private void CanShoot()
    {
        if (!shoot)
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        shoot = true;
        yield return new WaitForSeconds(timerWaitAttack);
        AnimationShoot();
        yield return new WaitForSeconds(timerShootAttack);
        shoot = false;
    }

    private void AnimationShoot()
    {
        animator.SetTrigger("Shoot");
    }

    private void ShootPrefab()
    {
        if(weapon != null)
        {
            weapon.Shoot();
        }
    }

    public void Die()
    {
        die = true;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");
    }

    private void OnDisabled()
    {
        Destroy(gameObject, 0.2f);
    }

}
