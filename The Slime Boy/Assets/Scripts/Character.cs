using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //private int enemyKilledCount = 0;

    ///private bool areEnemiesKilled = false;

    private bool isEnemy;

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField]
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    [SerializeField]
    private int pickupChance;
    public int PickupChance
    {
        get { return pickupChance; }
        set { pickupChance = value; }
    }

    [SerializeField]
    private List<GameObject> pickups;
    public List<GameObject> Pickups
    {
        get { return pickups; }
        set { pickups = value; }
    }


    public void TakeDamage(int damage) ///TODO: this could happen through parallel collisions and character (enemy) could die multiple times at once, it should lock this method/object of class Character to be accessible through 1 task at a time
    {
        ///var color = Color.gray;
        health = Mathf.Clamp(health - damage, 0, 9999);
        if (health == 0)
        {
            ///color = gameObject.tag == "Player" ? Color.white : Color.green;

            //if (gameObject.tag == "Enemy")
            //{
            //    int randomNumer = Random.Range(0, 101);
            //    if (randomNumer < pickupChance)
            //    {
            //        var randomPickup = Pickups[Random.Range(0, Pickups.Count)];
            //        Instantiate(randomPickup, transform.position, transform.rotation).SetActive(true);
            //    }
            //}
            isEnemy = gameObject.tag == "Enemy";

            Destroy(gameObject);
            // Application.LoadLevel(Application.loadedLevel);

            //if (color == Color.green) // killed enemy
            //{
            //    color = Color.red;
            //    ///StaticData.EnemyKilledCount++;
            //    ///Debug.Log("Killed: " + StaticData.EnemyKilledCount);
            //    ///
            //    /*if (StaticData.EnemyKilledCount >= StaticData.EnemyCount)
            //    {
            //        areEnemiesKilled = true;
            //        color = Color.green;
            //    }*/
            //    ///
            //}
            //if (!areEnemiesKilled && color != Color.white)
            //{
            //    return;
            //}
            //color = gameObject.tag == "Player" ? Color.white : Color.green;
            //StartCoroutine(FlashInput(GameObject.FindGameObjectWithTag("Background"), color));
            ///SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ///StaticData.EnemyKilledCount = 0;
        }
    }

    private void OnDestroy() // use this method for things you want to lock to execute only once per object's life (at its end)
    {
        if (isEnemy)
        {
            int randomNumer = Random.Range(0, 101);
            if (randomNumer < pickupChance)
            {
                var randomPickup = Pickups[Random.Range(0, Pickups.Count)];
                Instantiate(randomPickup, transform.position, transform.rotation).SetActive(true);
            }
        }
    }

    public static IEnumerator FlashInput(Color flashColor)
    {
        var input = GameObject.FindGameObjectWithTag("Background");
        var defaultColor = input.GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 10; i++)
        {
            // if the current color is the default color - change it to the flash color
            if (input.GetComponent<SpriteRenderer>().color == defaultColor)
            {
                input.GetComponent<SpriteRenderer>().color = flashColor;
            }
            else // otherwise change it back to the default color
            {
                input.GetComponent<SpriteRenderer>().color = defaultColor;
            }
            yield return new WaitForSeconds(.5f);
        }
        Destroy(input.gameObject, 1);
        yield return new WaitForSeconds(1);
    }
}
