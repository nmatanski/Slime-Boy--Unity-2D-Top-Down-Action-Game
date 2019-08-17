using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    private List<GameObject> patrolPoints;

    private int randomPoint;

    private int closestPointIndex;

    private bool isSmart = false;

    private float nextTime = 0f;

    private float timer = 0f;


    [SerializeField]
    private float speed;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint").ToList();
        randomPoint = Random.Range(0, patrolPoints.Count);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var randomPointPosition = patrolPoints[randomPoint].transform.position;

        timer += Time.deltaTime;
        if (Time.time > nextTime && timer > 4)
        {
            nextTime += 1f;

            var player = GameObject.FindGameObjectWithTag("Player");

            closestPointIndex = 0;
            var closestPoint = patrolPoints[closestPointIndex];
            var distance = Vector3.Distance(player.transform.position, closestPoint.transform.position);

            for (int i = 1; i < patrolPoints.Count; i++)
            {
                var tempDistance = Vector3.Distance(player.transform.position, patrolPoints[i].transform.position);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closestPointIndex = i;
                }
            }
            randomPointPosition = patrolPoints[closestPointIndex].transform.position; //not random but closest

            isSmart = true;
            Debug.Log("SMART SMART SMART");
        }
        else
        {
            Debug.Log("I'm so dumb, smaaaall brain, dumb-dumb. owo");
        }

        if (isSmart)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, randomPointPosition, 2 * speed * Time.deltaTime);
        }
        else
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, randomPointPosition, speed * Time.deltaTime);
        }

        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < .1f) // .1f minimum threshold - distance between the boss and player
        {
            randomPoint = Random.Range(0, patrolPoints.Count);
        }

        if (System.DateTime.Now.Second % 3 == 0)
        {
            isSmart = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
