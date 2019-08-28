using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float halfHealth;

    private Animator animator;

    private bool isInvulnerable = false;

    private bool isIntro = true;


    [SerializeField]
    private int health;

    [SerializeField]
    private List<Enemy> enemies;

    [SerializeField]
    private float spawnOffset;

    [SerializeField]
    private int damage;

    [SerializeField]
    private GameObject shockwave;

    [SerializeField]
    private GameObject deathEffect;


    private void Start()
    {
        halfHealth = health / 2f;
        animator = GetComponent<Animator>();
    }

    public void DealDamage(int damage)
    {
        float iSeconds = 5f;
        if (!isInvulnerable)
        {
            health = Mathf.Clamp(health - damage, 0, 9999);
            if (health == 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }

            if (health <= halfHealth)
            {
                animator.SetTrigger("phase2");
            }
            iSeconds = 1f;
        }


        var randomEnemy = enemies[Random.Range(0, enemies.Count)];
        float offset = Random.value > 0.5f ? spawnOffset : -spawnOffset;
        offset *= 1 + Random.value;

        Instantiate(randomEnemy, transform.position + new Vector3(offset, offset, 0), transform.rotation);

        StartCoroutine(GetInvulnerability(iSeconds));
    }

    public void SpawnShockwave()
    {
        var boss = gameObject.transform;
        Destroy(Instantiate(shockwave, boss.position, boss.rotation), .2f);
        ShakeScreen();
    }

    public void ShakeScreen()
    {
        float chance;
        if (isIntro)
        {
            chance = 1f;
            isIntro = false;
        }
        else
        {
            chance = .1f;
        }

        if (Random.value < chance)
        {
            Debug.Log(chance);
            Camera.main.GetComponent<Animator>().SetTrigger("shake");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().DealDamage(damage);
        }
    }

    private IEnumerator GetInvulnerability(float iSeconds)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(iSeconds);
        isInvulnerable = false;
    }
}
