using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> dropoffStack;

    private int _dropoffStackAmount;

    public int DropoffStackAmount{
        get => _dropoffStackAmount;

        set{
            if(value>_dropoffStackAmount){ //Increasing amount.
                if(value==dropoffStack.Count){ //If at full capacity return without changing anything.
                    return;
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
        
    }

}
