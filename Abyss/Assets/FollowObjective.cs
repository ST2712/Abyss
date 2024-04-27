using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowObjective : MonoBehaviour
{
    [SerializeField] private Transform target;
    public static NavMeshAgent navMeshAgent;
    private float xMovement;
    private float yMovement;

    public Animator animator;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(target.position);

        Vector3 direction = (target.position - transform.position).normalized;
        xMovement = direction.x;
        yMovement = direction.y;
        UpdateAnimation(xMovement, yMovement);
    }

    private void UpdateAnimation(float xMovement, float yMovement)
    {
        bool isWalking = xMovement != 0 || yMovement != 0;

        animator.SetBool("IsWalking", isWalking);
        animator.SetFloat("xMovement", xMovement);
        animator.SetFloat("yMovement", yMovement);
    }
}
