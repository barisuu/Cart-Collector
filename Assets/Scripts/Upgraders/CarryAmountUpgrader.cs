using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryAmountUpgrader : Upgrader
{
    private float _disableTimer;
    private int moneyRequired;
    private bool startTimer = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("CarryAmountLevel") > 0)
        {
            UpgradeManager.Instance.CarryAmountLevel = PlayerPrefs.GetInt("CarryAmountLevel");
            if (UpgradeManager.Instance.CarryAmountLevel >= UpgradeManager.Instance.CarryAmountPrices.Count)
            {
                lastUpgrade = true;
                this.transform.parent.gameObject.SetActive(false);
            }
        }
        moneyRequired = UpgradeManager.Instance.CarryAmountPrices[UpgradeManager.Instance.CarryAmountLevel];
        _disableTimer = DisableDuration;
    }

    private void Update()
    {
        if (lastUpgrade == false) //If not at last upgrade level start the timer to re-enable collision.
        {
            if (startTimer == true)
            {
                if (_disableTimer <= 0)
                {
                    MeshRenderer.material = mat_Blue;
                    GetComponent<BoxCollider>().enabled = true;
                    startTimer = false;
                    _disableTimer = DisableDuration;
                }
                else
                {
                    _disableTimer -= Time.deltaTime;
                }
            }
        }


    }

    private void disableUpgrader()
    {
        GetComponent<BoxCollider>().enabled = false;
        MeshRenderer.material = mat_Red;
        startTimer = true;
    }

    private void Pay()
    {
        if (moneyRequired <= 0)
        {
            return;
        }
        if (moneyRequired > 0)
        {
            MoneyManager.Instance.MoneyAmount--;
            EventManager.OnCollectMoney();
            moneyRequired--;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && MoneyManager.Instance.MoneyAmount > 0) // If player has money start paying for the upgrade.
        {
            if (UpgradeManager.Instance.CarryAmountLevel < UpgradeManager.Instance.CarryAmountPrices.Count)
            {
                Pay();
                if (moneyRequired <= 0)
                {
                    Player.Instance.CartCollect.currentMax += 2;
                    disableUpgrader();
                    UpgradeManager.Instance.CarryAmountLevel++;
                    PlayerPrefs.SetInt("CarryAmountLevel", UpgradeManager.Instance.CarryAmountLevel);
                    PlayerPrefs.SetInt("CurrentMax", Player.Instance.CartCollect.currentMax);
                    if (UpgradeManager.Instance.CarryAmountLevel >= UpgradeManager.Instance.CarryAmountPrices.Count)
                    {
                        lastUpgrade = true;
                        this.transform.parent.gameObject.SetActive(false);
                    }
                    if (lastUpgrade == false)
                    {
                        moneyRequired = UpgradeManager.Instance.CarryAmountPrices[UpgradeManager.Instance.CarryAmountLevel];
                    }
                }
            }

        }
    }
}
