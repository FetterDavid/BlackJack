using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] List<Transform> chipPositions = new List<Transform>();
    [SerializeField] GameObject betObj, chipHolder;
    [SerializeField] TextMeshProUGUI betText;
    [SerializeField] Transform playerWinHolderPos, delaerWinHolderPos;
    [SerializeField] Chip chip25, chip50, chip100, chip250, chip500;

    public int bet, lastBet;
    public  Player player;

    private List<GameObject> chips = new List<GameObject>();

    public void AddChip(GameObject chipObj, int chipValue)
    {
        if (bet == 0)
        { 
            betObj.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        }

        chips.Add(chipObj);

        player.ChangeMoney(chipValue * -1);
        bet += chipValue;
        lastBet = bet;
        betText.text = bet.ToString();

        chipObj.transform.DOMove(chipPositions[Random.Range(0, chipPositions.Count - 1)].position, 0.5f).OnComplete(() =>
        chipObj.transform.SetParent(chipHolder.transform));
        chipObj.transform.DOScale(new Vector3(0.66f, 0.66f, 1), 0.5f);
    }
    public void RemoveChip(ChipInBank chipInBank)
    {
        chips.Remove(chipInBank.gameObject);

        player.ChangeMoney(chipInBank.value);
        bet -= chipInBank.value;
        lastBet = bet;
        betText.text = bet.ToString();

        if (bet == 0)
        {
            betObj.transform.DOScale(new Vector3(0, 0, 1), 0.2f);
        }

        chipInBank.gameObject.transform.DOMove(chipInBank.parentChipPos, 0.5f).OnComplete(() =>
        Destroy(chipInBank.gameObject));
        chipInBank.gameObject.transform.DOScale(new Vector3(0.66f, 0.66f, 1), 0.5f);
    }
    public void PlayerGetTheChips()
    {
        chipHolder.transform.DOMove(playerWinHolderPos.position, 0.3f).OnComplete(ChipHolderReset);
    }
    public void DealerGetTheChips()
    {
        chipHolder.transform.DOMove(delaerWinHolderPos.position, 0.3f).OnComplete(ChipHolderReset);
    }
    private void ChipHolderReset()
    {
        foreach (var chip in chips)
        {
            Destroy(chip);
        }

        chipHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -610);

        bet = 0;
        betText.text = "";
        betObj.transform.localScale = new Vector3(0, 0, 0);
    }

    public void DealWithTheLastBet()
    {
        float remainBet = lastBet;

        while (remainBet/500.0f>=1)
        {
            chip500.Select();
            remainBet -= 500;
        }

        while (remainBet / 250.0f >= 1)
        {
            chip250.Select();
            remainBet -= 250;
        }

        while (remainBet / 100.0f >= 1)
        {
            chip100.Select();
            remainBet -= 100;
        }

        while (remainBet / 50.0f >= 1)
        {
            chip50.Select();
            remainBet -= 50;
        }

        while (remainBet / 25.0f >= 1)
        {
            chip25.Select();
            remainBet -= 25;
        }
    }
}
