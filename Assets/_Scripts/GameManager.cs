using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] Dealer dealer;
    [SerializeField] Player player;

    public enum State {StartOfHand ,Dealing, PlayerTurn, DealerTurn, EndOfHand};
    public State state;
    public DealManager dealManager;
    public UIManager uiManager;
    public Bank bank;

    //----------------------------------------------Results----------------------------------------------------------------
    public async void Result()
    {
        state = State.EndOfHand;

        if(dealer.hand.cards[1].doNotTurn) await dealManager.TurnDealerSecondCard();

        if (player.hand.handValue.value > 21) Bust();
        else if ((player.hand.handValue.value > dealer.hand.handValue.value && dealer.hand.handValue.value <= 21)
            || dealer.hand.handValue.value > 21) Win();
        else if (player.hand.handValue.value == dealer.hand.handValue.value) Push();
        else Lose();
    }
    public void Win()
    {
        uiManager.ShowResult(uiManager.winObj);
        bank.PlayerGetTheChips();
    }
    public void Bust()
    {    
        uiManager.ShowResult(uiManager.bustObj);
        bank.DealerGetTheChips();
    }
    public void Push()
    {
        uiManager.ShowResult(uiManager.pushObj);
        bank.PlayerGetTheChips();
    }
    public void Lose()
    {
        uiManager.ShowResult(uiManager.loseObj);
        bank.DealerGetTheChips();
    }

    //----------------------------------------------Actions----------------------------------------------------------------
    public void Deal()
    {
        if (state == State.StartOfHand)
        {
            dealManager.StartDraw();
            uiManager.dealButton.transform.DOMoveX(uiManager.buttonHidePos.position.x, 0.5f);
        }
    }
    public void PlayerHit()
    {
        if (state == State.PlayerTurn)
            dealManager.Hit(dealManager.playerHand);
    }
    public void Stand()
    {
        if (state == State.PlayerTurn)
        {
            state = State.DealerTurn;
            dealer.DealerTurn();
        }
    }
    public void DealerHit()
    {
        if (state == State.DealerTurn)
        {
            dealManager.Hit(dealer.hand);
        }
    }
    public void HandsClear()
    {
        player.hand.Clear();
        dealer.hand.Clear();
    }
}
