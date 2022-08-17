using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarMove : MonoBehaviour
{
    [SerializeField] List<GameObject> Waypoints;
    

    //Sequence mySequence = DOTween.Sequence();
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] myWaypoints = new Vector3[Waypoints.Count];
        for (var i = 0; i < Waypoints.Count; i++)
        {
            Debug.Log(Waypoints[i].transform.position);
            myWaypoints[i]=(Waypoints[i].transform.position);
        }
        CarLeave(myWaypoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CarLeave(Vector3[] waypointArray)
    {
        Sequence mySequence = DOTween.Sequence();
        //transform.DOMoveX(1.7f, 5);
        transform.DOPath(waypointArray, 35, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.1f,new Vector3(-1,0,0), Vector3.up);


    }
}
