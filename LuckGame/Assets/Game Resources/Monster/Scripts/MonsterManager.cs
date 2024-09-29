using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private MonsterMovementManager monsterMovementManager;
    private MonsterGuardManager monsterGuardManager;

    void Start()
    {
        monsterMovementManager = GetComponent<MonsterMovementManager>();
        monsterGuardManager = GetComponent<MonsterGuardManager>();
    }

    // 오버로딩 함수 (예정)
    public void LaunchedAttack(float damage//,
                               //float defenceModifier,
                               //float magicResistenceModifier,
                               /*float speedModifier*/)
    {
        monsterGuardManager.TakeDamage(damage);
    }
    
}
