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

        // 몬스터를 바라보게 설정 (2D에서 LookAt 방식)
        Vector3 direction = targetMonster.transform.position - transform.position;
        transform.right = direction; // 투사체의 오른쪽 방향이 몬스터를 향하도록 설정


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
