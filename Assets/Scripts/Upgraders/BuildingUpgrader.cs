using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgrader : Upgrader
{
    [SerializeField] public List<GameObject> MarketList;
    private float _disableTimer;
    private int moneyRequired;
    private bool startTimer = false;

    private void Start()
    {
        UpgradeManager.Instance.BuildingLevel = 0;
        if (PlayerPrefs.GetInt("BuildingLevel") > 0)
        {
            UpgradeManager.Instance.BuildingLevel = PlayerPrefs.GetInt("BuildingLevel");
            MarketList[0].SetActive(false);
            if (UpgradeManager.Instance.BuildingLevel >= UpgradeManager.Instance.BuildingPrices.Count)
            {
                lastUpgrade = true;
                this.transform.parent.gameObject.SetActive(false);
            }
        }
        MarketList[UpgradeManager.Instance.BuildingLevel].SetActive(true);
        moneyRequired = UpgradeManager.Instance.BuildingPrices[UpgradeManager.Instance.BuildingLevel];
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
        if (other.gameObject.CompareTag("Player") && MoneyManager.Instance.MoneyAmount>0) // If player has money start paying for the upgrade.
        {
            if (UpgradeManager.Instance.BuildingLevel < UpgradeManager.Instance.BuildingPrices.Count)
            {
                Pay();
                if (moneyRequired <= 0)
                {
                    disableUpgrader();
                    MarketList[UpgradeManager.Instance.BuildingLevel].SetActive(false);
                    UpgradeManager.Instance.BuildingLevel++;
                    MarketList[UpgradeManager.Instance.BuildingLevel].SetActive(true);
                    PlayerPrefs.SetInt("BuildingLevel", UpgradeManager.Instance.BuildingLevel);
                    if(UpgradeManager.Instance.BuildingLevel >= UpgradeManager.Instance.BuildingPrices.Count)
                    {
                        lastUpgrade = true;
                        this.transform.parent.gameObject.SetActive(false);
                    }
                    if(lastUpgrade == false)
                    {
                        moneyRequired = UpgradeManager.Instance.BuildingPrices[UpgradeManager.Instance.BuildingLevel];
                    }
                }
            }

        }
    }


}
