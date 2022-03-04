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
    
    /////
    
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


    //////
    public enum parametertype
    {
        SLEEP,
        HUNGRY,
        PEE,
        THIRST,
        FEAR
    }

    public parametertype parameter;

    [Header("Npc 상태별 게이지")]
    [Range(0, 100)]
    public float sleepy_percent;//수면
    [Range(0, 100)]
    public float hungry_percent;//배고픔
    [Range(0, 100)]
    public float pee_percent;//화장실
    [Range(0, 100)]
    public float thirst_percent;//목마름
    [Range(0, 100)]
    public float fear_percent;//공포

    
    public void Gazechange(float value,parametertype type)
    {
        switch (type)
        {
            case parametertype.SLEEP:
                sleepy_percent += value;
                break;
            case parametertype.HUNGRY:
                hungry_percent += value;
                break;
            case parametertype.PEE:
                hungry_percent += value;
                break;
            case parametertype.THIRST:
                hungry_percent += value;
                break;
            case parametertype.FEAR:
                hungry_percent += value;
                break;
        }

    }
    public void Allup(float value)
    {
        Gazechange(value, parametertype.SLEEP);
        Gazechange(value, parametertype.HUNGRY);
        Gazechange(value, parametertype.PEE);
        Gazechange(value, parametertype.THIRST);
        Gazechange(value, parametertype.FEAR);
    }

    [Space]
    public List<GameObject> npc_item = new List<GameObject>();//Npc가 소유한 아이템 리스트

    

    //public float Choose(float[] probs)
    //{

    //    float total = 0;

    //    foreach (float elem in probs)
    //    {
    //        total += elem;
    //    }

    //    float randomPoint = Random.value * total;

    //    for (int i = 0; i < probs.Length; i++)
    //    {
    //        if (randomPoint < probs[i])
    //        {
    //            Debug.Log(probs[i]);
    //            return i;
    //        }
    //        else
    //        {
    //            randomPoint -= probs[i];
    //        }
    //    }
    //    return probs.Length - 1;
    //}


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

       

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
        //Increase_Percent(sleepy_percent);
    }

    //public abstract void Increase_Precent(ref float[] percent_guage);
   





    public abstract void Move();
    
}
