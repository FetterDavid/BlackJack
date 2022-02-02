using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HandValue : MonoBehaviour
{
    [SerializeField] private Hand hand;
    [SerializeField] private TextMeshProUGUI valueText;

    public int value = 0;

    public void Change()
    {
        int newValue = 0;
        for (int i = 0; i < hand.cards.Count; i++)
        {
            newValue += hand.cards[i].value;
        }

        value = newValue;
        valueText.text = newValue.ToString();
    }

    public void Show()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }
    public void Hide()
    {
        transform.localScale = new Vector3(0, 0, 0);
        value = 0;
    }
}
