using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Dealer : MonoBehaviour
{
    [SerializeField] DealManager dealManager;
    [SerializeField] GameManager gameManager;
    public Hand hand;


    public async void DealerTurn()
    {
        await dealManager.TurnDealerSecondCard();

        while (hand.handValue.value < 17)
        {
            await DealerHit();
        }

        gameManager.Result();
    }

    public async Task DealerHit()
    {
        await Task.Delay(TimeSpan.FromSeconds(0.25f));

        gameManager.DealerHit();
        gameManager.StateSetToDealing();

        while (gameManager.state != GameManager.State.DealerTurn)
        {
            await Task.Yield();
        }
    }
}
