using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private const string iframesHPColor = "#00FFD6";

    private const string whiteColor = "#FFFFFF";

    private float halfHealth;

    private Animator animator;

    private bool isInvulnerable = false;

    private TextMeshProUGUI tooltip;

    private bool isIntro = true;

    private SceneTransition sceneTransition;

    private Player player;


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

    [SerializeField]
    private Slider bossHealthBar;

    [SerializeField]
    private int scorePoints;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        halfHealth = health / 2f;
        animator = GetComponent<Animator>();
        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = health;
        bossHealthBar.value = health;
        sceneTransition = FindObjectOfType<SceneTransition>();
        tooltip = GameObject.FindGameObjectWithTag("TooltipUI").GetComponent<TextMeshProUGUI>();
        ChangeText(tooltip, "The Creator");
    }

    public void DealDamage(int damage)
    {
        float iSeconds = 5f;
        if (!isInvulnerable)
        {
            health = Mathf.Clamp(health - damage, 0, 9999);
            bossHealthBar.value = health;

            if (health == 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                ChangeText(tooltip, "CONGRATULATIONS!");
                bossHealthBar.gameObject.SetActive(false);

                player.Score += scorePoints;

                player.PlayerPrefsController.SetScore(player.Score);

                Destroy(gameObject);
                sceneTransition.LoadScene("Win");
            }

            if (health <= halfHealth)
            {
                ChangeToPhase2();
                ChangeText(tooltip, "DIE!");
            }
            iSeconds = 1f;

            var randomEnemy = enemies[Random.Range(0, enemies.Count)];
            float offset = Random.value > 0.5f ? spawnOffset : -spawnOffset;
            offset *= 1 + Random.value;

            Instantiate(randomEnemy, transform.position + new Vector3(offset, offset, 0), transform.rotation);
        }

        if (Random.value < .01f)
        {
            animator.SetTrigger("spawningShockwaves");
        }

        StartCoroutine(GetInvulnerability(iSeconds));
    }

    public void SpawnShockwave()
    {
        var boss = gameObject.transform;
        Destroy(Instantiate(shockwave, boss.position, boss.rotation), .2f);
        if (Random.value < .2f || isIntro)
        {
            ShakeScreen();
            isIntro = false;
        }
    }

    public void ChangeToPhase2()
    {
        animator.SetTrigger("phase2");
    }

    public void ShakeScreen()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("shake");
    }

    public void ChangeText(TextMeshProUGUI textbox, string text)
    {
        StartCoroutine(textbox.ChangeText(text));
    }

    public void ChangeText(string text, System.Func<string, IEnumerator> changeText)
    {
        StartCoroutine(changeText(text));
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
        if (GameObject.FindGameObjectWithTag("BossHPBarFill") == null) ///TODO: temporary nullpointerexception fix
        {
            yield break;
        }

        var barSprite = GameObject.FindGameObjectWithTag("BossHPBarFill").GetComponent<Image>();

        barSprite.color = ColorUtility.TryParseHtmlString(iframesHPColor, out Color color) ? color : barSprite.color;
        player.Score -= 20; //punish for hitting the boss while being immune to damage
        isInvulnerable = true;

        yield return new WaitForSeconds(iSeconds);

        isInvulnerable = false;
        barSprite.color = ColorUtility.TryParseHtmlString(whiteColor, out color) ? color : barSprite.color;
    }
}
