using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    [SerializeField] float range = 5f;  // 터렛의 범위
    private MonsterGuardManager targetMonster;
    private List<MonsterGuardManager> monstersInRange = new List<MonsterGuardManager>();  // 범위 내 몬스터 리스트
    private CircleCollider2D rangeCollider;

    void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.radius = range;   // 범위 설정

        MonsterGuardManager.OnMonsterDestroyed += OnTargetDestroyed;  // 몬스터가 파괴되었을 때 이벤트 발생
    }

    public MonsterGuardManager GetTargetMonster()
    {
        return targetMonster;
    }

    // 범위 안에 몬스터가 들어왔을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            MonsterGuardManager monster = collision.GetComponent<MonsterGuardManager>();
            if (monster != null && !monstersInRange.Contains(monster))
            {
                monstersInRange.Add(monster);  // 범위 내 몬스터 추가

                // 타겟이 없으면 새로 추가된 몬스터를 타겟으로 설정
                if (targetMonster == null)
                {
                    SetClosestTarget();
                }
            }
        }
    }

    // 범위에서 몬스터가 나갔을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            MonsterGuardManager monster = collision.GetComponent<MonsterGuardManager>();
            if (monster != null && monstersInRange.Contains(monster))
            {
                monstersInRange.Remove(monster);  // 범위 내 몬스터 제거

                // 나간 몬스터가 현재 타겟이면 새로운 타겟을 설정
                if (monster == targetMonster)
                {
                    SetClosestTarget();
                }
            }
        }
    }

    // 타겟이 파괴될 경우 호출되는 함수
    public void OnTargetDestroyed(MonsterGuardManager destroyedMonster)
    {
        if (destroyedMonster == targetMonster)
        {
            SetClosestTarget();  // 새로운 타겟을 설정
        }
    }

    // 가장 가까운 타겟을 설정하는 함수
    void SetClosestTarget()
    {
        targetMonster = null;
        float closestDistance = Mathf.Infinity;

        foreach (MonsterGuardManager monster in monstersInRange)
        {
            if (monster != null)
            {
                float distance = Vector2.Distance(transform.position, monster.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetMonster = monster;
                }
            }
        }
    }
}
