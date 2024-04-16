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

    void Update()
    {
        if(health > numberOfHearts){
            health = numberOfHearts;
        }
        


        for (int i = 0; i < hearts.Length; i++){
            if (i < health){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }

        if(extraHealth == true){
            hearts[3].sprite = extraHeart;
            hearts[3].enabled = true;
        }
    }

    

}
