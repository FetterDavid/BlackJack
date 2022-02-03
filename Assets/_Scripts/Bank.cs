using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] List<Transform> chipPositions = new List<Transform>();
    [SerializeField] GameObject valueObj, chipHolder;
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] Transform playerWinHolderPos, delaerWinHolderPos;
    [SerializeField] Player player;

    public int value;

    private List<GameObject> chips = new List<GameObject>();

    public void AddChip(GameObject chipObj, int chipValue)
    {
        if (value == 0)
        { 
            valueObj.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        }

        chips.Add(chipObj);

        player.ChangeMoney(chipValue * -1);
        value += chipValue;
        valueText.text = value.ToString();

        chipObj.transform.DOMove(chipPositions[Random.Range(0, chipPositions.Count - 1)].position, 0.5f).OnComplete(() =>
        chipObj.transform.SetParent(chipHolder.transform));
        chipObj.transform.DOScale(new Vector3(0.66f, 0.66f, 1), 0.5f);
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

        value = 0;
        valueText.text = "";
        valueObj.transform.localScale = new Vector3(0, 0, 0);
    }
}
