using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonsterRotate : MonoBehaviour
{
    [SerializeField] bool isFlip = false; // 스프라이트 flip 여부
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterMovementManager monster = other.GetComponent<MonsterMovementManager>();
            if (monster != null)
            {
                monster.ChangeDirection(isFlip);
            }
        }
    }
}
