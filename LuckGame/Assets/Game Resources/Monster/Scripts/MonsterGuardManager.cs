using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGuardManager : MonoBehaviour
{
    [SerializeField] float currentHealth = 10.0f;  //현재체력
    [SerializeField] float maxHealth = 10.0f;      //최대체력
    [SerializeField] float defense = 10.0f;         //방어력
    [SerializeField] float magicResistance = 10.0f; //마법저항력

    // 유저 입장에서의 방마저는 실제 로직에서 0~1의 값으로 변환된다. 10 -> 0.9 그래서 이값이 데미지에 그대로 곱해져 감소시킨다.
    public float Defense { get { return Mathf.Max(100.0f - defense, 0.0f)*0.01f; } }
    public float MagicResistance { get { return Mathf.Max(100.0f - magicResistance, 0.0f)*0.01f; } }

    public delegate void MonsterDestroyed(MonsterGuardManager monster);
    public static event MonsterDestroyed OnMonsterDestroyed;

    //공격 받았을 때, 퍼블릭으로 선언해서 투사체 스크립트에서 호출되게 함.
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        //ShowDamageText(finalDamage); // 데미지 표시
        if (currentHealth <= 0)
        {
            Die(); // 사망 처리
        }
    }

    //사망처리
    void Die()
    {
        OnMonsterDestroyed?.Invoke(this);
        GameManager.Instance.RemoveMonsterCount();
        Destroy(gameObject);
    }
}
