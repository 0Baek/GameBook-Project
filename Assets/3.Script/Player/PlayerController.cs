/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f; //�̵��ӵ� 
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
        
        
            // left �Ǵ� a = -1 / right �Ǵ� d =1
            float x = Input.GetAxisRaw("Horizontal");
            //�¿� �̵� �������� 
            Move(x);
           animator.SetBool("IsRun", Mathf.Abs(x) > 0);

            //�÷��̾� ���� (�����̽� Ű�� ������ ����) 
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                jumpCount++;
            //���� ������ �ӵ��� ���������� ���η� ����
            rigid2D.velocity = Vector2.zero;
            //Vector2.zero = new vector2(0,0)
            //�÷��̾� ������ٵ� �������� ���� �ݴϴ�. 
            rigid2D.AddForce(new Vector2(0, jumpForce));
                Jump();
             animator.SetTrigger("IsJump");
            }

        



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ������ ���� Ƚ�� �ʱ�ȭ
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
       
        //x�� �̵��� x * speed��, y�� �̵��� ������ �ӷ� �� (����� �߷�)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }
    public void Jump()
    {
        // jumpForce�� ũ�⸸ŭ ���� �������� �ӷ� ���� 
        rigid2D.velocity = Vector2.up * jumpForce;
    }
}
*/