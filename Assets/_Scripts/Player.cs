using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Hand hand;
    public int money;

    [SerializeField] private TextMeshProUGUI moneyText;

    public void ChangeMoney(int changeValue)
    {
        DOVirtual.Int(money, money + changeValue, 0.5f, v => { moneyText.text = v.ToString(); });
        money += changeValue;
    }
}
