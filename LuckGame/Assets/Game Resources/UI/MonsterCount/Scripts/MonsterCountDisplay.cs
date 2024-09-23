using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterCountDisplay : MonoBehaviour
{
    public TextMeshProUGUI monsterCountText;

    void OnEnable()
    {
        // GameManager의 이벤트를 구독하여 몬스터 수 변경 시 HUD를 갱신함
        GameManager.Instance.MonsterCountChanged += UpdateMonsterCount;
    }

    void OnDisable()
    {
        // 이벤트 구독 해제하여 메모리 누수 방지
        GameManager.Instance.MonsterCountChanged -= UpdateMonsterCount;
    }

    void UpdateMonsterCount(int newCount)
    {
        // HUD에 표시할 텍스트 업데이트
        monsterCountText.text = newCount.ToString() + "/ 111";
    }
}
