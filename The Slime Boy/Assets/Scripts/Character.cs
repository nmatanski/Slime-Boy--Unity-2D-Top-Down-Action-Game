using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int health;


    private static IEnumerator FlashInput(GameObject input, Color flashColor)
    {
        Color defaultColor = input.GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 4; i++)
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

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 9999);
        if (health == 0)
        {
            // Destroy(gameObject);
            // Application.LoadLevel(Application.loadedLevel);

            var color = gameObject.tag == "Player" ? Color.red : Color.green;
            StartCoroutine(FlashInput(GameObject.FindGameObjectWithTag("Background"), color));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
