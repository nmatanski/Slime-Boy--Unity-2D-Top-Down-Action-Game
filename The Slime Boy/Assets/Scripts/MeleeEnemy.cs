using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private bool isFirstTime = true;


    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private Ghost ghost;


    // Update is called once per frame
    private void Update()
    {
        if (isFirstTime)
        {
            if (ghost != null) ///TODO: temp solution, the real solution is to change the summoner to have a Ghost variable in the inspector so it will get the prefab there
            {
                ghost.EnemyStartPosition = StartPosition;
            }
            isFirstTime = false;
        }

        //stopDistance = getStopDistance();

        if (Player != null)
        {
            if (Vector2.Distance(transform.position, Player.position) > StopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
                if (ghost != null) ///TODO: delete this if after the solution above
                {
                    ghost.IsMakingGhost = false;
                }
            }
            else
            {
                if (Time.time >= AttackTime)
                {
                    AttackTime = Time.time + TimeBetweenAttacks;
                    StartCoroutine(Attack());
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
            if (ghost != null) ///TODO: delete this if after the solution above
            {
                ghost.IsMakingGhost = true;
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolationParamter = 4 * (percent - Mathf.Pow(percent, 2)); // it gets points (values) from a parabole between 0 and 1
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolationParamter);

            yield return null;
        }
    }
}
