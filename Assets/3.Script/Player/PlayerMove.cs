using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private string nextSceneName;
   
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioScore;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public float maxSpeed;
    public float jumpPower;
    private int jumpCount = 0;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    AudioSource audioSource;


    private void Awake()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
   
    private void Update()
    {
        /* //���� 
         if (Input.GetButtonDown("Jump")&& !animator.GetBool("IsJump"))
         {
             rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
             animator.SetBool("IsJump", true);


             PlaySound("JUMP");



         }*/

        // ���� 
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            rigid.velocity = Vector2.up * jumpPower;
            jumpCount++; // ���� Ƚ�� ����

            animator.SetBool("IsJump", true);
            PlaySound("JUMP");
        }


        //������ȯ
        if (Input.GetButton("Horizontal"))
        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;


        if (Mathf.Abs(rigid.velocity.x )< 0.3) //���밪�� ����ٸ� 
            animator.SetBool("IsRun", false);
        else
            animator.SetBool("IsRun", true);

    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.Limitmin.x, stageData.LimitMax.x),
                                          Mathf.Clamp(transform.position.y, stageData.Limitmin.y, stageData.LimitMax.y));
    }
    private void FixedUpdate()
    {
        //����
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right *h,ForceMode2D.Impulse);

        //�ƽ� ���ǵ�
        if (rigid.velocity.x > maxSpeed) //������ 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1)) //����
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);

        //Landing platform  
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //��� 

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down,1, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance<0.5f)
                    animator.SetBool("IsJump", false);


            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //������ ��Ƽ� ���̴� 
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
               OnDamaged(collision.transform.position);



        }
        if (collision.gameObject.tag == "Voss")
        {
            OnHPzero(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            animator.SetBool("IsJump", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Score")
        {
            gameManager.stagePoint += 100;
            collision.gameObject.SetActive(false);
            //���� 
            PlaySound("SCORE");
        }
        else if (collision.gameObject.tag =="Score2")
        {
            gameManager.stagePoint2 += 100;
            collision.gameObject.SetActive(false);
            //���� 
            PlaySound("SCORE");
        }
        if (gameManager.stagePoint >= 1000)
        {
            
            StageNext(); //�ؽ�Ʈ �޼��� ȣ��
        }
        if (gameManager.stagePoint2 >=1000)
        {
            ClearScene();
        }
    }
    
    void OnDamaged(Vector2 targetPosition)
    {
        //HP ���� 
        gameManager.HPdown();

        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPosition.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) , ForceMode2D.Impulse);

        //Animation
        animator.SetTrigger("IsDamaged");


        Invoke("OffDamaged", 2); //������

        //����
        PlaySound("DAMAGED");



    }
    void OnHPzero(Vector2 targetPosition)
    {
        gameManager.HPzero();
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPosition.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 2, ForceMode2D.Impulse);

        //Animation
        animator.SetTrigger("IsDamaged");


        Invoke("OffDamaged", 2); //������

        //����
        PlaySound("DAMAGED");

    }
    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void OnAttack(Transform enemy)
    {

        //Point 
        gameManager.stagePoint += 100;
        gameManager.stagePoint2 += 100;

        //ReAction jump 
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);

        //���� 
        PlaySound("ATTACK");

        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
            
    }
    public void OnDie()
    {
        //Sprite 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        
        //Sprite Flip  ������ 
        spriteRenderer.flipY = true;

        //Collider Disable
        capsuleCollider.enabled = false; //��Ȱ�� 
                                         //���� 
      

        //Die Effect
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

       

        //Destroy
        Invoke("DeActive", 5);

        
        Invoke("LoadGameOverScene", 2);
       
    }
    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;

            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "SCORE":
                audioSource.clip = audioScore;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;

        }
        audioSource.Play();
    }
    private void LoadGameOverScene()
    {
      
        SceneManager.LoadScene(nextSceneName);

        

    }
  
  
    private void StageNext()
    {
        // ���� ���� ������ ���⿡ �ۼ�
        PlaySound("FINISH"); // ���� ���� ���� ��� ��

        // ��� ����� �� ���� ������ �̵�
        SceneManager.LoadScene("Stage_2");
       


    }
    private void ClearScene()
    {
        // ���� ���� ������ ���⿡ �ۼ�
        PlaySound("FINISH"); // ���� ���� ���� ��� ��

        // ��� ����� �� ���� ������ �̵�
        SceneManager.LoadScene("GameClear");

    }


}

   
