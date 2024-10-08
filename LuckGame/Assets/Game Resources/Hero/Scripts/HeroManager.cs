using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    //public float BuffMultiplier = 0.0f;           // 최대 체력에 비례한 버프 비율

    [SerializeField] bool isShortRangeHero = false; // 근거리 영웅인지 여부
    public float attackPower = 10.0f;               //공격력
    [SerializeField] List<Attack> attackList;       // 스킬들
    public Attack currentAttack;                    // 현재 쓰려는 스킬

    // 인터벌로 관리하면 추후 시전시간이 긴 스킬이 추가되도 쉽게 관리 가능
    [SerializeField] float attackSpeed = 1.0f;      // 공격 속도
    private float attackInterval;   // 공격 속도

    // 애니메이션 관련 변수
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    // 타게팅
    private TargetingManager targetingManager;
    private MonsterGuardManager targetMonster;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        targetingManager = GetComponent<TargetingManager>();
        currentAttack = attackList[0]; // 초기 스킬 설정
        attackInterval = 1.0f / attackSpeed;
        StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        FlipHero();
    }


    // X포지션 차이를 가지고 타겟몬스터를 바라보게 함
    void FlipHero()
    {
        if (targetMonster != null)
        {
            if (targetMonster.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            targetMonster = targetingManager.GetTargetMonster();
            if (targetMonster != null)
            {
                LaunchAttack(targetMonster);
            }
        }
    }

    // 총 데미지 계산
    float CalculateTotalDamage(MonsterGuardManager _targetMonster)
    {
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
        return totalDamage;
    }

    // 운빨로 평타 or 스킬 발동
    void ChoiceRandomCurrentAttack()
    {
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
    }

    public void LaunchAttack(MonsterGuardManager _targetMonster)
    {
        // 공격 애니메이션
        animator.SetTrigger("Attack");

        // 운빨로 스킬 선택
        ChoiceRandomCurrentAttack();

        // 총 데미지 계산
        float totalDamage = CalculateTotalDamage(_targetMonster);

        // 공격 실행
        if (isShortRangeHero)
        {
            // 근거리 공격
            _targetMonster.TakeDamage(totalDamage);
        }
        else
        {
            // 원거리 공격
            GameObject projectile = Instantiate(currentAttack.projectilePrefab, transform.position, Quaternion.identity, this.transform);
            projectile.GetComponent<Projectile>().Initialize(targetMonster, totalDamage, currentAttack.projectileSpeed);
        }
    }
}
