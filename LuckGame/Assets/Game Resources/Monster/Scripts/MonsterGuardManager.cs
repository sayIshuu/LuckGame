using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGuardManager : MonoBehaviour
{
    [SerializeField] float currentHealth = 10.0f;  //현재체력
    [SerializeField] float maxHealth = 10.0f;      //최대체력
    [SerializeField] float defense = 10.0f;         //방어력
    [SerializeField] float magicResistance = 10.0f; //마법저항력

    public float Defense { get { return (100.0f - defense)*0.01f; } }
    public float MagicResistance { get { return (100.0f - magicResistance)*0.01f; } }

    public delegate void MonsterDestroyed(MonsterGuardManager monster);
    public static event MonsterDestroyed OnMonsterDestroyed;

    //물리공격 받았을 때, 퍼블릭으로 선언해서 투사체 스크립트에서 호출되게 함.
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
        //사망처리
        OnMonsterDestroyed?.Invoke(this);
        GameManager.Instance.RemoveMonsterCount();
        Destroy(gameObject);
    }
}
