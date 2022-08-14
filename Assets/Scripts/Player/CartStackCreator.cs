using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartStackCreator : MonoBehaviour
{
    [SerializeField] private Transform spawnTarget;
    [SerializeField] private GameObject collectable;

    [SerializeField] private List<GameObject> collectableList;
    private List<GameObject> _closedCollectableList;

    // Start is called before the first frame update
    void Start()
    {
        _closedCollectableList = collectableList;
        StartCoroutine(Creator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Creator(){
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

    private  Vector3 GetForwardPos(){
         var prevCart = collectableList[collectableList.Count-1];
         Debug.Log("Inside forwardpos");
         Debug.Log("PrevCart = " + prevCart);
         Debug.Log("Prevcart localscale = " + prevCart.transform.localScale);
         return new Vector3(prevCart.transform.position.x,prevCart.transform.position.y,(prevCart.transform.position.z)+prevCart.transform.localScale.z/2);

    }
}
