using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinSoundClip;
    [SerializeField] public GameObject soundController;
    [SerializeField] private int coinValue;
    [SerializeField] public Score score;
    AudioSource audioSource;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(score == null){
                score = GameObject.Find("Amount").GetComponent<Score>();
            }
            score.getPoints(coinValue);
            soundController.GetComponent<CoinSoundController>().playSound(coinSoundClip);
            Destroy(gameObject);
        }
    }

    public void destroy(){
        Destroy(gameObject);
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}