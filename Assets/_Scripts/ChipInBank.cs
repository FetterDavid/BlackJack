using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipInBank : MonoBehaviour
{
    public int value;
    public Vector2 parentChipPos;
    public Bank bank;

    public void Select()
    {
        bank.RemoveChip(this);
    }
}
