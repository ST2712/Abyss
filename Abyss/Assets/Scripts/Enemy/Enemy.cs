using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform normalAttackRightController;
    [SerializeField] private Transform normalAttackLeftController;
    [SerializeField] private Transform attackRangeController;
    [SerializeField] private float rangeToAttack;
    [SerializeField] private AudioSource damage_sound;
    private Animator animator;
    private AudioSource audioSource;
    public GameObject coin;
    public GameObject soundController;
    private GameObject player;
    NavMeshAgent navMeshAgent;
    private int attackDamage;
    private bool canAttack = true;

    public event EventHandler OnAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackDamage = 1;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        damage_sound.PlayOneShot(damage_sound.clip);
        if (health <= 0)
        {
            audioSource.Play();
            GetComponent<FollowObjective>().enabled = false;
            die();
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2);
        }
    }

    private void die()
    {
        animator.SetTrigger("Dead");
        coin.GetComponent<Coin>().soundController = soundController;
        coin.GetComponent<Coin>().score = GameObject.FindGameObjectWithTag("Amount").GetComponent<Score>();
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= rangeToAttack && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        float axuiliar = navMeshAgent.speed;
        navMeshAgent.speed = 0;
        animator.SetTrigger("Attack");
        /*
        if ((player != null) && (Vector2.Distance(transform.GetChild(0).position, player.transform.position) <= attackRange))
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.GetChild(0).position, attackRange);

            foreach (Collider2D obj in objects)
            {
                if (obj.CompareTag("Player"))
                {
                    Debug.Log("Player hit");
                    obj.transform.GetComponent<Health>().takeDamage(attackDamage, Vector2.zero);
                }
            }
        }*/

        yield return new WaitForSeconds(attackCooldown);
        navMeshAgent.speed = axuiliar;
        canAttack = true;
    }

    public void ApplyDamage()
    {
        if ((player != null) && (Vector2.Distance(transform.GetChild(0).position, player.transform.position) <= attackRange))
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D obj in objects)
            {
                if (obj.CompareTag("Player"))
                {
                    Debug.Log("Player hit");
                    obj.transform.GetComponent<Health>().takeDamage(attackDamage, Vector2.zero);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with player");
            collision.gameObject.GetComponent<Health>().takeDamage(attackDamage, collision.GetContact(0).normal);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(normalAttackRightController.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(normalAttackLeftController.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackRangeController.position, rangeToAttack);
    }
}