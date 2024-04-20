using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}