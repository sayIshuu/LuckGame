using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int monsterCount = 0;

    // 인스턴스 접근
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).Name);
                    _instance = singleton.AddComponent<GameManager>();
                    DontDestroyOnLoad(singleton);  // 씬이 변경되어도 삭제되지 않음
                }
            }
            return _instance;
        }
    }

    /* Awake가 작동하면 게임 시작과 동시에 GameManager오브젝트가 사라진다. 원인 찾기전까지 주석처리. 어차피 씬변경전엔 필요없는 코드
    // Awake는 MonoBehaviour에서 호출되는 생명주기 함수
    private void Awake()
    {
        // 인스턴스 중복 방지
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 변경 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 자신을 파괴
        }
    }
    */

    public int MonsterCount
    {
        get { return monsterCount; }
    }

    public void AddMonsterCount()
    {
        monsterCount++;
        MonsterCountChanged?.Invoke(monsterCount);  // 값이 변경되면 이벤트 발동
    }

    // 몬스터 수가 변경될 때 발생하는 이벤트 (int 파라미터는 새로운 몬스터 수)
    public event System.Action<int> MonsterCountChanged;

}
