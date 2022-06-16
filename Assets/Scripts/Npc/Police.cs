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
        //�̷��� �˾Ƽ� �ϰ��� ����
    }

    //���߿� �������� NPC Script�� �ű� ����.
    public enum Attack_Type { GUN, PUNCH, CUDGEL }
    [Header("NPC ���� Ÿ��")]
    public Attack_Type attack_type;

    [Header("���� ������Ʈ")]
    public GameObject gun;
    public GameObject gun_bullet;
    public GameObject cudgel;

    //�ִϸ��̼� �̺�Ʈ ���� ���������� üũ
    bool is_attack;
    //�ִϸ��̼� Ʈ���� HashCode
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

    // �� �̺�Ʈ�� ���� ���� ����� ���� �Ķ���ͷ� ������Ʈ �޾Ƽ� �ϳ��� �����ϴ°ɷ� �����ص� �ɵ� �� ���ϰ���;
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
            //�ָ� ������ ������? ��ư �˾Ƽ� �ϼ�
            is_attack = true;
        }
        else
        {
            //�ָ� ������ ������ �ִ°� ����.
            is_attack = false;
        }
    }

    void Cudgel_Attack() { anime.SetTrigger(cudgel_attack_hashcode); }

    void Cudgel_Ani_Event()
    {
        if (!is_attack)
        {
            //��� ������ ������? ��ư �˾Ƽ� �ϼ�
            cudgel.SetActive(true);
            is_attack = true;
        }
        else
        {
            //��� ������ ������ �ִ°� ����.
            cudgel.SetActive(false);
            is_attack = false;
        }
    }
}
