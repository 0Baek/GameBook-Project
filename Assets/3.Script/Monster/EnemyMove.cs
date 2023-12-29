using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();


        Invoke("Think",3); // 5초 딜레이 
    }
    void FixedUpdate()
    {
        //무브
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형 체크 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove , rigid.position.y) ;
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    //재귀 함수 
    void Think()
    {
        //set next active
        nextMove = Random.Range(-1, 2); //0,-1,1 
        

        //sprite animation
        animator.SetInteger("RunSpeed", nextMove);

        //sprite
        if (nextMove!=0) //0이 아닐때만 실행
        //nextMove가 1면 flipX를 실행한다 
        spriteRenderer.flipX = nextMove == 1;
       


        //재귀 맨아래쓰는게 좋음 
        Invoke("Think", 3);
    }
    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 3);

    }
    public void OnDamaged()
    {
        //Sprite 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip  뒤집기 
        spriteRenderer.flipY = true;

        //Collider Disable
        capsuleCollider.enabled = false; //비활성 

        //Die Effect
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //Destroy
        Invoke("DeActive", 5);

    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
