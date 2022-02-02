using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Hand : MonoBehaviour
{
    public  List<Card> cards = new List<Card>();
    [SerializeField] Transform firstCard, secondCard;
    [SerializeField] Vector3 cardSize;
    [SerializeField] bool playerHand;

    public HandValue handValue;
    public GameManager gameManager;

    public void AddCard(Card card)
    {
        cards.Add(card);
        handValue.Change();

        //First 2 card
        if (cards.Count == 1) card.Dealing(firstCard.position, cards.Count, new Vector3(0, 0, firstCard.rotation.eulerAngles.z), cardSize);
        else if (cards.Count == 2)
        {
            card.doNotTurn = !playerHand;
            card.Dealing(secondCard.position, cards.Count, new Vector3(0, 0, secondCard.rotation.eulerAngles.z), cardSize);
        }

        // More than 2 cards
        else
        {
            float startPos = cards.Count * -0.25f + 0.25f;

            card.Dealing(new Vector3(startPos * -1, transform.position.y, 0), cards.Count, new Vector3(0, 0, 0), cardSize);

            for (int i = 0; i < cards.Count - 1; i++)
            {
                cards[i].Move(new Vector3(startPos, transform.position.y, 0));
                startPos += 0.5f;
            }
        }

        //Bust
        BustCheck(card);
    }
    public void Clear()
    {
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }

        cards.Clear();
        handValue.Hide();
    }

    public async void BustCheck(Card card)
    {
        if (handValue.value > 21)
        {
            await WaitForDealing(card);
            gameManager.Result();
        }
    }
    public async Task WaitForDealing(Card card)
    {
        while (!card.dealingFinished)
        {
            await Task.Yield();
        }
    }
}

