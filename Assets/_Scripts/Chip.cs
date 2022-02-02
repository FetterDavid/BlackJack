using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public int value;
    public GameObject chipObj;
    public Bank bank;

    public void Select()
    {
        GameObject chip = Instantiate(chipObj, transform.position, Quaternion.Euler(0, 0, 0));
        chip.transform.SetParent(bank.gameObject.transform);
        bank.AddChip(chip,value);
    }
}