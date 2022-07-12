using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Grandma : Npc
{
    public Camera cam;//Npc 눈
    public RaycastHit hit;//레이
    int player_layermask = 1 << 6;
    private State state_check
    {
        get
        {
            return this.state;
        }
        set
        {
            switch (value)
            {
                case State.Move:
                    Move();
                    break;
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
                case State.FAINT:
                    Faint();
                    break;
            }
        }
    }
    //

    ///////////////////////

    void Reback_Velocity()
    {
        if (state == current_state && npc_ghost != null && npc_ghost.GetComponent<Ghost>().pathfinding_list.Count > 0)
        {
            //
            int random_close_door = Random.Range(0, 2);
            if (random_close_door == 0)
            {
                if (path_finding[0].transform.gameObject.layer != 10)
                {
                    Close_Door_Save = path_finding[0].transform.parent.gameObject;
                    Invoke("For_Close_Door_Delay", 1f);
                }
            }
            //
            path_finding.RemoveAt(0);
            npc_ghost.GetComponent<Ghost>().pathfinding_list.RemoveAt(0);
        }
        //
        this.agent.enabled = true;
        //this.agent.isStopped = false;
        opening_check = false;
    }
    void For_Close_Door_Delay() // invoke
    {
        if (Close_Door_Save != null)
        {
            Close_Door_Save.GetComponent<DoorScript>().CloseDoor();
            Close_Door_Save = null;
        }
    }
    private void Move()
    {
        if (!state_end_check)
        {
            if (npc_ghost != null && opening_check == false)
            {
                this.agent.SetDestination(npc_ghost.transform.position);
            }
            else if (npc_ghost == null && opening_check == false)
            {
                if (path_finding.Count > 0)
                {
                    if (path_finding[0].layer != 10) //Room layer
                    {
                        if (this.agent.enabled == true)//형식적 확인
                            this.agent.SetDestination(path_finding[0].transform.position);
                    }
                    else if (path_finding[0].layer == 10) //Room layer
                    {
                        if (this.agent.enabled == true)
                        {
                            this.agent.SetDestination(target_room.transform.position);
                        }
                    }
                }
            }
            if (path_finding.Count > 0)
            {
                if (path_finding[0].layer == 9)//Door
                {
                    if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                    {
                        var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                        Vector3 dis = path_finding[0].transform.position - this.transform.position;
                        if (Vector3.SqrMagnitude(dis) <= 1f)
                        {
                            opening_check = true;
                            this.agent.enabled = false;
                            //
                            door_info.OpenDoor();
                            //
                            current_state = State.Move;
                            Invoke("Reback_Velocity", 2f);
                        }
                    }
                    else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                    {
                        Vector3 dis = path_finding[0].transform.position - transform.position;

                        if (Vector3.SqrMagnitude(dis) <= 0.5f)
                        {
                            int random_close_door = Random.Range(0, 2);
                            if (random_close_door == 0)
                            {
                                Close_Door_Save = path_finding[0].transform.parent.gameObject;
                                Invoke("For_Close_Door_Delay", 1f);
                            }
                            path_finding.RemoveAt(0);
                            if (npc_ghost != null)
                                npc_ghost.GetComponent<Ghost>().pathfinding_list.RemoveAt(0);
                        }
                    }
                }
                else if (path_finding[0].layer == 10)//Room
                {
                    Vector3 dir = target_room.transform.position - transform.position;
                    if (Vector3.SqrMagnitude(dir) <= 3f && Vector3.SqrMagnitude(dir) >= 1f)
                    {
                        //transform.rotation = Quaternion.LookRotation(dir).normalized;
                        transform.LookAt(target_room.transform);
                    }
                    if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance < 1f) // agent.remainingDistance 
                    {
                        //룸 도착하면
                        state_end_check = true;
                    }
                }
            }
        }
        if (state_end_check)
        {
            Pathfinding_List_Initialization();
            if (next_state == State.NULL)
            {
                State_Initizlize();
                Change_State_Move();
            }
            else if (next_state != State.NULL)
            {
                State_Initizlize();
                state = next_state;
                next_state = State.NULL;
                //다음 state 설정
            }

            state_end_check = false;
        }
    }
    private void Sleep()
    {
        if (!state_end_check)
        {
            if (npc_ghost != null && opening_check == false)
            {
                this.agent.SetDestination(npc_ghost.transform.position);
            }
            else if (npc_ghost == null && opening_check == false)
            {
                if (path_finding.Count > 0)
                {
                    if (path_finding[0].layer != 10) //Room layer
                    {
                        if (this.agent.enabled == true)//형식적 확인
                            this.agent.SetDestination(path_finding[0].transform.position);
                    }
                    else if (path_finding[0].layer == 10) //Room layer
                    {
                        if (this.agent.enabled == true)
                            this.agent.SetDestination(target_item.GetComponent<Item_Info>().enter_spot.transform.position);
                    }
                }
            }

            if (path_finding.Count > 0)
            {
                if (path_finding[0].layer == 9)//Door
                {
                    if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                    {
                        var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                        Vector3 dis = path_finding[0].transform.position - this.transform.position;
                        if (Vector3.SqrMagnitude(dis) <= 1f)
                        {
                            opening_check = true;
                            this.agent.enabled = false;
                            //
                            door_info.OpenDoor();
                            //
                            current_state = State.SLEEP;
                            Invoke("Reback_Velocity", 2f);
                        }
                    }
                    else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                    {
                        Vector3 dis = path_finding[0].transform.position - transform.position;

                        if (Vector3.SqrMagnitude(dis) <= 0.5f)
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
                    if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance < 1) // agent.remainingDistance 
                    {
                        this.agent.enabled = false;
                        //상호작용 애니메이션

                        if (path_finding[0].gameObject.name == "BED_ROOM_1")
                        {
                            transform.position = new Vector3(target_item.transform.position.x, target_item.transform.position.y + 0.5f, target_item.transform.position.z);
                            Vector3 look = Vector3.zero;
                            transform.rotation = Quaternion.LookRotation(look);
                        }
                        if (path_finding[0].gameObject.name == "BED_ROOM_2")
                        {
                            transform.position = new Vector3(target_item.transform.position.x, target_item.transform.position.y + 0.5f, target_item.transform.position.z);
                            transform.rotation = target_item.transform.rotation;
                        }
                        if (current_room == target_room)
                        {
                            //상호작용 애니메이션
                            //state_end_check = true;//애니메이션 끝나면 true  ㄱ
                            anim.SetTrigger(sleep_hash);
                        }
                    }
                }
            }
        }
        //행동 끝난 경우
        if (state_end_check)
        {
            Pathfinding_List_Initialization();
            if (next_state == State.NULL)
            {
                State_Initizlize();
                Change_State_Move();
                //this.state = State.Move;

                sleepy_percent = 0;
            }
            else if (next_state != State.NULL)
            {
                state = next_state;
                //next_state = null;
                next_state = State.NULL;
                //다음 state 설정
                State_Initizlize();
                sleepy_percent = 0;
            }
            state_end_check = false;
        }

    }
    private void Pee()
    {
        current_state = State.PEE;
        if (!state_end_check)
        {
            if (npc_ghost != null && opening_check == false)
            {
                this.agent.SetDestination(npc_ghost.transform.position);
            }
            else if (npc_ghost == null && opening_check == false)
            {
                if (path_finding.Count > 0)
                {
                    if (path_finding[0].layer != 10) //Room layer
                    {
                        if (this.agent.enabled == true)//형식적 확인
                            this.agent.SetDestination(path_finding[0].transform.position);
                    }
                    else if (path_finding[0].layer == 10) //Room layer
                    {
                        if (this.agent.enabled == true)
                            this.agent.SetDestination(target_item.GetComponent<Item_Info>().enter_spot.transform.position);
                    }
                }
            }

            if (path_finding.Count > 0)
            {
                if (path_finding[0].layer == 9)//Door
                {
                    if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                    {
                        var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                        Vector3 dis = path_finding[0].transform.position - this.transform.position;
                        if (Vector3.SqrMagnitude(dis) <= 1f)
                        {
                            opening_check = true;
                            this.agent.enabled = false;
                            //
                            door_info.OpenDoor();
                            //
                            current_state = State.PEE;
                            Invoke("Reback_Velocity", 2f);
                        }
                    }
                    else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                    {
                        Vector3 dis = path_finding[0].transform.position - transform.position;

                        if (Vector3.SqrMagnitude(dis) <= 0.5f)
                        {
                            int random_close_door = Random.Range(0, 2);
                            if (random_close_door == 0)
                            {
                                Close_Door_Save = path_finding[0].transform.parent.gameObject; //문 기억
                                Invoke("For_Close_Door_Delay", 1f);
                            }

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
                        //transform.rotation = target_item.transform.rotation;
                    }
                    if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance < 0.1f) // agent.remainingDistance 
                    {
                        this.agent.enabled = false;
                        transform.rotation = target_item.transform.rotation;

                        //상호작용 애니메이션
                        if (path_finding[0].gameObject.name == "BATH_ROOM_1")
                        {
                            this.transform.position = new Vector3(-22.6f, 3.361f, -21.83f);
                        }
                        if (path_finding[0].gameObject.name == "BATH_ROOM_2")
                        {
                            this.transform.position = new Vector3(-16.89f, 3.361f, -17.84f);
                        }
                        if (path_finding[0].gameObject.name == "BATH_ROOM_3")
                        {
                            this.transform.position = new Vector3(-16.5f, 0.7f, -18.8f);
                        }
                        if (current_room == target_room)
                        {
                            //상호작용 애니메이션
                            if (once == false)
                            {
                                anim.SetTrigger(pee_hash);
                                Invoke("state_end_check_for_invoke", 5f);
                                once = true;
                            }
                        }
                    }
                }
            }
        }
        //행동 끝난 경우
        if (state_end_check)
        {
            Pathfinding_List_Initialization();
            if (next_state == State.NULL)
            {
                State_Initizlize();
                Change_State_Move();

                state_end_check = false;
                pee_percent = 0;
            }
            else if (next_state != State.NULL)
            {
                state = next_state;
                next_state = State.NULL;
                //다음 state 설정

                State_Initizlize();
                state_end_check = false;
                pee_percent = 0;
            }
            state_end_check = false;
            once = false;
        }
    }
    private void Thirst()
    {
        if (!state_end_check)
        {
            if (npc_ghost != null && opening_check == false)
            {
                this.agent.SetDestination(npc_ghost.transform.position);
            }
            else if (npc_ghost == null && opening_check == false)
            {
                if (path_finding.Count > 0)
                {
                    if (path_finding[0].layer != 10) //Room layer
                    {
                        if (this.agent.enabled == true)//형식적 확인
                            this.agent.SetDestination(path_finding[0].transform.position);
                    }
                    else if (path_finding[0].layer == 10) //Room layer
                    {
                        if (this.agent.enabled == true)
                            this.agent.SetDestination(target_item.GetComponent<Item_Info>().enter_spot.transform.position);
                    }
                }
            }

            if (path_finding.Count > 0)
            {
                if (path_finding[0].layer == 9)//Door
                {
                    if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                    {
                        var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                        Vector3 dis = path_finding[0].transform.position - this.transform.position;
                        if (Vector3.SqrMagnitude(dis) <= 1f)
                        {
                            opening_check = true;
                            this.agent.enabled = false;
                            //
                            door_info.OpenDoor();
                            //
                            current_state = State.THIRST;
                            Invoke("Reback_Velocity", 2f);
                        }
                    }
                    else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                    {
                        Vector3 dis = path_finding[0].transform.position - transform.position;

                        if (Vector3.SqrMagnitude(dis) <= 0.5f)
                        {
                            int random_close_door = Random.Range(0, 2);
                            if (random_close_door == 0)
                            {
                                Close_Door_Save = path_finding[0].transform.parent.gameObject; //문 기억
                                Invoke("For_Close_Door_Delay", 1f);
                            }

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
                    if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance < 1) // agent.remainingDistance 
                    {
                        if (current_room == target_room)
                        {
                            this.agent.enabled = false;
                            //상호작용 애니메이션


                            if (current_room == target_room)
                            {
                                //상호작용 애니메이션
                                anim.SetTrigger(thirst_hash);
                            }
                        }
                    }
                }
            }
        }
        //행동 끝난 경우
        if (state_end_check)
        {
            Pathfinding_List_Initialization();
            if (next_state == State.NULL)
            {
                State_Initizlize();
                Change_State_Move();

                state_end_check = false;
                thirst_percent = 0;
            }
            else if (next_state != State.NULL)
            {
                state = next_state;
                next_state = State.NULL;
                //다음 state 설정

                State_Initizlize();
                state_end_check = false;
                thirst_percent = 0;
            }
            state_end_check = false;
        }
    }

    bool first_report_check = false;
    private void Report()
    {
        //처음 초기화
        if (!first_report_check)
        {
            Destroy(npc_ghost);
            state = State.IDLE;
            target_item = null;
            target_room = null;
            opening_check = false;
            sleepy_percent = 0;
            sleepy_percent_check = sleepy_percent;
            pee_percent = 0;
            pee_percent_check = pee_percent;
            thirst_percent = 0;
            thirst_percent_check = thirst_percent;
            Pathfinding_List_Initialization();
            current_state = State.REPORT;
            this.agent.enabled = true;
            agent.speed = report_npc_speed;
            //npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, player.transform, ghost, this);
        }


        if (personality == Npc_Personality.AGGESSIVE)
        {
            state = State.TRACE;
            first_report_check = true;
        }
        else if (personality == Npc_Personality.Defensive)
        {
            if (!first_report_check)
            {
                int report_obj_count = NpcManager.instance.phone_items.Count;
                target_item = NpcManager.instance.phone_items[Random.Range(0, report_obj_count)].gameObject;
                npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, ghost, target_item, this);
            }
            first_report_check = true;
            if (!state_end_check)
            {
                if (npc_ghost != null && opening_check == false)
                {
                    this.agent.SetDestination(npc_ghost.transform.position);
                }
                else if (npc_ghost == null && opening_check == false)
                {
                    if (path_finding.Count > 0)
                    {
                        if (path_finding[0].layer != 10) //Room layer
                        {
                            if (this.agent.enabled == true)//형식적 확인
                                this.agent.SetDestination(path_finding[0].transform.position);
                        }
                        else if (path_finding[0].layer == 10) //Room layer
                        {
                            if (this.agent.enabled == true)
                                this.agent.SetDestination(target_item.GetComponent<Item_Info>().enter_spot.transform.position);
                        }
                    }
                }

                if (path_finding.Count > 0)
                {
                    if (path_finding[0].layer == 9)//Door
                    {
                        if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                        {
                            var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                            Vector3 dis = path_finding[0].transform.position - this.transform.position;
                            if (Vector3.SqrMagnitude(dis) <= 1f)
                            {
                                opening_check = true;
                                this.agent.enabled = false;
                                //
                                door_info.OpenDoor();
                                //
                                current_state = State.REPORT;
                                Invoke("Reback_Velocity", 2f);
                            }
                        }
                        else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                        {
                            Vector3 dis = path_finding[0].transform.position - transform.position;

                            if (Vector3.SqrMagnitude(dis) <= 0.5f)
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
                        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance < 1) // agent.remainingDistance 
                        {
                            this.agent.enabled = false;
                            //상호작용 애니메이션

                            anim.SetTrigger(call_police_hash);
                            //state_end_check = true;//애니메이션 끝나면 true  ㄱ
                        }
                    }
                }
            }
            else if (state_end_check)
            {
                //경찰 호출
                if (!NpcManager.instance.police_report)
                    NpcManager.instance.Report_Police();

                //웅크린 동작 애니메이션
            }
        }
    }

    private void Trace()
    {
        Vector3 distance = player_obj.transform.position - transform.position;
        if (current_state != State.TRACE)
        {
            npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, player.transform, ghost, this);
            //Debug.Log("Ghost생성"); 
        }
        current_state = State.TRACE;
        if (Vector3.SqrMagnitude(distance) <= 5)
        {
            agent.enabled = false;

            Vector3 look_rotation = new Vector3(player_obj.transform.position.x, transform.position.y, player_obj.transform.position.z);
            transform.LookAt(look_rotation);
            //
            switch (attack_type)
            {
                case Attack_Type.GUN:
                    ///
                    ///
                    ///
                    anim.SetTrigger(gun_hash);
                    break;
                case Attack_Type.PUNCH:
                    ///
                    ///
                    ///
                    anim.SetTrigger(punch_hash);
                    break;
                case Attack_Type.CUDGEL:
                    ///
                    ///
                    ///
                    cudgel.SetActive(true);
                    anim.SetTrigger(cudgel_hash);
                    break;
            }
            //
        }
        else if (Vector3.SqrMagnitude(distance) > 5f)
        {
            anim.ResetTrigger(gun_hash);
            anim.ResetTrigger(punch_hash);
            anim.ResetTrigger(cudgel_hash);
            Debug.Log(Vector3.SqrMagnitude(distance));
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                agent.enabled = true;

            if (npc_ghost == null) { npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, player.transform, ghost, this); }
            if (!state_end_check)
            {
                if (npc_ghost != null && opening_check == false) { this.agent.SetDestination(npc_ghost.transform.position); }
                else if (npc_ghost == null) { this.agent.SetDestination(player_obj.transform.position); }
                if (path_finding.Count > 0)
                {
                    if (path_finding[0].layer == 9)//Door
                    {
                        if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened == false)
                        {
                            var door_info = path_finding[0].transform.parent.GetComponent<DoorScript>();
                            Vector3 dis = path_finding[0].transform.position - this.transform.position;

                            if (Vector3.SqrMagnitude(dis) <= 1f)
                            {
                                opening_check = true;
                                //this.agent.enabled = false;
                                //
                                door_info.OpenDoor();
                                //
                                current_state = State.TRACE;
                                path_finding.RemoveAt(0);
                                opening_check = false;
                                Destroy(npc_ghost);
                                npc_ghost = null;
                            }
                        }
                        else if (path_finding[0].transform.parent.GetComponent<DoorScript>().Opened)
                        {
                            path_finding.RemoveAt(0);
                            if (npc_ghost != null) { npc_ghost.GetComponent<Ghost>().pathfinding_list.RemoveAt(0); }

                            Vector3 dis = path_finding[0].transform.position - transform.position;

                            if (Vector3.SqrMagnitude(dis) <= 0.5f)
                            {

                            }
                        }
                    }
                }
            }
            if (path_finding.Count > 0)
            {
                if (path_finding[0].layer == 6)
                {
                    Pathfinding_List_Initialization();
                }
            }
        }
    }
    bool faint_first_check;
    private void Faint()
    {
        //current_state = State.FAINT;
        if (!faint_first_check)
        {
            Destroy(npc_ghost);
            state = State.IDLE;
            target_item = null;
            target_room = null;
            npc_ghost = null;
            opening_check = false;
            sleepy_percent = 0;
            sleepy_percent_check = sleepy_percent;
            pee_percent = 0;
            pee_percent_check = pee_percent;
            thirst_percent = 0;
            thirst_percent_check = thirst_percent;
            Pathfinding_List_Initialization();
            //current_state = State.REPORT;
            this.agent.enabled = false;
            //agent.speed = report_npc_speed;
        }
    }
    private void Awake()
    {
        layermask_for_except = ~layermask_for_except;
        //player_obj = GameManager.instance.Player;
        //player = GameManager.instance.Player.GetComponent<Player>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();


    }

    void Start()
    {
        player_obj = GameManager.instance.Player;
        player = GameManager.instance.Player.GetComponent<Player>();

        if (npc_type != Npc_Type.POLICE)
            Invoke("Change_State_Move", 1f);

        //this.state = State.IDLE;
        //Select_Personality();
        this.personality = Npc_Personality.AGGESSIVE;

        StartCoroutine(State_Gaze_Change());
        //
        //player_texture = (Texture2D)player.GetComponent<MeshRenderer>().material.mainTexture;
        //
        //player_texture = player.GetComponent<MeshRenderer>().material.mainTexture;
    }
    //Color player_texture_Color;
    //Color screen_uv_color;


    void Update()
    {
        #region
        state_check = this.state;
        //Debug.Log(this.state);
        #endregion

        #region
        if (this.agent.enabled) { anim.SetBool(moveing_hash, true); }
        else if (!this.agent.enabled) { anim.SetBool(moveing_hash, false); }
        #endregion

        if (this.state != State.REPORT && this.state != State.TRACE)
            if (Check_Unit())
            {
                Vector3 p_dir = player.transform.position - cam.transform.position;
                if (Physics.Raycast(cam.transform.position, new Vector3(p_dir.x, p_dir.y + 0.5f, p_dir.z), out hit, Mathf.Infinity, layermask_for_except))
                {
                    Debug.DrawRay(cam.transform.position, p_dir, Color.red);
                    if (hit.transform.gameObject.layer == 6)//player
                    {
                        if (player.lighted == true)
                        {

                            ///진행중인 애니메이션 꺼주기

                            ///

                            ///Percent_Initialization
                            ///
                            current_state = State.REPORT;

                            Fear_Check();
                        }
                        #region
                        //if(hit.transform.gameObject.)
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

                        //Debug.DrawRay(cam.transform.position,hit.transform.position - cam.transform.position, Color.blue,10000000000000000000);
                        #endregion ///
                    }
                }
            }


        if (faint_gauge <= 0)
        {
            state = State.FAINT;
        }
        
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    anim.SetTrigger(gun_hash);
        //}
    }





    public bool Check_Unit()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(player_obj.transform.position);
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
        if (state_continue)
            StartCoroutine(State_Gaze_Change());
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            current_room = col.gameObject;
        }
    }
}
