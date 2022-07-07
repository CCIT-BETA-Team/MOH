using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<GameObject> npc_item = new List<GameObject>();//Npc가 소유한 아이템 리스트
    public List<GameObject> path_finding = new List<GameObject>();
    //
    public Animator anim;
    //
   
    public GameObject player_obj;
    public Player player;
    public GameObject ghost;
    public GameObject npc_ghost;
    protected GameObject Close_Door_Save;
    //
    protected int layermask_for_except = (1 << 9) | (1 << 10) | (1 << 15);
    
    //
    public GameObject target_room;
    public GameObject target_item;
    public GameObject current_room;
    //
    protected bool opening_check = false;
    protected bool state_end_check = false;
    protected bool move_check = false;// 아직 안씀
    protected bool aggessive_trace_check = true;

    protected bool state_continue = true;//Trace or Report 상태로 바뀌면 변경
    //
    //npc에 타격에 따른 감소값
    public float faint_gauge = 100;
    //

    public float attack_range;//임의 값 설정
    public AudioSource sound;
    //
    protected float npc_speed;
    protected float report_npc_speed = 1.5f;
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
        ESCAPE,//도주
        TRACE,//추적
        FEAR,//경계
        NULL     //
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


    /// <summary>
    /// For Police , Aggessive Npc
    /// </summary>
    public enum Attack_Type { GUN, PUNCH, CUDGEL }
    [Header("NPC 공격 타입")]
    public Attack_Type attack_type;

    [Header("무기 오브젝트")]
    public GameObject gun;
    public GameObject gun_bullet;
    public GameObject cudgel;

    [Range(0, 100)]
    public float sleepy_percent;
    [Range(0, 100)]
    public float pee_percent;
    [Range(0, 100)]
    public float thirst_percent;
    [Range(0, 100)]
    public float fear_percent;

    #region
    protected readonly int moveing_hash = Animator.StringToHash("agent_move_check");
    protected readonly int gun_hash = Animator.StringToHash("agent_attack_check_gun");
    protected readonly int punch_hash = Animator.StringToHash("agent_attack_check_punch");
    protected readonly int cudgel_hash = Animator.StringToHash("agent_attack_check_cudgel");
    protected readonly int sleep_hash = Animator.StringToHash("sleep_check");
    protected readonly int pee_hash = Animator.StringToHash("pee_check");
    protected readonly int pee_end_hash = Animator.StringToHash("pee_end_check");
    protected readonly int thirst_hash = Animator.StringToHash("thirst_check");
    protected readonly int call_police_hash = Animator.StringToHash("call_police_check");

    #endregion



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
            case State.TRACE:
                //
                break;
            case State.FEAR:
                //
                break;
        }
    }



    #region
    /// 
    //public Texture2D player_texture;
    //public RenderTexture tex;
    //public Texture2D uv_tex=null;
    //public Texture2D uv_texture(RenderTexture rtex)
    //{
    //    if (uv_tex == null)
    //    {
    //        uv_tex = new Texture2D(2000, 2000, TextureFormat.RGB565, false);
    //    }
    //        RenderTexture.active = rtex;
    //        uv_tex.ReadPixels(new Rect(0, 0, rtex.width, rtex.height), 0, 0);
    //        uv_tex.Apply();

    //    if(uv_tex==null)
    //    {
    //        return null;
    //    }
    //    else
    //    {
    //        return uv_tex;
    //    }
    //}
    /// 
    #endregion
    [SerializeField]
    protected State next_state = State.NULL;
    protected State current_state = State.NULL;

    public float sleepy_percent_check
    {
        get { return sleepy_percent; }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    sleepy_percent = 0;
                    State_Initizlize();
                    current_state = State.SLEEP;
                    Re_Set_Room:
                    target_room = NpcManager.instance.Bed_Room[Random.Range(0, NpcManager.instance.Bed_Room.Count)].gameObject;
                    target_item = target_room.GetComponent<Room>().Decide_Target_Item();

                    if (target_item != null)
                    npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, npc_ghost, this);
                    else if(target_item == null) { goto Re_Set_Room; }

                    agent.enabled = true;

                    state = State.SLEEP;
                }
                else { next_state = State.SLEEP;  }
            }
            else
            {
                sleepy_percent = value;
            }
        }
    }

    public float pee_percent_check
    {
        get
        {
            return pee_percent;
        }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    pee_percent = 0;
                    State_Initizlize();
                    current_state = State.PEE;

                    Re_Set_Room:
                    target_room = NpcManager.instance.Bath_Room[Random.Range(0, NpcManager.instance.Bath_Room.Count)].gameObject;
                    target_item = target_room.GetComponent<Room>().Decide_Target_Item();


                    if (target_item != null)
                        npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, npc_ghost, this);
                    else if(target_item == null) { goto Re_Set_Room; }
                    agent.enabled = true;
                    state = State.PEE;
                }
                else {  next_state = State.PEE;  }
            }
            else
            {
                pee_percent = value;
            }
        }
    }

    public float thirst_percent_check
    {
        get
        {
            return thirst_percent;
        }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    thirst_percent = 0;
                    State_Initizlize();
                    current_state = State.THIRST;

                    Re_Set_Room:
                    target_room = NpcManager.instance.Dining_Room[Random.Range(0, NpcManager.instance.Dining_Room.Count)].gameObject;
                    target_item = target_room.GetComponent<Room>().Decide_Target_Item();

                    if (target_item != null)
                        npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, npc_ghost, this);
                    else if (target_item == null) { goto Re_Set_Room; }

                    agent.enabled = true;
                    state = State.THIRST;
                }
                else {  next_state = State.THIRST;  }
            }
            else
            {
                thirst_percent = value;
            }
        }
    }






    private void Awake()
    {
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
        Select_Personality();

        //if(npc_type != Npc_Type.POLICE)
        //Invoke("Change_State_Move", 1f);

        if(npc_type == Npc_Type.POLICE) { attack_type = Attack_Type.GUN; }
        
    }


    private void Update()
    {
        if(state != State.FAINT)
        if(faint_gauge <=0)
        {
            npc_ghost = null;
            target_item = null;
            target_room = null;
            opening_check = false;
            Pathfinding_List_Initialization();
            state = State.FAINT;
        }
    }


    public void Pathfinding_List_Initialization()
    {
        path_finding = new List<GameObject>();
    }


    public void Change_State_Move()
    {
        state = State.Move;
        this.agent.enabled = true;
        if(ghost != null)
        npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, ghost, this);
        //target_room = npc_ghost.GetComponent<Ghost>().target_room;
        target_room = npc_ghost.GetComponent<Ghost>().target_room;
    }
    public void State_Initizlize()
    {
        state = State.IDLE;
        npc_ghost = null;
        target_item = null;
        target_room = null;
        opening_check = false;
    }
    public void Select_Personality()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            this.personality = Npc_Personality.AGGESSIVE;
            int select_attack_type = Random.Range(0, 3);
            switch(select_attack_type)
            {
                case 0:
                    this.attack_type = Attack_Type.GUN;
                    break;
                case 1:
                    this.attack_type = Attack_Type.PUNCH;
                    break;
                case 2:
                    this.attack_type = Attack_Type.CUDGEL;
                    break;
            }
        }
        else if (a == 1)
        {
            this.personality = Npc_Personality.Defensive;
        }
    }
    public void Fear_Check()//플레이어를 감지하여 경계도가 100이 된 상황
    {

        for(int i = 0; i < NpcManager.instance.npc_list.Count;i++)
        {
            NpcManager.instance.npc_list[i].GetComponent<Npc>().state = State.REPORT;
        }
    }

    public void anim_event_state_check()
    {
        anim.ResetTrigger("sleep_hash");
        anim.ResetTrigger("pee_hash");
        anim.ResetTrigger("thirst_hash");
        state_end_check = true;
    }

    //For_State_End_Check
    protected bool once = false;
    public void state_end_check_for_invoke()
    {
        anim.SetTrigger(pee_end_hash);
        state_end_check = true;
    }
    
    public void State_end_check_true()
    {
        state_end_check = true;
    }

    //For_Gun
    public void Set_Active_True_For_Gun()
    {
        gun.SetActive(true);
    }
    public void Set_Active_False_For_Gun()
    {
        if(gun.activeSelf == true) { gun.SetActive(false); }
    }
}
