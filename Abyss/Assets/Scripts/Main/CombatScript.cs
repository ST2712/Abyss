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

    private Animator animator;

    private void punch(){

        animator.SetTrigger("Punch");

        Collider2D[] objects = Physics2D.OverlapCircleAll(punchController.position, ratioPunch);

        foreach(Collider2D obj in objects){
            if(obj.CompareTag("Enemy")){
                obj.transform.GetComponent<Enemy>().takeDamage(punchDamage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchController.position, ratioPunch);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeNextPunch > 0){
            timeNextPunch -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Fire1") && timeNextPunch <= 0){
            punch();
            timeNextPunch = timeBetweenPunches;
        }
    }
}
