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

    [SerializeField]private AudioSource audioSource;

    [SerializeField] private AudioClip heartBeat;

    private TopDownMovement topDownMovement;
    [SerializeField] private float noControlTime;
    private Animator animator;
    private GameObject player;
    private Vector2 diePoint;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        topDownMovement = GetComponent<TopDownMovement>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
        animator.SetBool("canAttack", true);
        player = GameObject.Find("Player");

    }

    void Update()
    {

        if (health <= 0)
        {
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(heartBeat);
                StartCoroutine(PauseSoundAndResume());
            }
        }
        else
        {
            audioSource.Stop();
        }

    }

    public void takeDamage(int damage, Vector2 hitPoint)
    {
        player.GetComponent<CombatScript>().enableAttack(false);
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
                player.GetComponent<CombatScript>().enabled = false;
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

    private IEnumerator NoControl()
    {
        topDownMovement.canMove = false;
        yield return new WaitForSeconds(noControlTime);
        topDownMovement.canMove = true;
        player.GetComponent<CombatScript>().enableAttack(true);
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

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.PlayDelayed(1f);
        }
    }
}