using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stagePoint2;
    public int stageIndex;
    public int HP;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhp;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;

    private void Update()
    {
        totalPoint = stagePoint + stagePoint2;

        UIPoint.text = totalPoint.ToString();
       
   
    }

    public void NextStage()
    {
        if (stageIndex < Stages.Length-1)
        {
            //스테이지  
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            //PlayerReposition();

            //UIStage.text = "STAGE" + (stageIndex + 1);

        }
        else //게임 오버 
        {
           /* Time.timeScale = 0;

            RestartBtn.SetActive(true);
            Text btnText = RestartBtn.GetComponentInChildren <Text>();
            btnText.text = "Game Clear!";
            RestartBtn.SetActive(true);*/
        }

    }
    public void HPdown()
    {
        if (HP>1)
        {
            HP--;
            UIhp[HP].color = new Color(1, 1, 1, 0.4f);
        }
        else
        {

            //체력 1개도 감소되게
            UIhp[0].color = new Color(1, 0, 0, 0.4f);

            

            player.OnDie();

            Debug.Log("사망");
            
            //재시작
           // RestartBtn.SetActive(true);

        }
    }
    public void HPzero()
    {
        if (HP<1)
        {
            
            UIhp[0].color = new Color(0, 0, 0, 0.4f);

        }
        player.OnDie();
    }
   
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            HPdown();

            if (HP>1)
            {
                //PlayerReposition();
            }
            //사망 모션
           //collision.attachedRigidbody.velocity = Vector2.zero;
            //collision.transform.position = new Vector3(0, 0, -1);
        }
    }
    /*void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);

        player.VelocityZero();
    }*/
}
