using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGuard : MonoBehaviour
{
    [SerializeField] float currentHealth = 10.0f;  //현재체력
    [SerializeField] float maxHealth = 10.0f;      //최대체력
    [SerializeField] float defense = 1.0f;         //방어력
    [SerializeField] float magicResistance = 1.0f; //마법저항력

    //물리공격 받았을 때, 퍼블릭으로 선언해서 투사체 스크립트에서 호출되게 함.
    public void TakePhysicalDamage(float damageAmount)
    {
        float finalDamage = Mathf.Max(0, damageAmount - defense); // 방어력 계산
        currentHealth -= finalDamage;
        //ShowDamageText(finalDamage); // 데미지 표시
        if (currentHealth <= 0)
        {
            Die(); // 사망 처리
        }
    }

    //마법공격 받았을 때, 퍼블릭으로 선언해서 투사체 스크립트에서 호출되게 함.
    public void TakeMagicDamage(float damageAmount)
    {
        float finalDamage = Mathf.Max(0, damageAmount - magicResistance); // 마법저항력 계산
        currentHealth -= finalDamage;
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
        Destroy(gameObject);
    }
}
