using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCollect : MonoBehaviour
{
    [SerializeField] private List<GameObject> cartStack;
    [SerializeField] public int currentMax;  
    
    private int cartAmount;

    public int CartAmount{
        get => cartAmount; //Getter
        set{
            if(value > cartAmount){ // Increasing amount.
                if(cartAmount==cartStack.Count){ //If at full capacity return without changing anything.
                    return;
                }
                else{
                    cartAmount = value;
                    cartStack[cartAmount - 1].SetActive(true);
                }
            }
        }

    }
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Cart"){

            other.gameObject.SetActive(false);
            if(cartAmount >= currentMax){
                Player.Instance.PlayerRenderer.setFull();
            }
            else{
                CartAmount++;
            }
            

        }
    }
}
