using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private GameObject coinPrefab;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(dieWithDelay());
        }
    }

    private IEnumerator dieWithDelay()
    {
        audioSource.Play();
        die();
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 4);
        yield return new WaitForSeconds(3.9f);
        dropCoin();
    }

    private void die()
    {
        animator.SetTrigger("Dead");
    }

    private void dropCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab de moneda no asignado en el script Enemy.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
