using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Vector3 position;
    private Quaternion rotation;

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

    [SerializeField]
    private int pickupChance;

    [SerializeField]
    private List<GameObject> pickups;

    [SerializeField]
    private int healthPickupChance;

    [SerializeField]
    private GameObject healthPickup;

    [SerializeField]
    private GameObject deathEffect;


    // Start is called before the first frame update
    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        StartPosition = transform.position;
        MaxHealth = Health;
    }

    public override void DealDamage(int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, 9999);
        if (Health == 0)
        {
            position = transform.position;
            rotation = transform.rotation;

            var deathParticles = Instantiate(deathEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
            deathParticles.simulationSpeed = 3f;

            Destroy(gameObject);
        }
    }

    private void OnDestroy() // use this method for things you want to lock to execute only once per object's life (at its end)
    {
        int randomNumer = Random.Range(0, 101);
        bool hasPickup = false;
        GameObject randomPickup = null;
        if (randomNumer < pickupChance && pickups != null)
        {
            randomPickup = pickups[Random.Range(0, pickups.Count-1)];
            hasPickup = true;
        }

        int randomHealth = Random.Range(0, 101);
        if (randomHealth < healthPickupChance)
        {
            Instantiate(healthPickup, position, rotation);
        }
        else if (hasPickup) // if there is a pickup and health pickup it will give the health pickup, but not the other one
        {
            Instantiate(randomPickup, position, rotation).SetActive(true);
        }

    }
}
