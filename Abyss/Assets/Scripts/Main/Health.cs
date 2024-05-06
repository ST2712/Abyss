using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numberOfHearts;
    public bool extraHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite extraHeart;

    private AudioSource heartBite;

    private TopDownMovement topDownMovement;
    [SerializeField] private float noControlTime;
    private Animator animator;
    private GameObject player;
    private Vector2 diePoint;

    void Start()
    {
        heartBite = GetComponent<AudioSource>();
        topDownMovement = GetComponent<TopDownMovement>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
        animator.SetBool("canAttack", true);
        player = GameObject.Find("Player");

    }

    void Update()
    {

        if(health <= 0){
            player.GetComponent<Transform>().position = diePoint;
        }

        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            hearts[i].enabled = i < numberOfHearts;
        }

        if (extraHealth)
        {
            hearts[3].sprite = extraHeart;
            hearts[3].enabled = true;
        }

        if (health == 1)
        {
            if (!heartBite.isPlaying)
            {
                heartBite.Play();
                StartCoroutine(PauseSoundAndResume());
            }
        }
        else
        {
            heartBite.Stop();
        }

    }

    public void takeDamage(int damage, Vector2 hitPoint)
    {
        animator.SetBool("canAttack", false);
        player.GetComponent<CombatScript>().punchDamage = 0;
        if (extraHealth)
        {
            extraHealth = false;
            animator.SetTrigger("Hurt");
            StartCoroutine(NoControl());
            StartCoroutine(NoCollision());
            topDownMovement.bounce(hitPoint);
            return;
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                animator.SetBool("isDead", true);
                animator.SetTrigger("Dead");
                player.GetComponent<CombatScript>().punchDamage = 0;
                GetComponent<Collider2D>().enabled = false;
                diePoint = player.transform.position;
                Destroy(gameObject, 4);
            }
            animator.SetTrigger("Hurt");
            StartCoroutine(NoControl());
            StartCoroutine(NoCollision());
            topDownMovement.bounce(hitPoint);
        }
    }

        public void takeDamage(int damage)
    {
        if (extraHealth)
        {
            extraHealth = false;
            return;
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                animator.SetTrigger("Dead");
                GetComponent<Collider2D>().enabled = false;
                diePoint = player.transform.position;
                Destroy(gameObject, 4);
            }
            //animator.SetTrigger("Hurt");
        }
    }

    private IEnumerator NoControl()
    {
        topDownMovement.canMove = false;
        yield return new WaitForSeconds(noControlTime);
        topDownMovement.canMove = true;
        animator.SetBool("canAttack", true);
        player.GetComponent<CombatScript>().punchDamage = 20;
    }

        private IEnumerator NoCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(noControlTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    IEnumerator PauseSoundAndResume()
    {
        yield return new WaitForSeconds(1f);

        if (heartBite.isPlaying)
        {
            heartBite.Stop();
            heartBite.PlayDelayed(1f);
        }
    }
}