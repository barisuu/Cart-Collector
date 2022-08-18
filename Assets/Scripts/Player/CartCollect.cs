using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCollect : MonoBehaviour
{
    [SerializeField] public List<GameObject> cartStack;
    [SerializeField] public int currentMax;
    private static readonly int hasCart = Animator.StringToHash("hasCart");

    private int _cartAmount;

    public int CartAmount{
        get => _cartAmount; //Getter
        set{
            if(value > _cartAmount){ // Increasing amount.
                if(_cartAmount==currentMax){ //If at full capacity return without changing anything.
                    return;
                }
                else{
                    _cartAmount = value;
                    cartStack[_cartAmount - 1].SetActive(true);
                }
                if (_cartAmount >= currentMax) //If at or over max reset to max and set player to max visual.
                {
                    _cartAmount = currentMax;
                    //Player.Instance.PlayerRenderer.setFull();
                }
            }
            else
            {
                if (_cartAmount <= 0)
                {
                    return;
                }
                _cartAmount = value;
                if(_cartAmount <= 0)
                {
                    _cartAmount = 0;
                    Player.Instance.Animator.SetBool(hasCart, false);
                }
                cartStack[_cartAmount].SetActive(false);
            }
        }

    }
 
    // Start is called before the first frame update
    void Start()
    { 
        if (PlayerPrefs.GetInt("CurrentMax")>0)
        {
            currentMax = PlayerPrefs.GetInt("CurrentMax");
        }
        else
        {
            currentMax = 3;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Cart"){
            if(CartAmount < currentMax)
            {
                other.gameObject.SetActive(false);
                CartAmount++;
                Player.Instance.Animator.SetBool(hasCart, true);
            }
        }
    }
}
