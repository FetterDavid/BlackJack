using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Hand hand;
    public int money;

    [SerializeField] private TextMeshProUGUI moneyText;

    private bool moneyIsChanging;
    private float moneyChangingValue;

    private void Start()    
    {
        money = PlayerPrefs.GetInt("money");
        if (money <= 0) money = 5000;
        moneyText.text = money.ToString();
    }

    private void FixedUpdate()
    {
        if (moneyIsChanging)
        {
            int currentMoney = Convert.ToInt32(moneyText.text);

            if (Math.Abs(currentMoney - money) > Math.Abs(moneyChangingValue))
            {
                moneyText.text = (currentMoney + moneyChangingValue).ToString();
            }
            else
            {
                moneyText.text = (money).ToString();
                moneyIsChanging = false;
            }
        }
    }

    public void ChangeMoney(int changeValue)
    {
        money += changeValue;
        PlayerPrefs.SetInt("money",money);

        moneyIsChanging = true;
        moneyChangingValue = Convert.ToInt32((money - Convert.ToInt32(moneyText.text)) / 20.0f);
    }
}
