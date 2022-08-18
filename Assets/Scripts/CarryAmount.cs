using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarryAmount : MonoBehaviour
{
    private TextMeshPro carryText;
    private int amountCarried;
    private int currentMax;

    private void Start()
    {
        carryText = gameObject.GetComponent<TextMeshPro>();
        currentMax = Player.Instance.CartCollect.currentMax;
        amountCarried = Player.Instance.CartCollect.CartAmount;
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(Player.Instance.gameObject.transform.position.x, gameObject.transform.position.y, Player.Instance.gameObject.transform.position.z);
        if (currentMax != Player.Instance.CartCollect.currentMax) //Update the max value in this class when it changes in Player class.
        {
            currentMax = Player.Instance.CartCollect.currentMax;
            carryText.text = amountCarried + "/" + currentMax;
        }
        if (amountCarried != Player.Instance.CartCollect.CartAmount)
        {
            amountCarried = Player.Instance.CartCollect.CartAmount;
            carryText.text = amountCarried + "/" + currentMax;
        }
        
        if (Player.Instance.CartCollect.isFull == true)
        {
            carryText.faceColor = Color.red;
            carryText.outlineColor = Color.black;
            carryText.text = "MAX!";
        }
        else
        {
            carryText.faceColor = Color.white;
            carryText.outlineColor = new Color32(5, 68, 131, 255);
        }
        
    }
}
