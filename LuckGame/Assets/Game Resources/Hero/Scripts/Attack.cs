using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string attackName;
   
    public float probability = 1.0f;     //발동 확률

    public bool isMagicAttack = false;
    public bool isPhysicalAttack = false;

    public float bonusPercent = 1.0f;
}
