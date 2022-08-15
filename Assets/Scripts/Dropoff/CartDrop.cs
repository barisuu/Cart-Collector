using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> dropoffStack;
    private bool canTakeCart;

    private int _dropoffStackAmount=0;

    public int DropoffStackAmount{
        get => _dropoffStackAmount;

        set{
            if(value>_dropoffStackAmount){ //Increasing amount.
                if(value>=dropoffStack.Count){ //If at full capacity return without changing anything.
                    return;
                }
                else
                {
                    _dropoffStackAmount = value;
                    Debug.Log("Dropoffstackamount: " +_dropoffStackAmount);
                    dropoffStack[_dropoffStackAmount-1].SetActive(true);
                }
                
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
        if (canTakeCart == true && Player.Instance.CartCollect.CartAmount>0)
        {
            Player.Instance.CartCollect.CartAmount--;
            DropoffStackAmount++;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Player.Instance.CartCollect.CartAmount>0)
        {
                canTakeCart = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                canTakeCart = false;
        }
    }

}
