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
        dealer.hand.handValue.Show();

        if (player.hand.handValue.value > 21) Bust();
        else if ((player.hand.handValue.value > dealer.hand.handValue.value && dealer.hand.handValue.value <= 21)
            || dealer.hand.handValue.value > 21) Win();
        else if (player.hand.handValue.value == dealer.hand.handValue.value) Push();
        else Lose();
    }
    public void Win()
    {
        player.ChangeMoney(bank.bet * 2);

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
        player.ChangeMoney(bank.bet);

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
        if (state == State.StartOfHand && bank.lastBet != 0)
        {
            //if (bank.bet == 0) bank.DealWithTheLastBet();//Last Bet Aotomatic add

            dealManager.StartDraw();
            uiManager.dealButton.transform.DOMoveX(uiManager.buttonHidePos.position.x, 0.5f);
        }
    }
    public void PlayerHit()
    {
        if (state == State.PlayerTurn)
            dealManager.Hit(dealManager.playerHand, false);
    }
    public void DoubleBet()
    {
        if (player.money >= bank.lastBet)
        {
            bank.DealWithTheLastBet();
            dealManager.Hit(dealManager.playerHand, true);
        }
    }
    public void Stand()
    {
        if (state == State.PlayerTurn)
        {
            state = State.DealerTurn;
            dealer.hand.handValue.Show();
            dealer.DealerTurn();
        }
    }
    public void DealerHit()
    {
        if (state == State.DealerTurn)
        {
            dealManager.Hit(dealer.hand, false);
        }
    }
    public void HandsClear()
    {
        player.hand.Clear();
        dealer.hand.Clear();
    }

    //----------------------------------------------State Changes------------------------------------------------------------

    public void StateSetToStartOfHand()
    {
        state = State.StartOfHand;
        bank.DealWithTheLastBet();
    }
    public void StateSetToDealing()
    {
        state = State.Dealing;
    }
    public void StateSetToPlayerTurn()
    {
        state = State.PlayerTurn;
    }
    public void StateSetToDealerTurn(int delay = 0)
    {
        state = State.DealerTurn;
    }
    public void StateSetToEndOfHand()
    {
        state = State.EndOfHand;
    }
}
