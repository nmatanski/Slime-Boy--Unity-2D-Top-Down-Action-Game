using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    private Vector2 targetPosition;

    private Animator animator;

    private float summonTime;


    [SerializeField]
    private float timeBetweenSummons;

    [SerializeField]
    private Enemy enemyToSummon;

    [SerializeField]
    private float meleeAttackSpeed;

    [SerializeField]
    private float minX;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float minY;

    [SerializeField]
    private float maxY;


    public override void Start()
    {
        base.Start();
        var randomXY = new Dictionary<char, float>
        {
            { 'X', Random.Range(minX, maxX) },
            { 'Y', Random.Range(minY, maxY) }
        };
        targetPosition = new Vector2(randomXY['X'], randomXY['Y']);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //stopDistance = getStopDistance();

        if (Player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);

                if (Time.time > summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    animator.SetTrigger("summon");
                }
            }
            if (Vector2.Distance(transform.position, Player.position) < StopDistance && Time.time >= AttackTime)
            {
                AttackTime = Time.time + TimeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    public void Summon()
    {
        if (Player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }
    private IEnumerator Attack()
    {
        Player.GetComponent<Player>().DealDamage(Damage);

        var originalPosition = transform.position;
        var target = Player.position;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * meleeAttackSpeed;
            float interpolationParamter = 4 * (percent - Mathf.Pow(percent, 2)); // it gets points (values) from a parabole between 0 and 1
            transform.position = Vector2.Lerp(originalPosition, target, interpolationParamter);

            yield return null;
        }
    }
}
