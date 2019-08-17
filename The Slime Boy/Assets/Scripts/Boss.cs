﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector]
    private float halfHealth;

    [HideInInspector]
    private Animator animator;


    [SerializeField]
    private int health;

    [SerializeField]
    private List<Enemy> enemies;

    [SerializeField]
    private float spawnOffset;


    private void Start()
    {
        halfHealth = health / 2f;
        animator = GetComponent<Animator>();
    }

    public void DealDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 9999);
        if (health == 0)
        {
            Destroy(gameObject);
        }

        if (health <= halfHealth)
        {
            animator.SetTrigger("phase2");
        }

        var randomEnemy = enemies[Random.Range(0, enemies.Count)];
        float offset = Random.value > 0.5f ? spawnOffset : -spawnOffset;
        offset *= 1 + Random.value;

        Instantiate(randomEnemy, transform.position + new Vector3(offset, offset, 0), transform.rotation);
    }
}