using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numberOfHearts;
    public bool extraHealth;
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite extraHeart;

    private AudioSource heartBeat;

    void Start()
    {
        heartBite = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            hearts[i].enabled = i < numberOfHearts;
        }

        if(extraHealth)
        {
            hearts[3].sprite = extraHeart;
            hearts[3].enabled = true;
        }

        if(health == 1)
        {
            if (!heartBite.isPlaying)
            {
                heartBeat.Play();
                StartCoroutine(PauseSoundAndResume());
            }
        }else{
            heartBite.Stop();
        }
    }

    IEnumerator PauseSoundAndResume()
    {
        yield return new WaitForSeconds(1f);

        if(heartBeat.isPlaying)
        {
            heartBeat.Stop();
            heartBeat.PlayDelayed(1f);
        }
    }
}
