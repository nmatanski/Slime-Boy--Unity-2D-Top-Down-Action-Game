using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    private Animator animator;


    [SerializeField]
    private Transform shotPoint;

    [SerializeField]
    private GameObject enemyBullet;


    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //stopDistance = getStopDistance();

        if (Player != null)
        {
            if (Vector2.Distance(transform.position, Player.position) > StopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
            }

            if (Time.time >= AttackTime)
            {
                AttackTime = Time.time + TimeBetweenAttacks;
                animator.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        var direction = Player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
