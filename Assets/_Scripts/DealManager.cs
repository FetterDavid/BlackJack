using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class DealManager : MonoBehaviour
{
    [SerializeField] List<Sprite> cardFaces = new List<Sprite>();
    [SerializeField] List<int> cardValues = new List<int>();
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform deckTransform;
    public Hand playerHand, dealerHand;
    [SerializeField] private List<int> deck = new List<int>();
    private int nextCardIndex;

    public GameManager gameManager;
    public UIManager uiManager;

    private void Start()
    {
        Shuffle(4);
    }

    private void Shuffle(int numberOfDecks)
    {
        System.Random rnd = new System.Random();
        deck.Clear();

        //Fill up the list with the card ids
        List<int> deck1 = new List<int>();
        for (int i = 0; i < 52; i++)
        {
            deck1.Add(i);
        }

        //Shuffel and add decks to main deck
        for (int i = 0; i < numberOfDecks; i++)
        {
            deck1 = deck1.OrderBy(item => rnd.Next()).ToList();

            foreach (var id in deck1)
            {
                deck.Add(id);
            }
        }

        //Shuffle the main deck
        deck = deck.OrderBy(item => rnd.Next()).ToList();
    }


    //-----------------------------------------------Tasks--------------------------------------------------------------------
    public async void Hit(Hand hand, bool doubleBet)
    {
        gameManager.StateSetToDealing();

        await Draw(hand);

        if (doubleBet)
        {
            gameManager.StateSetToPlayerTurn();
            gameManager.Stand();
            return;
        }

        if (hand == playerHand && playerHand.handValue.value <= 21) gameManager.state = GameManager.State.PlayerTurn;
        else gameManager.StateSetToDealerTurn();
    }
    public async void StartDraw()
    {
        gameManager.StateSetToDealing();

        await Draw(playerHand);
        await Draw(dealerHand);
        await Draw(playerHand);
        playerHand.handValue.Show();
        await Draw(dealerHand);
        dealerHand.handValue.Show(dealerHand.cards[0].value);

        if (playerHand.handValue.value == 21 || dealerHand.handValue.value == 21)
        {
            gameManager.Result();
        }
        else uiManager.StartPlayerTurn();
    }
    public async Task Draw(Hand hand)
    {
        GameObject cardObj = Instantiate(cardPrefab, deckTransform.position, deckTransform.rotation);
        Card card = cardObj.GetComponent<Card>();

        card.id = deck[nextCardIndex];
        card.value = cardValues[card.id % 13];
        card.face.sprite = cardFaces[card.id];
        hand.AddCard(card);
        nextCardIndex++;

        while (!card.dealingFinished)
        {
            await Task.Yield();
        }
    }
    public async Task TurnDealerSecondCard()
    {
        //dealerHand.handValue.Show();
        dealerHand.cards[1].doNotTurn = false;
        dealerHand.cards[1].dealingFinished = false;
        dealerHand.cards[1].Turn();

        while (!dealerHand.cards[1].dealingFinished)
        {
            await Task.Yield();
        }
    }
}
