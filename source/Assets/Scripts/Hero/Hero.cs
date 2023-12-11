using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private float jumpForce = 15f; 
    private bool isGrounded = false;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private GameObject PomerPanel;
    [SerializeField] private GameObject WinPanel;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    public override void GetDamage() 
    {
        health -= 1;
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
    }

    public override void Die()
    {
        State = States.death;
        Invoke("SetPomerPanel", 1.1f);
    }

    private void SetPomerPanel()
    {
        PomerPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void SetWinPanel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        lives = 5;
        health = lives;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;
        PomerPanel.SetActive(false);
        WinPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded && !isAttacking && health > 0) State = States.idle;

        if (!isAttacking && Input.GetButton("Horizontal") && health > 0)
            Run();
        if (!isAttacking && isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();

        if (health > lives)
            health = lives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded && health > 0) State = States.jump;
    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Attack()
    {
        if (isGrounded && isRecharged && health > 0) 
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++) 
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.35f);
        isRecharged = true;
    }
}


public enum States
{
    idle,
    run,
    jump,
    attack,
    death
}