/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f; //이동속도 
    private float jumpForce = 4.0f;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private Rigidbody2D rigid2D;
    private Animator animator;
    private StageData stageData;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        
            // left 또는 a = -1 / right 또는 d =1
            float x = Input.GetAxisRaw("Horizontal");
            //좌우 이동 방향제어 
            Move(x);
           animator.SetBool("IsRun", Mathf.Abs(x) > 0);

            //플레이어 점프 (스페이스 키를 누르면 점프) 
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                jumpCount++;
            //점프 직전의 속도를 순간적으로 제로로 변경
            rigid2D.velocity = Vector2.zero;
            //Vector2.zero = new vector2(0,0)
            //플레이어 리지드바디에 위쪽으로 힘을 줍니다. 
            rigid2D.AddForce(new Vector2(0, jumpForce));
                Jump();
             animator.SetTrigger("IsJump");
            }

        



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿으면 점프 횟수 초기화
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }


    public void Move(float x)
    {
       
        //x축 이동은 x * speed로, y축 이동은 기존의 속력 값 (현재는 중력)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }
    public void Jump()
    {
        // jumpForce의 크기만큼 왼쪽 방향으로 속력 설정 
        rigid2D.velocity = Vector2.up * jumpForce;
    }
}
*/