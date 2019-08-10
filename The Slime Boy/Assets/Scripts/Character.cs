using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    //private int enemyKilledCount = 0;

    ///private bool areEnemiesKilled = false;

    public int MaxHealth { get; set; }

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


    public abstract void DealDamage(int damage); ///TODO: this could happen through parallel collisions and character (enemy) could die multiple times at once, it should lock this method/object of class Character to be accessible through 1 task at a time

    public virtual void Heal(int healAmount)
    {
        Health = Mathf.Clamp(Health + healAmount, 1, MaxHealth);
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
