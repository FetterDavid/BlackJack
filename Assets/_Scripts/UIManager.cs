using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject winObj, loseObj, pushObj, bustObj;
    public GameObject dealButton, hitButton, standButton;
    public GameManager gameManager;

    [SerializeField] public Transform buttonPos, buttonHidePos;

    public void ShowResult(GameObject resultObj)
    {
        resultObj.SetActive(true);
        resultObj.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 400, 0), 1).OnComplete(() => StartOfHand(resultObj));
    }

    public void StartOfHand(GameObject resultObj)
    {
        gameManager.state = GameManager.State.StartOfHand;
        gameManager.HandsClear();

        resultObj.SetActive(false);
        resultObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

        dealButton.transform.DOMoveX(buttonPos.position.x, 0.5f);

        hitButton.transform.DOMoveX(buttonHidePos.position.x, 0.5f);
        standButton.transform.DOMoveX(buttonHidePos.position.x, 0.5f);
    }

    public void StartPlayerTurn()
    {
        gameManager.state = GameManager.State.PlayerTurn;

        hitButton.transform.DOMoveX(buttonPos.position.x, 0.5f);
        standButton.transform.DOMoveX(buttonPos.position.x, 0.5f);
    }
}
