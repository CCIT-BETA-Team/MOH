using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : Npc
{
    public Animator anime;

    private void Awake()
    {
        
    }
    void Start()
    {
        attack_type = Attack_Type.GUN;

        player_obj = GameManager.instance.Player;
        player = GameManager.instance.Player.GetComponent<Player>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (this.agent.enabled) { anim.SetBool(moveing_hash, true); }
        else if (!this.agent.enabled) { anim.SetBool(moveing_hash, false); }

        Trace();
    }
    private void Trace()
    {
        Vector3 distance = player_obj.transform.position - this.gameObject.transform.position;
        if (current_state != State.TRACE)
        {
            npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, player.transform, ghost, this);
            
            //Debug.Log("Ghost생성"); 
        }
        current_state = State.TRACE;
        if (Vector3.SqrMagnitude(distance) <= 5f)
        {
            agent.enabled = false;

            Vector3 look_rotation = new Vector3(player_obj.transform.position.x, transform.position.y, player_obj.transform.position.z);
            transform.LookAt(look_rotation);
            anim.SetTrigger(gun_hash);
        }
        else if (Vector3.SqrMagnitude(distance) > 5f)
        {
            anim.ResetTrigger(gun_hash);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("running"))
                agent.enabled = true;

            if (npc_ghost == null) { npc_ghost = NpcManager.instance.Ins_Ghost(this.transform, player.transform, ghost, this);  }
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
    #region


    //애니메이션 이벤트 현재 공격중인지 체크
    bool is_attack;
    //애니메이션 트리거 HashCode
    int gun_attack_hashcode = Animator.StringToHash("GunAttack");
    int punch_attack_hashcode = Animator.StringToHash("PunchAttack");
    int cudgel_attack_hashcode = Animator.StringToHash("CudgelAttack");

    
    public delegate void Atk();
    public Atk Attack;

    void Set_Attack()
    {
        switch(attack_type)
        {
            case Attack_Type.GUN:
                Attack = Gun_Attack;
                break;
            case Attack_Type.PUNCH:
                Attack = Punch_Attack;
                break;
            case Attack_Type.CUDGEL:
                Attack = Cudgel_Attack;
                break;
        }
    }

    void Gun_Attack() { anime.SetTrigger(gun_attack_hashcode); }

    // 이 이벤트는 무기 별로 만들기 보다 파라미터로 오브젝트 받아서 하나로 통합하는걸로 수정해도 될듯 난 못하겠음;
    void Gun_Ani_Event()
    {
        if(!is_attack)
        {
            gun.SetActive(true);
            is_attack = true;
        }
        else
        {
            gun.SetActive(false);
            is_attack = false;
        }
    }

    void Gun_Shot_Event()
    {
        GameObject bullet = Instantiate(gun_bullet, gun.transform.position, transform.rotation);
        bullet.GetComponent<Gun_Bullet>().parent_npc = gameObject;
    }

    void Punch_Attack() { anime.SetTrigger(punch_attack_hashcode); }

    void Punch_Ani_Event()
    {
        if (!is_attack)
        {
            //주먹 닿으면 데미지? 암튼 알아서 하셈
            is_attack = true;
        }
        else
        {
            //주먹 닿으면 데미지 주는거 끄기.
            is_attack = false;
        }
    }

    void Cudgel_Attack() { anime.SetTrigger(cudgel_attack_hashcode); }

    void Cudgel_Ani_Event()
    {
        if (!is_attack)
        {
            //곤봉 닿으면 데미지? 암튼 알아서 하셈
            cudgel.SetActive(true);
            is_attack = true;
        }
        else
        {
            //곤봉 닿으면 데미지 주는거 끄기.
            cudgel.SetActive(false);
            is_attack = false;
        }
    }
    #endregion


}
