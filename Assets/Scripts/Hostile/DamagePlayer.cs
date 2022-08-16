using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        if(other.tag=="Player" || other.tag=="StackCart")
            Player.Instance.CartCollect.CartAmount--;
    }
}
