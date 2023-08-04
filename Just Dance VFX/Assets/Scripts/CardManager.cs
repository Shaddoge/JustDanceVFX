using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private CardPlayer[] cardPlayers;
    
    private const float COOLDOWN = 2f;
    private bool onCooldown = false;

    private void Awake()
    {
        cardPlayers = GetComponentsInChildren<CardPlayer>();
    }

    public void Play(int index)
    {
        if (onCooldown) return;
        if (cardPlayers.Length == 0) return;
        if (index < 0 || index >= cardPlayers.Length) return;
        
        StartCoroutine(StartCooldown());

        for (int i = 0; i < cardPlayers.Length; i++)
        {
            if (i != index)
                cardPlayers[i].SetStartState();
            else
                cardPlayers[i].Play();
        }
    }

    private IEnumerator StartCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(COOLDOWN);
        onCooldown = false;
    }
}
