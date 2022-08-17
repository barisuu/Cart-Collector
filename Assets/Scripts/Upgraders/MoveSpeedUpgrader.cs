using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpgrader : Upgrader
{
    private float _disableTimer;
    private int moneyRequired;
    private bool startTimer = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("MoveSpeedLevel") > 0)
        {
            UpgradeManager.Instance.BuildingLevel = PlayerPrefs.GetInt("MoveSpeedLevel");
        }
        moneyRequired = UpgradeManager.Instance.MoveSpeedPrices[UpgradeManager.Instance.MoveSpeedLevel];
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
            if (UpgradeManager.Instance.MoveSpeedLevel < UpgradeManager.Instance.MoveSpeedPrices.Count)
            {
                Pay();
                if (moneyRequired <= 0)
                {
                    Player.Instance.Movement.moveSpeed += 1f;
                    disableUpgrader();
                    UpgradeManager.Instance.MoveSpeedLevel++;
                    PlayerPrefs.SetInt("MoveSpeedLevel", UpgradeManager.Instance.MoveSpeedLevel);
                    PlayerPrefs.SetFloat("MoveSpeed", Player.Instance.Movement.moveSpeed);
                    if (UpgradeManager.Instance.MoveSpeedLevel >= UpgradeManager.Instance.MoveSpeedPrices.Count)
                    {
                        lastUpgrade = true;
                        gameObject.SetActive(false);
                    }
                    if (lastUpgrade == false)
                    {
                        moneyRequired = UpgradeManager.Instance.MoveSpeedPrices[UpgradeManager.Instance.MoveSpeedLevel];
                    }
                }
            }

        }
    }
}
