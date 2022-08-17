using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [SerializeField] public List<int> BuildingPrices;
    [SerializeField] public List<int> MoveSpeedPrices;
    [SerializeField] public List<int> CarryAmountPrices;

    public int BuildingLevel { get; set; }
    public int MoveSpeedLevel { get; set; }
    public int CarryAmountLevel { get; set; }

}
