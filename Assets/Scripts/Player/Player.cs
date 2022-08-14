using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    public Movement Movement { get; private set; }

    public CartCollect CartCollect { get; private set; }

    public PlayerRenderer PlayerRenderer { get; private set; }

    public Rigidbody Rigidbody { get; private set;}

    //public Animator Animator { get; private set;}
    
    //public SkinnedMeshRenderer SkinnedMeshRenderer { get; private set;}
    public MeshRenderer MeshRenderer {get; private set;}

    private void Awake(){
        Movement = GetComponent<Movement>();

        CartCollect = GetComponent<CartCollect>();

        Rigidbody = GetComponent<Rigidbody>();

        MeshRenderer = GetComponent<MeshRenderer>();
    }


}
