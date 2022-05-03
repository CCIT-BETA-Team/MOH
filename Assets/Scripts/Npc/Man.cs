using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Man : Npc
{
    RaycastHit hit;//레이
    int layermask = 1 << 6;

    public Camera cam;//Npc 눈
    public float attack_range;//임의 값 설정
    public AudioSource sound;
    
    public override void Select_Personality()
    {
        int a = Random.Range(0, 2);
        if(a == 0)
        {
            this.personality = Npc_Personality.AGGESSIVE;
        }
        else if(a == 1)
        {
            this.personality = Npc_Personality.Defensive;
        }
    }

    State state_check
    {
        get
        {
            return this.state;
        }
        set
        {
            switch (value)
            {
                case State.SLEEP:
                    Sleep();
                    break;
                case State.PEE:
                    Pee();
                    break;
                case State.THIRST:
                    Thirst();
                    break;
                case State.REPORT:
                    Report();
                    break;
                case State.TRACE:
                    Trace();
                    break;
            }
        }
    }

    State? next_state;

    float sleepy_percent_check
    {
        get{ return sleepy_percent; }
        set
        {
            if(value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    //int count = Random.Range(0, NpcManager.instance.sleep_items.Count);
                    //target_item = NpcManager.instance.sleep_items[count].gameObject; 
                    npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, npc_ghost, this);
                    this.agent.enabled = true;
                    this.state = State.SLEEP;
                }
                else{ if(next_state == null ){next_state = State.SLEEP;} }
            }
            else
            {
                sleepy_percent = value;
            }
        }
    }
    float pee_percent_check
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
                    int count = Random.Range(0, NpcManager.instance.pee_items.Count);
                    target_item = NpcManager.instance.pee_items[count].gameObject;
                    NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, npc_ghost, this);
                    this.state = State.PEE;
                }
                else
                {
                    if (next_state == null)
                    {
                        next_state = State.PEE;
                    }
                }
            }
            else
            {
                pee_percent = value;
            }
        }
    }
    float thirst_percent_check
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
                    int count = Random.Range(0, NpcManager.instance.thirsty_items.Count);
                    target_item = NpcManager.instance.thirsty_items[count].gameObject;
                    NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, npc_ghost, this);
                    this.state = State.THIRST;
                }
                else
                {
                    if (next_state == null)
                    {
                        next_state = State.THIRST;
                    }
                }
            }
            else
            {
                thirst_percent = value;
            }
        }
    }

    //
    GameObject Close_Door_Save;

    void Reback_Velocity()
    {
        //
        int random_close_door = Random.Range(0,2);
        if(random_close_door == 0) { Close_Door_Save = path_finding[0].transform.parent.gameObject; Invoke("For_Close_Door_Delay", 1f); }
        //
        path_finding.RemoveAt(0);
        if (npc_ghost != null)
            npc_ghost.GetComponent<Ghost>().pathfinding_list.RemoveAt(0);
        //
        this.agent.enabled = true;
        this.agent.isStopped = false;
        opening_check = false;
    }

    void For_Close_Door_Delay() // invoke
    {
        if(Close_Door_Save != null) { Close_Door_Save.GetComponent<DoorScript>().CloseDoor();  Close_Door_Save = null; }
    }
    //

    public bool opening_check = false;

    private void Sleep()
    {
        if (npc_ghost != null && opening_check == false)
        {
            this.agent.SetDestination(npc_ghost.transform.position);
        }
        else if (npc_ghost == null && opening_check == false)
        {
            if (path_finding.Count > 0)
            {
                if (path_finding[0].layer != 10) //Room
                {
                    if (this.agent.enabled == true)//형식적 확인
                    this.agent.SetDestination(path_finding[0].transform.position);
                }
                else if(path_finding[0].layer == 10) //Room
                {
                    if (this.agent.enabled == true)
                        this.agent.SetDestination(target_item.GetComponent<Item>().enter_spot.transform.position);
                }
            }
        }

        if(path_finding.Count > 0)
        {
            if(path_finding[0].layer == 9)//Door
            {
                if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                {
                    var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                    Vector3 dis = path_finding[0].transform.position - this.transform.position; 
                    if(Vector3.SqrMagnitude(dis) <= 1f )
                    {
                        opening_check = true;
                        this.agent.enabled = false;
                        //
                        door_info.OpenDoor();
                        //
                        
                        Invoke("Reback_Velocity", 2f);
                    }
                }
                else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                {
                    Vector3 dis = path_finding[0].transform.position - transform.position;

                    if(Vector3.SqrMagnitude(dis) <= 0.5f)
                    {
                        int random_close_door = Random.Range(0, 2);
                        if (random_close_door == 0) { Close_Door_Save = path_finding[0].transform.parent.gameObject; Invoke("For_Close_Door_Delay", 1f); }

                        path_finding.RemoveAt(0);
                        if (npc_ghost != null)
                            npc_ghost.GetComponent<Ghost>().pathfinding_list.RemoveAt(0);
                    }
                }
            }
            else if (path_finding[0].layer == 10)//Room
            {
                Vector3 dir = target_item.transform.position - transform.position;
                if (Vector3.SqrMagnitude(dir) <= 3f && Vector3.SqrMagnitude(dir) >= 1f)
                {
                    transform.rotation = Quaternion.LookRotation(dir).normalized;
                }
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.025f) // agent.remainingDistance 
                {
                    
                }
            }
        }



    }

    private void Pee()
    {
        pee_percent = 0;
        pee_percent_check = pee_percent;
    }
    private void Thirst()
    {
        thirst_percent = 0;
        thirst_percent_check = thirst_percent;
        
    }
    private void Report()
    {
        switch (personality)
        {
            case Npc_Personality.AGGESSIVE:
                Aggessive();
                break;
            case Npc_Personality.Defensive:
                Defensive();
                break;
        }
    }
    private void Trace()
    {
        Transform player_transform = player.transform;
        agent.SetDestination(player_transform.position);

    }

   

    bool aggessive_trace_check = true;
    void Aggessive()
    {
        if(aggessive_trace_check == true)
        {
            Transform player_transform = player.transform;
            agent.SetDestination(player_transform.position);

            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2, 2,
            2 ), Quaternion.identity, layermask);
            
            if(col.Length != 0)
            {
               //
            }
        }
        else if(aggessive_trace_check == false)
        {
            //쫓는걸 그만두고 신고하러 감
        }
    }

    void Defensive()
    {
        
    }

    void Start()
    {
        this.state = State.IDLE;
        Select_Personality();
        StartCoroutine(State_Gaze_Change());
        //
        player_texture = (Texture2D)player.GetComponent<MeshRenderer>().material.mainTexture;
        //
        //player_texture = player.GetComponent<MeshRenderer>().material.mainTexture;
    }
    Color player_texture_Color;
    Color screen_uv_color;


    float player_check_time;//플레이어 감지 시간
    bool? miss_player;
    bool report_state_check = false;
    void Update()
    {
        //
        state_check = this.state;
        //
      

        if (Check_Unit())
        {
            if (Physics.Raycast(cam.transform.position, (player.transform.position - cam.transform.position), out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == 6)//player
                {
                    //miss_player = false;
                    //Vector2 player_uv = hit.textureCoord;
                    //Vector2 screen_pos = cam.WorldToViewportPoint(player.transform.position);
                    //player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
                    //screen_uv_color = uv_texture(tex).GetPixel((int)(Screen.width * screen_pos.x), (int)(Screen.height * screen_pos.y));
                    //if (screen_uv_color.r + screen_uv_color.g + screen_uv_color.b < 0.1f) { }
                    //else
                    //{
                    //    if (this.state != State.REPORT)
                    //    {
                    //        fear_percent = 100;
                    //        Fear_Check();
                    //    }
                    //}
                    //Debug.Log("Pixel position   :   "+screen_pos.x +"   "+screen_pos.y);
                    //Debug.Log("Color R : " + player_texture_Color.r + " , " + "Color G : " + +player_texture_Color.g + " , " + "Color B : " + +player_texture_Color.b + " , " + "Texture uv Pixel Color");
                    //Debug.Log("Color R : " + screen_uv_color.r + " , " + "Color G : " + + screen_uv_color.g + " , "+"Color B : " + + screen_uv_color.b + " , "+ "Scene uv Pixel Color");
                    ////화면에서 보는 플레이어 컬러
                }
                else
                {
                    if(miss_player == false && miss_player != null)
                    {
                        miss_player = true;
                        player_check_time += Time.deltaTime;
                        if(this.state == State.REPORT || this.state == State.TRACE)
                        {
                            if(player_check_time > 5.0f)
                            {
                                aggessive_trace_check = false;
                                player_check_time = 0;
                            }
                        }
                    }
                }
                

                if (this.state == State.REPORT || this.state == State.TRACE)
                {

                }
                
            }
        }
    }
 
    public void Fear_Check()//플레이어를 감지하여 경계도가 100이 된 상황
    {
        StopAllCoroutines();//가중치 증가 중지

        //가중치 초기화
        sleepy_percent = 0;
        sleepy_percent_check = sleepy_percent;
        pee_percent = 0;
        pee_percent_check = pee_percent;
        thirst_percent = 0;
        thirst_percent_check = thirst_percent;

        this.state = State.REPORT;
    }

    

    public bool Check_Unit()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(player.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }


    IEnumerator State_Gaze_Change()
    {
        yield return new WaitForSeconds(2f);
        
        StateGazeUp(this.state);

        ///
        sleepy_percent_check = sleepy_percent;
        pee_percent_check = pee_percent;
        thirst_percent_check = thirst_percent;
        ///

        StartCoroutine(State_Gaze_Change());
    }

 
}
