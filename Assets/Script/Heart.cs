using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public int numberOfHerats;
    public Image[] hearts;
    public Sprite  fullHeart;
    public Sprite  halfHeart;
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        if(player.health >numberOfHerats )
        {
             player.health= numberOfHerats;
        }
    }
    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numberOfHerats)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if(player.health>i)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = halfHeart ;
            }
        }
    }
}
