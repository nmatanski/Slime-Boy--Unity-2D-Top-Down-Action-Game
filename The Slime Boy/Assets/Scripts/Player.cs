using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private Rigidbody2D rb;

    private Vector2 moveAmmount;

    private Animator animator;

    private Animator cameraAnimator;

    private int timer = 0;

    private int damageAmount;

    private bool isInvulnerable = false;


    [SerializeField]
    private Animator hurtAnimator;

    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private Sprite fullHeart;

    [SerializeField]
    private Sprite threeQuartersHeart;

    [SerializeField]
    private Sprite halfHeart;

    [SerializeField]
    private Sprite quarterHeart;

    [SerializeField]
    private Sprite emptyHeart;

    public bool IsHitByShockwave { get; set; } = false;


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        cameraAnimator = Camera.main.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        MaxHealth = Health;
    }

    // Update is called once per frame
    private void Update()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmmount = moveInput.normalized * Speed;

        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningUp", false);
            animator.SetBool("isRunningDown", false);

            if (moveInput == Vector2.left)
            {
                animator.SetBool("isRunningLeft", true);
            }
            else if (moveInput == Vector2.right)
            {
                animator.SetBool("isRunningRight", true);
            }
            else if (moveInput == Vector2.up)
            {
                animator.SetBool("isRunningUp", true);
            }
            else if (moveInput == Vector2.down)
            {
                animator.SetBool("isRunningDown", true);
            }
        }
        else
        {
            timer++;
            if (timer > 10)
            {
                animator.SetBool("isRunningLeft", false);
                animator.SetBool("isRunningRight", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isRunningDown", false);
                timer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmmount * Time.deltaTime);
    }

    private void UpdateHealthUI(int currentHealth)
    {
        int fullHeartsCount = Mathf.FloorToInt(currentHealth / 4f);

        for (int i = 0; i < fullHeartsCount; i++)
        {
            hearts[i].sprite = fullHeart;
        }

        //if (fullHeartsCount == hearts.Length)
        //{
        //    return;
        //}

        int heartType = currentHealth % 4;
        Sprite damagedHeart = null;
        switch (heartType)
        {
            case 1:
                damagedHeart = quarterHeart;
                break;
            case 2:
                damagedHeart = halfHeart;
                break;
            case 3:
                damagedHeart = threeQuartersHeart;
                break;
        }
        if (fullHeartsCount >= hearts.Length)
        {
            return;
        }
        hearts[fullHeartsCount].sprite = damagedHeart;
        Debug.Log(fullHeartsCount);
        for (int i = fullHeartsCount + 1; i < hearts.Length; i++)
        {
            hearts[i].sprite = emptyHeart;
        }

        //if (heartType == 0 && fullHeartsCount < hearts.Length)
        //{
        //    hearts[hearts.Length - 1].sprite = emptyHeart;
        //}

        for (int i = 0; i < hearts.Length; i++) ///TODO: Remove this when you find the bug with the null sprites instead of empty ones
        {
            if (hearts[i].sprite == null)
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public override void DealDamage(int damage) ///TODO: Add override keyword, but add it to the Character's DealDamage, then fix it in the Enemy class
    {
        damageAmount = damage;
        StartCoroutine(DealDamage());
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        UpdateHealthUI(Health);
    }

    private IEnumerator RestartLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private IEnumerator DealDamage()
    {
        if (isInvulnerable)
        {
            yield break;
        }

        Health = Mathf.Clamp(Health - damageAmount, 0, 9999);

        UpdateHealthUI(Health);

        Debug.Log(IsHitByShockwave.ToString());
        if (IsHitByShockwave)
        {
            Debug.Log("\n\n\n\tHit by Shockwave");
            yield return new WaitForSeconds(.1f); ///TODO: It should be .2f but the player will "visible"
            IsHitByShockwave = false;
            Debug.Log("\n\t\tHurtPanel happens now!");
        }

        hurtAnimator.SetTrigger("hurt");
        cameraAnimator.SetTrigger("shake");

        StartCoroutine(GetInvulnerability());

        if (Health == 0)
        {
            ///TODO: temporary restart
            StartCoroutine(RestartLevel(3f));
            //Destroy(gameObject); //add this
            ///end
        }
    }

    private IEnumerator GetInvulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(.2f);
        isInvulnerable = false;
    }
}
