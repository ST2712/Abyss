using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{

    [SerializeField] private Transform ratioPunchController;
    [SerializeField] private Transform normalPunchControllerRight;
    [SerializeField] private Transform normalPunchControllerLeft;
    [SerializeField] private Transform normalPunchControllerUp;
    [SerializeField] private Transform normalPunchControllerDown;
    [SerializeField] private float ratioPunch;
    [SerializeField] private float normalRatioPunch;
    [SerializeField] public float punchDamage;
    [SerializeField] private float timeBetweenPunches;
    [SerializeField] private float timeNextPunch;
    private Vector2 direction;

    private Rigidbody2D rb2D;

    private float xMovement;
    private float yMovement;

    private Animator animator;


    private void circlePunch()
    {
        animator.SetTrigger("SpinPunch");

        Collider2D[] objects = Physics2D.OverlapCircleAll(ratioPunchController.position, ratioPunch);

        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Enemy"))
            {
                obj.transform.GetComponent<Enemy>().takeDamage(punchDamage);
            }
        }
        GameObject player = GameObject.Find("Player");
        StartCoroutine(player.GetComponent<TopDownMovement>().CancelSpeed(0.333f));
    }


    private void directionalPunch()
    {
        /*
        animator.SetTrigger("NormalPunch");

        Ray2D ray = new Ray2D(punchController.position, Vector2.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 2.0f);

        if (hit)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().takeDamage(punchDamage);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 2.0f, Color.red);
        }*/

        GameObject player = GameObject.Find("Player");
        StartCoroutine(player.GetComponent<TopDownMovement>().CancelSpeed(0.7f));

        Vector2 direction = normalPunchControllerDown.position; ;
        float xLast = animator.GetFloat("xLast");
        float yLast = animator.GetFloat("yLast");

        if (xLast == 0 && yLast == 0)
        {
            direction = normalPunchControllerDown.position;
        }
        else if (xLast == 1 && yLast == 0)
        {
            direction = normalPunchControllerRight.position;
        }
        else if (xLast == -1 && yLast == 0)
        {
            direction = normalPunchControllerLeft.position;
        }
        else if (xLast == 0 && yLast == 1)
        {
            direction = normalPunchControllerUp.position;
        }
        else if (xLast == 0 && yLast == -1)
        {
            direction = normalPunchControllerDown.position;
        }
        else if (xLast == 1 && yLast == 1)
        {
            direction = normalPunchControllerUp.position;
        }
        else if (xLast == -1 && yLast == 1)
        {
            direction = normalPunchControllerUp.position;
        }
        else if (xLast == 1 && yLast == -1)
        {
            direction = normalPunchControllerDown.position;
        }
        else if (xLast == -1 && yLast == -1)
        {
            direction = normalPunchControllerDown.position;
        }

        animator.SetTrigger("NormalPunch");

        Collider2D[] objects = Physics2D.OverlapCircleAll(direction, normalRatioPunch);

        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Enemy"))
            {
                obj.transform.GetComponent<Enemy>().takeDamage(punchDamage);
            }
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ratioPunchController.position, ratioPunch);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(normalPunchControllerRight.position, normalRatioPunch);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(normalPunchControllerLeft.position, normalRatioPunch);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(normalPunchControllerUp.position, normalRatioPunch);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(normalPunchControllerDown.position, normalRatioPunch);
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

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

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.P).ToString().ToLower() == "p") && timeNextPunch <= 0)
        {
            directionalPunch();
            animator.SetTrigger("Movement");
            timeNextPunch = timeBetweenPunches;
        }
    }
}
