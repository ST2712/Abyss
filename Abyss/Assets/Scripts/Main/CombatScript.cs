using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{

    [SerializeField] private Transform punchController;
    [SerializeField] private float ratioPunch;
    [SerializeField] private float punchDamage;
    [SerializeField] private float timeBetweenPunches;
    [SerializeField] private float timeNextPunch;
    [SerializeField] private float movementSpeed;
    private Vector2 direction;

    private Rigidbody2D rb2D;

    private float xMovement;
    private float yMovement;

    private Animator animator;

    private void circlePunch()
    {

        animator.SetTrigger("CirclePunch");

        Collider2D[] objects = Physics2D.OverlapCircleAll(punchController.position, ratioPunch);

        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Enemy"))
            {
                obj.transform.GetComponent<Enemy>().takeDamage(punchDamage);
            }
        }
    }

    private void directionalPunch(Vector2 direction)
    {
        animator.SetTrigger("NormalPunch");

        RaycastHit2D[] hits = Physics2D.RaycastAll(punchController.position, direction, ratioPunch);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().takeDamage(punchDamage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchController.position, ratioPunch);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");
        animator.SetFloat("xMovement", xMovement);
        animator.SetFloat("yMovement", yMovement);

        if (xMovement != 0 || yMovement != 0)
        {
            animator.SetFloat("xLast", xMovement);
            animator.SetFloat("yLast", yMovement);

        }
        direction = new Vector2(xMovement, yMovement).normalized;

        if (timeNextPunch > 0)
        {
            timeNextPunch -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && timeNextPunch <= 0)
        {
            circlePunch();
            timeNextPunch = timeBetweenPunches;
        }

        Vector2 playerDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Verificar si el jugador está pulsando el botón de golpear y si ha pasado el tiempo entre golpes
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.P).ToString().ToLower() == "p") && timeNextPunch <= 0)
        {
            directionalPunch(playerDirection);
            timeNextPunch = timeBetweenPunches;
        }
    }

        private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);
    }
}
