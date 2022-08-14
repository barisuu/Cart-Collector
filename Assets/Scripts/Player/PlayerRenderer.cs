using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : Player
{

    [SerializeField] private Material mat_default;
    [SerializeField] private Material mat_full;

    public void setFull(){
        Player.Instance.MeshRenderer.material=mat_full;
    }

    public void setDefault(){
        Player.Instance.MeshRenderer.material=mat_default;
    }

}
