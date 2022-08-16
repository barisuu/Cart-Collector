using System;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        EventManager.CollectMoney += SetMoneyText;
    }

    private void OnDisable()
    {
        EventManager.CollectMoney -= SetMoneyText;
    }

    private void SetMoneyText(object sender, EventArgs e)
    {
        moneyText.text = MoneyManager.Instance.MoneyAmount.ToString();
        PlayerPrefs.SetString("CharacterMoney", moneyText.text);
    }

    private void Start()
    {
        MoneyManager.Instance.MoneyAmount = int.Parse(PlayerPrefs.GetString("CharacterMoney"));
        moneyText.text = PlayerPrefs.GetString("CharacterMoney") ;
    }
}

