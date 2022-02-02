using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
        gameManager.DealerHit();
        gameManager.state = GameManager.State.Dealing;
        while (gameManager.state!=GameManager.State.DealerTurn)
        {
            await Task.Yield();
        }
    }
}
