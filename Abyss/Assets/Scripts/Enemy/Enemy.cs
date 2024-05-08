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
    private Animator animator;
    private AudioSource audioSource;
    public GameObject coin;
    public GameObject soundController;
    private GameObject player;
    NavMeshAgent navMeshAgent;
    private int attackDamage;
    private bool canAttack = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        //attackDamage = 1;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            audioSource.Play();
            //GetComponent<FollowObjective>().enabled = false;
            die();
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 4);
        }
    }

    private void die()
    {
        animator.SetTrigger("Dead");
        coin.GetComponent<Coin>().soundController = soundController;
        //coin.GetComponent<Coin>().score = GameObject.FindGameObjectWithTag("Amount").GetComponent<Score>();
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
        animator.SetTrigger("Attack");

        float axuiliar = navMeshAgent.speed;
        //navMeshAgent.speed = 0;
        yield return new WaitForSeconds(0.5f); // Tiempo de la animación de ataque, ajusta según tu animación
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            //player.GetComponent<Health>().takeDamage(attackDamage);
            Collider2D[] objects = Physics2D.OverlapCircleAll(normalAttackRightController.position, attackRange);
        }
        yield return new WaitForSeconds(attackCooldown);
        navMeshAgent.speed = axuiliar;
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(1, collision.GetContact(0).normal);
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