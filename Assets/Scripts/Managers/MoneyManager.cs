using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    public float MoneyAmount { get; set; }
    public float Multiplier { get; set; }

}
