using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;


    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private GameObject sound;


    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
        if (GameObject.FindGameObjectsWithTag("FireballSound").Length < 3 || Random.value < .5f)
        {
            Instantiate(sound, transform.position, transform.rotation);
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}
