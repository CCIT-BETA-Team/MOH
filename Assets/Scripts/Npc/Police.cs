using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Npc
{
    public Animator anime;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { Gun_Attack(); }
    }

    void Move()
    {
        //이러면 알아서 하겠지 ㅋㅋ
    }

    //나중에 강성준이 NPC Script로 옮길 예정.
    public enum Attack_Type { GUN, PUNCH, CUDGEL }
    [Header("NPC 공격 타입")]
    public Attack_Type attack_type;

    [Header("무기 오브젝트")]
    public GameObject gun;
    public GameObject gun_bullet;
    public GameObject cudgel;

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
}
