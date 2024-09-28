using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private int monsterCount = 0;

    private void Awake()
    {
        // 싱글톤 인스턴스를 설정
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    /* 자꾸 캐시가 남아서 자기 자신을 파괴하는 버그 발생해서 위처럼 수정
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


    // 몬스터 수가 변경될 때 발생하는 이벤트 (int 파라미터는 새로운 몬스터 수) 추가, 감소 이벤트핸들러 다 붙여줄 수 있게
    public event System.Action<int> MonsterCountChanged;

    public int MonsterCount
    {
        get { return monsterCount; }
    }

    public void AddMonsterCount()
    {
        monsterCount++;
        MonsterCountChanged?.Invoke(monsterCount);
    }
}
