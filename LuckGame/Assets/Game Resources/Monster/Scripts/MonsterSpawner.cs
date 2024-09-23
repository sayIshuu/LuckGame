using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    // 생성할 프리팹 오브젝트
    [SerializeField] GameObject monsterPrefab;
    // 생성할 위치
    [SerializeField] Vector2 spawnPosition;
    // 생성 부모 오브젝트
    [SerializeField] Transform parentTransform;
    // 생성 간격 (초 단위)
    [SerializeField] float spawnInterval = 0.5f;
    // 생성할 횟수
    [SerializeField] int spawnCount = 40;

    // 임시 스폰 버튼 -> 추후 시간에 따라 자동으로 생성하도록 수정
    public Button spawnButton;

    void Start()
    {
        // 버튼 클릭 이벤트에 스폰 함수를 등록
        spawnButton.onClick.AddListener(StartSpawning);
    }

    // 스폰을 시작하는 함수
    void StartSpawning()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 프리팹 생성
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, parentTransform);
            // 생성된 몬스터 수 증가
            GameManager.Instance.AddMonsterCount();

            // 지정된 시간만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
