using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //private int enemyKilledCount = 0;

    private bool areEnemiesKilled = false;

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField]
    private int health;


    public void TakeDamage(int damage)
    {
        var color = Color.gray;
        health = Mathf.Clamp(health - damage, 0, 9999);
        if (health == 0)
        {
            color = gameObject.tag == "Player" ? Color.white : Color.green;
            Destroy(gameObject);
            // Application.LoadLevel(Application.loadedLevel);

            if (color == Color.green) // killed enemy
            {
                color = Color.red;
                StaticData.EnemyKilledCount++;
                Debug.Log("Killed: " + StaticData.EnemyKilledCount);
                if (StaticData.EnemyKilledCount >= StaticData.EnemyCount)
                {
                    areEnemiesKilled = true;
                    color = Color.green;
                }
            }
            if (!areEnemiesKilled && color != Color.white)
            {
                return;
            }
            color = gameObject.tag == "Player" ? Color.white : Color.green;
            StartCoroutine(FlashInput(GameObject.FindGameObjectWithTag("Background"), color));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            StaticData.EnemyKilledCount = 0;
        }
    }

    private static IEnumerator FlashInput(GameObject input, Color flashColor)
    {
        Color defaultColor = input.GetComponent<SpriteRenderer>().color;
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
