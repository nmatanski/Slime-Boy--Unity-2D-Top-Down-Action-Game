using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Weapon weaponToUpgrade;

    [SerializeField]
    [Range(1, 6)]
    private int levelupValue = 1;

    [SerializeField]
    private GameObject pickupEffect;


    //private void Start()
    //{
    //    Debug.Log($"pickups on the map: {GameObject.FindGameObjectsWithTag("Pickup").Length}");
    //    if (GameObject.FindGameObjectsWithTag("Pickup").Length > 1)
    //    {
    //        Destroy(gameObject);
    //    }
        
    //}


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log($"Level: {weaponToUpgrade.Level}");
            if (weaponToUpgrade.Level + levelupValue < 8)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

                weaponToUpgrade.Level += levelupValue;
            }
            Debug.Log($"Levelup to {weaponToUpgrade.Level}");
            Destroy(gameObject);
        }
    }

    //private void OnDestroy()
    //{
    //    weaponToUpgrade.Level += levelupValue;
    //}
}
