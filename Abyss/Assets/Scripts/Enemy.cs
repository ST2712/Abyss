using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;
    private Animator animator;
    private AudioSource audioSource;
    public GameObject coin;
    public GameObject soundController;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void takeDamage(float damage){
        health -= damage;
        if(health <= 0){
            audioSource.Play();
            //FollowObjective.navMeshAgent.speed = 0;
            die();
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 4);
        }
    }

    private void die(){
        animator.SetTrigger("Dead");
        coin.GetComponent<Coin>().soundController = soundController;
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<Health>().takeDamage(1, collision.GetContact(0).point);
        }
    }
}