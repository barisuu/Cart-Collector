using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> dropoffStack;
    [SerializeField] private float ConversionTime;
    [SerializeField] private float TakingTime;

    private int _dropoffStackAmount = 0;
    private float _deactivateTimer;
    public int DropoffStackAmount
    {
        get => _dropoffStackAmount;

        set
        {
            if (value > _dropoffStackAmount)
            { //Increasing amount.
                if (value >= dropoffStack.Count)
                { //If at full capacity return without changing anything.
                    return;
                }
                else
                {
                    _dropoffStackAmount = value;
                    dropoffStack[_dropoffStackAmount - 1].SetActive(true);
                    Debug.Log("Amount : " + _dropoffStackAmount);
                }

            }
            else if(value < _dropoffStackAmount)
            {
                if(_dropoffStackAmount <= 0)
                {
                    return;
                }
                _dropoffStackAmount = value;
                if(_dropoffStackAmount <= 0)
                {
                    _dropoffStackAmount = 0;
                }
                dropoffStack[_dropoffStackAmount].SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _dropoffStackAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DropoffStackAmount > 0)
        {
            if (_deactivateTimer <= 0)
            {
                CalculateMoney();
                DeactivateCart();
            }
            else
            {
                _deactivateTimer -= Time.deltaTime;
            }
        }
        
    }

    private void CalculateMoney()
    {
        MoneyManager.Instance.MoneyAmount += MoneyManager.Instance.Multiplier * 1;
        EventManager.OnCollectMoney();
    }

    private void ActivateCart()
    {
        if (_dropoffStackAmount >= dropoffStack.Count)
        {
            _dropoffStackAmount = dropoffStack.Count;
            return;
        }
        DropoffStackAmount++;
        Player.Instance.CartCollect.CartAmount--;
    }

    private void DeactivateCart()
    {
        if(_dropoffStackAmount <= 0)
        {
            MoneyManager.Instance.Multiplier = 1; // Reset the multiplier back to 1 when there are no more carts in the dropoff point.
            _dropoffStackAmount = 0;
            return;
        }
            DropoffStackAmount--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") ||other.gameObject.CompareTag("StackCart"))
        {
            if (Player.Instance.CartCollect.CartAmount > 0)
            {
                ActivateCart();
                _deactivateTimer = ConversionTime;
                if (DropoffStackAmount >= 3)
                {
                    MoneyManager.Instance.Multiplier = 2f;
                    if (DropoffStackAmount >= 5)
                    {
                        MoneyManager.Instance.Multiplier = 4f;
                        if (DropoffStackAmount >= 10)
                        {
                            MoneyManager.Instance.Multiplier = 8f;
                            if (DropoffStackAmount >= 15)
                            {
                                MoneyManager.Instance.Multiplier = 10f;
                            }
                        }
                    }
                }
            }
        }
    }

}