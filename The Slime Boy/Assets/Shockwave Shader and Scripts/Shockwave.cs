using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private Player player;


    [SerializeField]
    private float scalingSpeedCoefficient;

    [SerializeField]
    private float maxSizeCoefficient;

    [SerializeField]
    private int damage;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        transform.localScale += new Vector3(scalingSpeedCoefficient, scalingSpeedCoefficient, scalingSpeedCoefficient);
        if (transform.localScale.x > maxSizeCoefficient)
        {
            transform.localScale = new Vector3(scalingSpeedCoefficient, scalingSpeedCoefficient, scalingSpeedCoefficient);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.IsHitByShockwave = true;
            player.DealDamage(damage);
        }
    }
}
