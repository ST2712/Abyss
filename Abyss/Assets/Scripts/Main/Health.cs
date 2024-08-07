using System;
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

    [SerializeField] private AudioSource healthAudioSource;
    [SerializeField] private AudioSource damageAudioSource;

    private TopDownMovement topDownMovement;
    [SerializeField] private float noControlTime;
    private Animator animator;
    private GameObject player;
    private Vector2 diePoint;

    private GameOverMenu gameOverMenu;

    private CombatScript combatScript;

    private PauseMenu pauseMenu;

    void Awake()
    {
        gameOverMenu = GameObject.Find("DeadScreen").GetComponent<GameOverMenu>();
        gameOverMenu.gameObject.SetActive(false);
    }

    void Start()
    {
        topDownMovement = GetComponent<TopDownMovement>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
        animator.SetBool("canAttack", true);
        player = GameObject.Find("Player");
        combatScript = player.GetComponent<CombatScript>();
        pauseMenu = player.GetComponent<PauseMenu>();
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

        if (health == 1 && !extraHealth)
        {
            if (!healthAudioSource.isPlaying)
            {
                healthAudioSource.PlayOneShot(healthAudioSource.clip);
            }
        }
        else
        {
            healthAudioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.gameObject.activeSelf)
        {
            pauseMenu.Pause();

        }

    }

    public void takeDamage(int damage, Vector2 hitPoint)
    {
        player.GetComponent<CombatScript>().enableAttack(false);
        player.GetComponent<CombatScript>().punchDamage = 0;
        damageAudioSource.PlayOneShot(damageAudioSource.clip);
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
                //Destroy(gameObject, 3);
                gameOverMenu.GameOver();
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
}