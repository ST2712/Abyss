using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    [SerializeField] public float movementSpeed;
    private Vector2 direction;
    [SerializeField] private Vector2 v2bounceSpeed;
    private Rigidbody2D rb2D;

    private float xMovement;
    private float yMovement;

    private Animator animator;

    public bool canMove = true;

    private bool axesDisabled = false;

    [SerializeField] private AudioSource playerAudioSource;

    private PauseMenu pauseMenu;

    private CombatScript combatScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        pauseMenu = GameObject.Find("Player").GetComponent<PauseMenu>();
        combatScript = GameObject.Find("Player").GetComponent<CombatScript>();
        axesDisabled = false;
    }

    void Update()
    {
        if (!axesDisabled)
        {
            xMovement = Input.GetAxisRaw("Horizontal");
            yMovement = Input.GetAxisRaw("Vertical");
            animator.SetFloat("xMovement", xMovement);
            animator.SetFloat("yMovement", yMovement);

            if (xMovement != 0 || yMovement != 0)
            {
                animator.SetFloat("xLast", xMovement);
                animator.SetFloat("yLast", yMovement);
                if (!playerAudioSource.isPlaying && canMove && playerAudioSource.isActiveAndEnabled)
                {
                    playerAudioSource.PlayOneShot(playerAudioSource.clip);
                }
            }

            direction = new Vector2(xMovement, yMovement).normalized;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                combatScript.timeNextPunch = 0.01f;
                if (pauseMenu.gameIsPaused)
                {
                    pauseMenu.Resume();
                }
                else
                {
                    pauseMenu.Pause();
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void bounce(Vector2 hitPoint)
    {
        rb2D.velocity = new Vector2(-v2bounceSpeed.x * hitPoint.x, -v2bounceSpeed.y * hitPoint.y);
    }

    public IEnumerator CancelSpeed(float time)
    {
        float auxiliarMovementSpeed = movementSpeed;
        movementSpeed = 0;
        axesDisabled = true;
        yield return new WaitForSeconds(time);
        axesDisabled = false;
        movementSpeed = auxiliarMovementSpeed;
    }
}