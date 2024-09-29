using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private MonsterGuardManager targetMonster;
    private float damage;
    private float speed;

    public void Initialize(MonsterGuardManager target, float damageAmount, float projectileSpeed)
    {
        targetMonster = target;
        damage = damageAmount;
        speed = projectileSpeed;
    }

    void Update()
    {
        if (targetMonster == null)
        {
            Destroy(gameObject); // 몬스터가 사라지면 투사체도 제거
            return;
        }

        // 몬스터를 향해 투사체 이동
        transform.position = Vector3.MoveTowards(transform.position, targetMonster.transform.position, speed * Time.deltaTime);

        // 몬스터에 도착하면 데미지 처리
        if (Vector3.Distance(transform.position, targetMonster.transform.position) < 0.1f)
        {
            targetMonster.TakeDamage(damage);
            Destroy(gameObject); // 투사체 제거
        }
    }
}
