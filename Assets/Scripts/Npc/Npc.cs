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
    public NavMeshAgent agent;
    public List<GameObject> npc_item = new List<GameObject>();//Npc가 소유한 아이템 리스트
    public List<GameObject> path_finding = new List<GameObject>();
    //
    public Animator anim;
    //
    public GameObject player;
    public GameObject target_spot;
    //
    public GameObject ghost;
    public GameObject npc_ghost;
    //
    public GameObject target_item;
    //
    public RaycastHit hit;//레이
    public int player_layermask = 1 << 6;
    public float attack_range;//임의 값 설정
    public AudioSource sound;
    //
    public float npc_speed;
    public int faint_time;
    public enum Npc_Type{
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
        SLEEP,//졸림
        PEE,//화장실
        THIRST,//목마름
        REPORT,//신고
        FAINT,//기절
        ATTACK,//공격 
        ESCAPE,//도주
        TRACE,//추적
        FEAR//경계 
    }
    public State state = State.IDLE;
    public enum parametertype
    {
        NONE,
        SLEEP,
        PEE,
        THIRST,
        FEAR
    }
    public parametertype parameter;
    public enum Npc_Personality
    {
        AGGESSIVE,
        Defensive
    }
    public Npc_Personality personality = Npc_Personality.AGGESSIVE;

    [Range(0, 100)]
    public float sleepy_percent;
    [Range(0, 100)]
    public float pee_percent;
    [Range(0, 100)]
    public float thirst_percent;
    [Range(0, 100)]

    public float fear_percent;

    
    public Room target_room;

    public void Gazechange(float value,parametertype type)
    {
        switch (type)
        {
            case parametertype.SLEEP:
                if (sleepy_percent <= 100f)
                    sleepy_percent += value * (Random.value);
                break;
            case parametertype.PEE:
                if (pee_percent <= 100f)
                    pee_percent += value * (Random.value);
                break;
            case parametertype.THIRST:
                if (thirst_percent <= 100f)
                    thirst_percent += value * (Random.value);
                break;
            case parametertype.FEAR:
                if (fear_percent <= 100f)
                    fear_percent += value * (Random.value);
                break;
        }

    }
    public void Allup(float value,bool Fear_Check)
    {
        Gazechange(value * (Random.value / 3), parametertype.SLEEP);
        Gazechange(value * (Random.value / 3), parametertype.PEE);
        Gazechange(value * (Random.value / 3), parametertype.THIRST);
        if(Fear_Check)
        Gazechange(value * (Random.value / 3), parametertype.FEAR);
    }
    public void StateGazeUp(State what)
    {
        switch (what) 
        {
            case State.IDLE :
                Allup(3, false);
                break;
            case State.Move :
                Allup(3, false);
                break;
            case State.SLEEP :
                Gazechange(3, parametertype.PEE);
                Gazechange(3, parametertype.THIRST);
                break;
            case State.PEE :
                Gazechange(3, parametertype.SLEEP);
                Gazechange(3, parametertype.THIRST);
                break;
            case State.THIRST :
                Gazechange(3, parametertype.PEE);
                Gazechange(3, parametertype.SLEEP);
                break;
            case State.REPORT :
                //
                break;
            case State.FAINT :
                //
                break;
            case State.ATTACK :
                //
                break;
            case State.ESCAPE :
                //
                break;
            case State.TRACE:
                //
                break;
            case State.FEAR:
                //
                break;
        }
    }

    /// 
    public Texture2D player_texture;
    public RenderTexture tex;
    public Texture2D uv_tex=null;
    public Texture2D uv_texture(RenderTexture rtex)
    {
        if (uv_tex == null)
        {
            uv_tex = new Texture2D(2000, 2000, TextureFormat.RGB565, false);
        }
            RenderTexture.active = rtex;
            uv_tex.ReadPixels(new Rect(0, 0, rtex.width, rtex.height), 0, 0);
            uv_tex.Apply();

        if(uv_tex==null)
        {
            return null;
        }
        else
        {
            return uv_tex;
        }
    }
    /// 







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

    }


    public void Pathfinding_List_Initialization()
    {
        path_finding = new List<GameObject>();
    }




    /// <summary>
    /// 
    /// </summary>
    public abstract void Select_Personality();

    



}
