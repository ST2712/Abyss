using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void takeDamage(float damage){
        health -= damage;
        if(health <= 0){
            die();
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 4);
        }
    }

    private void die(){
        animator.SetTrigger("Dead");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
