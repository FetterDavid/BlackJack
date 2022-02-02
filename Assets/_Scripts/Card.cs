using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer back;
    [SerializeField] float dealingSpeed, turningSpeed;
    public SpriteRenderer face;

    public int id, value;
    public bool dealingFinished, doNotTurn;
    public float time;


    public void Dealing(Vector3 pos, int orderInLayer,Vector3 rotation, Vector3 scale)
    {
        time = Vector2.Distance(transform.position, pos) / dealingSpeed;
        transform.DOMove(pos, time).OnComplete(Turn);

        transform.DOScale(scale, time);

        if (!doNotTurn) transform.DORotate(rotation, time);
        else transform.DORotate(new Vector3(0, 0, rotation.z), time); //Dealer second card not turning

        //set order in layer
        face.sortingOrder = orderInLayer;
        back.sortingOrder = orderInLayer;
    }

    public void Turn()
    {
        if (doNotTurn) dealingFinished = true;
        else transform.DORotate(new Vector3(0, 180, transform.rotation.eulerAngles.z), turningSpeed).OnComplete(() => { dealingFinished = true; });
    }

    public void Move(Vector3 pos)
    {
        transform.DOMove(pos, turningSpeed);
        transform.DORotate(new Vector3(0, 180, 0),turningSpeed);
    }
}
