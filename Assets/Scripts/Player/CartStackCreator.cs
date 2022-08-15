using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartStackCreator : MonoBehaviour
{
    [SerializeField] private Transform spawnTarget;
    [SerializeField] private GameObject collectable;
    [SerializeField] private float offset_x;
   // [SerializeField] private float offset_y;
    [SerializeField] private float offset_z;

    [SerializeField] private List<GameObject> collectableList;
    private List<GameObject> _closedCollectableList;

    // Start is called before the first frame update
    void Start()
    {
        _closedCollectableList = collectableList;
        //StartCoroutine(PlayerStackCreator());
        StartCoroutine(DropoffStackCreator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayerStackCreator(){
        for(var i = 0 ; i<20; i++){
            var newCollectable = Instantiate(collectable, spawnTarget);
            if(i==0){ // Create the first cart at (0,0,0)
                newCollectable.transform.localPosition = new Vector3(0,0.3f,0);
                newCollectable.name = "Cart_" + (i+1);
                collectableList.Add(newCollectable);
            }
            else{
                Debug.Log("Before getforwardpos");
                newCollectable.transform.localPosition = GetForwardPos();
                newCollectable.name = "Cart_" + (i+1);
                collectableList.Add(newCollectable);
            }
            yield return new WaitForEndOfFrame();
        }
        
    }

    private IEnumerator DropoffStackCreator()
    {
        for(var i = 0; i<20; i++)
        {
            var newCart = Instantiate(collectable, spawnTarget);
            if (i == 0)
            {
                newCart.transform.localPosition = new Vector3(0, 0.3f, 0);
                newCart.name = "Cart_" + (i+1);
                collectableList.Add(newCart);
            }
            else if(i%5==0 && i>1)
            {
                newCart.transform.localPosition = GetSidewaysPos(); // Shifting to next column
                newCart.transform.localPosition = new Vector3(newCart.transform.localPosition.x, newCart.transform.localPosition.y, 0); // Changing z to start of the previous column
                newCart.name = "Cart" + (i+1);
                collectableList.Add(newCart);
            }
            else
            {
                newCart.transform.localPosition = GetForwardPos();
                newCart.name = "Cart" + (i+1);
                collectableList.Add(newCart);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private  Vector3 GetForwardPos(){
         var prevCart = collectableList[collectableList.Count-1];
         Debug.Log("Inside forwardpos");
         Debug.Log("PrevCart = " + prevCart);
         Debug.Log("Prevcart localscale = " + prevCart.transform.localPosition);
         return new Vector3(prevCart.transform.position.x,prevCart.transform.position.y,offset_z+prevCart.transform.position.z);

    }

    private Vector3 GetSidewaysPos()
    {
        var prevCart = collectableList[collectableList.Count - 1];
        return new Vector3(prevCart.transform.position.x + offset_x, prevCart.transform.position.y, prevCart.transform.position.z);
    }
}
