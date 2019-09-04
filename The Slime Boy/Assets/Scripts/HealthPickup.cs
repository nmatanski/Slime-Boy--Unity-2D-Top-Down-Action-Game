using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private int healAmount;

    [SerializeField]
    private GameObject pickupEffect;


    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) ///TODO: temporary nullpointerexception fix
        {
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && player != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
