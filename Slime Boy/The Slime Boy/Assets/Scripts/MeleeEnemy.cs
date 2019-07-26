using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float attackTime;

    private float stopDistance;

    [SerializeField]
    private float stopDistanceMin;

    [SerializeField]
    private float stopDistanceMax;

    [SerializeField]
    private float attackSpeed;


    // Update is called once per frame
    private void Update()
    {
        stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);
        if (stopDistance > 6)
        {
            stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);
        }
        if (Player != null)
        {
            if (Vector2.Distance(transform.position, Player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + TimeBetweenAttacks;
                }
            }
        }
    }

    private IEnumerator Attack()
    {
        Player.GetComponent<Player>().TakeDamage(Damage);

        var originalPosition = transform.position;
        var targetPosition = Player.position;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolationParamter = 4 * (percent - Mathf.Pow(percent, 2)); // it gets points (values) from a parabole between 0 and 1
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolationParamter);

            yield return null;
        }
    }
}
