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


    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
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