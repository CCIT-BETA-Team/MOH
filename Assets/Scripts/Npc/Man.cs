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

    public GameObject ghost;
    GameObject npc_ghost;

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
                case State.HUNGRY:
                    Hungry();
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
    public Room npc_room;
    float sleepy_percent_check
    {
        get
        {
            return sleepy_percent;
        }
        set
        {
            if(value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    npc_ghost = Instantiate(ghost, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                    target_room = npc_room;
                    npc_ghost.GetComponent<Ghost>().target_room = target_room.gameObject;
                    if(target_room.GetComponent<Room>().bed != null)
                    {
                        if(target_room.GetComponent<Room>().bed.GetComponent<Furniture>().bed_enter != null)
                        target_spot =  target_room.GetComponent<Room>().bed.GetComponent<Furniture>().bed_enter;
                        else
                        {
                            target_spot = target_room.GetComponent<Room>().bed;
                        }
                    }
                    npc_ghost.GetComponent<Ghost>().parent_npc = this;
                    npc_ghost.GetComponent<Ghost>().Move_Point(target_room.gameObject);
                    //Invoke("Go_Npc_Room", 2f);

                    this.state = State.SLEEP;
                }
                else
                {
                   if(next_state == null)
                    {
                        next_state = State.SLEEP;
                    }
                }
            }
            else
            {
                sleepy_percent = value;
            }
        }
    }
    float hungry_percent_check
    {
        get
        {
            return hungry_percent;
        }
        set
        {
            if (value >= 100)
            {
                if (this.state == State.IDLE || this.state == State.Move)
                {
                    npc_ghost = Instantiate(ghost, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                    npc_ghost.GetComponent<Ghost>().target_room = target_room.gameObject;
                    //if (target_room.GetComponent<Room>().Kitchen != null)
                    //{
                    //    if (npc_room.GetComponent<Room>().Kitchen.GetComponent<Furniture>().kitchen_enter != null)
                    //        target_spot = npc_room.GetComponent<Room>().Kitchen.GetComponent<Furniture>().kitchen_enter;
                    //    else
                    //    {
                    //        target_spot = npc_room.GetComponent<Room>().Kitchen;
                    //    }
                    //}
                    if (target_room.GetComponent<Room>().Kitchen == null) Debug.Log(23);
                    npc_ghost.GetComponent<Ghost>().parent_npc = this;
                    npc_ghost.GetComponent<Ghost>().Move_Point(target_room.gameObject);
                    Invoke("Go_Target_Room", 2f);

                    this.state = State.HUNGRY;
                }
                else
                {
                    if (next_state == null)
                    {
                        next_state = State.HUNGRY;
                    }
                }
            }
            else
            {
                hungry_percent = value;
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
                    this.state = State.PEE;
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
                    this.state = State.THIRST;
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

    Vector3 npc_velocity;


    bool npc_movecheck = false;
    bool first_roomopen_check = false;
    private void Sleep()
    {
        if (path_finding.Count > 0)
        {
            if (path_finding.Count == 1 && first_roomopen_check == false)
            {
                Go_Next_Door();
            }
            if(path_finding[path_list_number] != null)
            if (path_finding[path_list_number].layer == LayerMask.NameToLayer("Door"))
            {
                var get_door = path_finding[path_list_number].transform.parent.GetComponent<DoorScript>();
                Vector3 dis = path_finding[path_list_number].transform.position - this.transform.position;
                if (path_finding[path_list_number].transform.parent.GetComponent<DoorScript>().Opened == false)
                {
                    agent.SetDestination(path_finding[path_list_number].gameObject.transform.position);
                    if (Vector3.SqrMagnitude(dis) <= 0.5f)
                    {
                        npc_velocity = this.agent.velocity;
                        first_roomopen_check = true;
                        npc_movecheck = true;
                        this.agent.isStopped = true;
                        this.agent.velocity = Vector3.zero;
                        
                        ///
                        get_door.OpenDoor();

                        Invoke("Go_Target_Room", 2f);
                    }
                }
            }
        }

        if(npc_movecheck)
        {
            if(path_finding.Count != path_list_number + 1)
            {
                if (path_finding[path_list_number + 1].layer == LayerMask.NameToLayer("Door"))
                {
                    path_list_number++;
                    npc_movecheck = false;
                    Go_Next_Door();
                }
                else if(path_finding[path_list_number + 1].layer == LayerMask.NameToLayer("Room"))
                {
                    //path_list_number++;
                    if (agent.remainingDistance <= 0.5f && agent.velocity.sqrMagnitude > 0.2f * 0.2f)
                    {
                        //Vector3 dir = target_room.GetComponent<Room>().bed.transform.position - this.transform.position;
                        //Quaternion rotation = Quaternion.LookRotation(dir);
                        //this.transform.rotation = rotation;
                        Debug.Log(23);
                    }
                    

                }
            }
        }



        //if (agent.remainingDistance <= 0.5f && agent.velocity.sqrMagnitude > 0.2f * 0.2f)
        //{
        //    Vector3 dir = target_room.GetComponent<Room>().bed.transform.position - this.transform.position;
        //    Quaternion rotation = Quaternion.LookRotation(dir);
        //    this.transform.rotation = rotation;
        //}





        //숙면


        //숙면이 끝난 것을 체크

        //sleepy_percent = 0;
        //if (next_state != null)
        //{
        //    this.state = next_state.Value;
        //    path_finding.Clear();
        //    path_list_number = 0;
        //    next_state = null;
        //}
        //else 
        //{
        //    path_finding.Clear();
        //    path_list_number = 0;
        //    this.state = State.Move; 
        //}


    }
    
    private void Hungry()
    {
        if (path_finding.Count > 0)
        {
            if (path_finding[path_list_number].layer == LayerMask.NameToLayer("Door"))
            {
                var get_door = path_finding[path_list_number].transform.parent.GetComponent<DoorScript>();
                Vector3 dis = path_finding[path_list_number].transform.position - this.transform.position;
                if (path_finding[path_list_number].transform.parent.GetComponent<DoorScript>().Opened == false)
                {
                    agent.SetDestination(path_finding[path_list_number].gameObject.transform.position);
                    if (Vector3.SqrMagnitude(dis) <= 0.5f)
                    {
                        this.agent.isStopped = true;
                        npc_velocity = this.agent.velocity;
                        this.agent.velocity = Vector3.zero;
                        ///
                        get_door.OpenDoor();
                        ///
                        if (path_finding.Count != path_list_number + 1)
                        {
                            //Invoke("Go_Target_Room", 2f);
                            path_list_number++;
                        }
                        else
                        {
                            //Invoke("Go_Target_Spot", 2f);
                        }
                    }
                }
            }
        }

        if (agent.remainingDistance <= 0.5f && agent.velocity.sqrMagnitude > 0.2f * 0.2f)
        {
            Vector3 dir = target_room.GetComponent<Room>().Kitchen.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            this.transform.rotation = rotation;
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


    /// <summary>
    /// invoke용 네비메쉬 이동 함수
    /// </summary>
    
   void Go_Next_Door()
    {
        //this.agent.velocity = npc_velocity;
        if(path_list_number != 0)
        {
            this.agent.velocity = npc_velocity;
        }
        this.agent.isStopped = false;
        this.agent.SetDestination(path_finding[path_list_number].transform.position);
    }
  
    void Go_Target_Room()
    {
        this.agent.velocity = npc_velocity;
        this.agent.isStopped = false;
        agent.SetDestination(target_room.gameObject.transform.position);
    }
    
    void Go_Target_Spot()
    {
        this.agent.isStopped = false;
        this.agent.velocity = npc_velocity;
        if (target_spot != null)
        {
            this.agent.SetDestination(target_spot.transform.position);

        }
    }

    void Start()
    {
        this.state = State.IDLE;
        Select_Personality();
        StartCoroutine(State_Gaze_Change());
        player_texture = (Texture2D)player.GetComponent<MeshRenderer>().material.mainTexture;
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
                    miss_player = false;
                    Vector2 player_uv = hit.textureCoord;
                    Vector2 screen_pos = cam.WorldToViewportPoint(player.transform.position);
                    player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
                    screen_uv_color = uv_texture(tex).GetPixel((int)(Screen.width * screen_pos.x), (int)(Screen.height * screen_pos.y));
                    if (screen_uv_color.r + screen_uv_color.g + screen_uv_color.b < 0.1f) { }
                    else
                    {
                        if (this.state != State.REPORT)
                        {
                            fear_percent = 100;
                            Fear_Check();
                        }
                    }
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
        hungry_percent = 0;
        hungry_percent_check = hungry_percent;
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
        hungry_percent_check = hungry_percent;
        pee_percent_check = pee_percent;
        thirst_percent_check = thirst_percent;
        ///

        StartCoroutine(State_Gaze_Change());
    }

 
}
