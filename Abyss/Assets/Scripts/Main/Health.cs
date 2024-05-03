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

    void Start()
    {
        heartBite = GetComponent<AudioSource>();
        topDownMovement = GetComponent<TopDownMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        if (extraHealth)
        {
            extraHealth = false;
            StartCoroutine(NoControl());
            topDownMovement.bounce(hitPoint);
            return;
        }
        else
        {
            health -= damage;
            if(health <= 0)
            {
            animator.SetTrigger("Dead");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 4);
            }
            //animator.SetTrigger("Hurt");
            StartCoroutine(NoControl());
            topDownMovement.bounce(hitPoint);
        }
    }

    private IEnumerator NoControl()
    {
        topDownMovement.canMove = false;
        yield return new WaitForSeconds(noControlTime);
        topDownMovement.canMove = true;
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