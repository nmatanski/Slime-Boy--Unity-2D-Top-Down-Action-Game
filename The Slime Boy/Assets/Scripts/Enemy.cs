using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private float stopDistanceMin;

    public float StopDistanceMin
    {
        get { return stopDistanceMin; }
        set { stopDistanceMin = value; }
    }

    [SerializeField]
    private float stopDistanceMax;

    public float StopDistanceMax
    {
        get { return stopDistanceMax; }
        set { stopDistanceMax = value; }
    }


    [SerializeField]
    private float timeBetweenAttacks;
    public float TimeBetweenAttacks
    {
        get { return timeBetweenAttacks; }
        set { timeBetweenAttacks = value; }
    }

    [SerializeField]
    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float StopDistance
    {
        get
        {
            var stopDistance = Random.Range(StopDistanceMin, StopDistanceMax);
            if (stopDistance >= StopDistanceMax)
            {
                Random.seed++; ///?
                stopDistance = Random.Range(StopDistanceMin, StopDistanceMax);
            }

            return stopDistance;
        }
    }


    public float AttackTime { get; set; } //timer

    public Transform Player { get; set; }

    public Vector3 StartPosition { get; set; }


    // Start is called before the first frame update
    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        StartPosition = transform.position;
    }
}
