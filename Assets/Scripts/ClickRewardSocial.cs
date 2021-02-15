using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRewardSocial : MonoBehaviour
{
    private GameLogicRewardSocial2 gameLogic;
    private AudioSource[] sounds;
    public int person;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicRewardSocial2>();
        sounds = GetComponents<AudioSource>();

    }


    public void OnMouseDown()
    {
        if (gameLogic.j){
            gameLogic.personclicked(person);
        }

    }
    
}
