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
    public int value1, value2;

    public void Change()
    {
        value1 = 0;
        value2 = 0;

        for (int i = 0; i < hand.cards.Count; i++)
        {
            if (hand.cards[i].value == 11)
            {
                if (value2 + 11 < 21) value2 += 11;
                else value2 = value1 + 11;
                value1++;
            }
            else {
                value1 += hand.cards[i].value;
                value2 += hand.cards[i].value;
            }
        }

        if (value2 <= 21) value = value2;
        else value = value1;

        //value = value2 <= 21 ? value2 : value1;
        valueText.text = value.ToString();
    }

    public void Show(int temp = 0)
    {
        if(temp!=0) valueText.text=temp.ToString();
        else valueText.text = value.ToString();

        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }
    public void Hide()
    {
        transform.localScale = new Vector3(0, 0, 0);
        value = 0;
    }
}
