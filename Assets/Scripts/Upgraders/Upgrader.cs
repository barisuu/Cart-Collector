using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] public float DisableDuration;
    [SerializeField] protected Material mat_Blue;
    [SerializeField] protected Material mat_Red;
    protected bool lastUpgrade;

    public MeshRenderer MeshRenderer { get; private set; }

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

}
