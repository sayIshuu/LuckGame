using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    // 플레이어의 공격 종류. 추후 추가 예정.
    //public List<Attack> Attacks; // 플레이어가 사용할 수 있는 여러 공격 종류
    //public Attack currentAttack; // 현재 선택된 공격
    //public float BuffMultiplier = 0.0f; // 최대 체력에 비례한 버프 비율

    //인터벌로 관리하면 추후 시전시간이 긴 스킬이 추가되도 쉽게 관리 가능
    [SerializeField] float attackInterval = 1.0f; // 공격 속도
    [SerializeField] bool isShortRangeHero = false; // 근거리 영웅인지 여부

    //공격력
    public float attackPower = 10.0f;

    public List<Attack> attackList; // 스킬들
    public Attack currentAttack; // 현재 스킬

    private TargetingManager targetingManager;

    private void Start()
    {
        targetingManager = GetComponent<TargetingManager>();
        currentAttack = attackList[0]; // 초기 스킬 설정
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            LaunchAttack();
        }
    }

    public void LaunchAttack(MonsterGuardManager _targetMonster)
    {
        // 운빨로 스킬 선택
        float randomValue = Random.Range(0.0f, 1.0f);
        float cumulativeProbability = 0.0f;

        foreach (Attack attack in attackList)
        {
            // 단, 스킬 확률 총합이 1이 되도록 설정해야 함.
            cumulativeProbability += attack.probability;
            if (randomValue <= cumulativeProbability)
            {
                currentAttack = attack;
                break;
            }
        }

        // 총 데미지 계산
        float totalDamage = 0.0f;
        if (currentAttack.isPhysicalAttack)
        {
            totalDamage = attackPower * currentAttack.bonusPercent * 
                    _targetMonster.Defense; // 물리 공격
        }
        else if (currentAttack.isMagicAttack)
        {
            totalDamage = attackPower * currentAttack.bonusPercent * 
                    _targetMonster.MagicResistance; // 마법 공격
        }

        // 공격 실행
        if (isShortRangeHero)
        {
            // 근거리 공격
            _targetMonster.TakeDamage(totalDamage);
        }
        else
        {
            // 원거리 공격
            // 공격 실행
        }
    }
}
