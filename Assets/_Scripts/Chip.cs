using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public int value;
    public GameObject chipObj;
    public Bank bank;
    public GameManager gameManager;

    public void Select()
    {

        if (gameManager.state == GameManager.State.StartOfHand && bank.player.money >= value)
        {
            GameObject chip = Instantiate(chipObj, transform.position, Quaternion.Euler(0, 0, 0));
            chip.transform.SetParent(bank.gameObject.transform);
            chip.GetComponent<ChipInBank>().value = value;
            chip.GetComponent<ChipInBank>().parentChipPos = transform.position;
            chip.GetComponent<ChipInBank>().bank = bank;
            bank.AddChip(chip, value);
        }
    }
}
