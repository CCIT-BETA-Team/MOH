using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class NpcStatePercent
{

}

public abstract class Npc : MonoBehaviour
{
    NavMeshAgent agent;
   

    public int npc_speed;//Npc 이동속도
    public int faint_time;//기절시간

    public enum Npc_Type
    {
        NONE,
        POLICE,//경찰 // 남녀구분x
        GRANDFATHER,//할아버지
        GRANDMA,//할머니
        MAN,//남자
        WOMAN,//여자
    }
    public Npc_Type npc_type = Npc_Type.NONE;
    
    public enum State 
    {
        IDLE,//대기
        Move,//정찰
        SLEEPY,//졸림
        HUNGRY,//배고픔
        PEE,//화장실
        THIRST,//목마름
        REPORT,//신고
        FAINT,//기절
        ATTACK,//공격
        ESCAPE,//도주
        TRACE,//추적
        FEAR//공포
    }
    public State state = State.IDLE;

    [Header("Npc 상태별 게이지")]
    [Range(0, 100)]
    public float sleepy_percent;
    [Range(0, 100)]
    public float hungry_percent;
    [Range(0, 100)]
    public float pee_percent;
    [Range(0, 100)]
    public float thirst_percent;
    [Range(0, 100)]
    public float faint_percent;
    [Range(0, 100)]
    public float fear_percent;





    [Space]
    public List<GameObject> npc_item = new List<GameObject>();//Npc가 소유한 아이템 리스트


    public float[] state2;

    public float Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                //Debug.Log(probs[i]);
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        state2[0] = sleepy_percent;

        switch (npc_type)
        {
            case Npc_Type.NONE:
                break;
            case Npc_Type.POLICE:
                break;
            case Npc_Type.GRANDFATHER:
                break;
            case Npc_Type.GRANDMA:
                break;
            case Npc_Type.MAN:
                break;
            case Npc_Type.WOMAN:
                break;
        }


    }


    private void Update()
    {
        //Choose(asd);
        
    }


    public void Select_State()//대기 , 이동 , 수면 , 배고픔 , 화장실 , 목마름 
    {
        //무작위 행동을 주로
        switch (state) 
        {
            case State.IDLE:
                //Increase_Percent(sleepy_percent, hungry_percent, pee_percent, thirst_percent);
                break;
            case State.Move:

                break;

        }

    }



    void Increase_Percent(params float[] percent_guage)
    {
        for(int i = 0; i < percent_guage.Length; i++)
        {
            percent_guage[i] += Time.deltaTime / 3;
        }
    }

    
    



    public abstract void Move();
}
