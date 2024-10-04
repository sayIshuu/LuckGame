using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    public string attackName;

    //======원거리 일 시========/
    public GameObject projectilePrefab = null; //투사체 프리팹
    public float projectileSpeed = 10.0f;       //투사체 속도
    //===========================/

   
    public float probability = 1.0f;     //발동 확률

    public bool isMagicAttack = false;
    public bool isPhysicalAttack = false;

    public float bonusPercent = 1.0f;
}
