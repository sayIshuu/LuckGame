using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    [SerializeField] float speed = 2.0f; // 이동 속도
    private Vector2 moveDirection = Vector2.left; // 이동할 방향
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 매 프레임마다 오브젝트를 이동
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    // 트리거에서 호출되는 방향을 바꾸는 함수
    public void ChangeDirection(bool isFlip)
    {
        //90도 회전
        moveDirection = Quaternion.Euler(0, 0, -90) * moveDirection;

        // 스프라이트 flip
        if (isFlip)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
