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

    public List<Transform> path_finding = new List<Transform>();

    public int npc_speed;//Npc �̵��ӵ�
    public int faint_time;//�����ð�

    public GameObject player;
    public LayerMask layermask;

    public enum Npc_Type
    {
        NONE,
        POLICE,//���� // ���౸��x
        GRANDFATHER,//�Ҿƹ���
        GRANDMA,//�ҸӴ�
        MAN,//����
        WOMAN,//����
    }
    public Npc_Type npc_type = Npc_Type.NONE;
    
    
    public enum State 
    {
        IDLE,//���
        Move,//����
        SLEEP,//����
        HUNGRY,//�����
        PEE,//ȭ���
        THIRST,//�񸶸�
        REPORT,//�Ű�
        FAINT,//����
        ATTACK,//���� // 2������ �Ű� �� �ڿ��� ���� ����
        ESCAPE,//����// 2������ �Ű� �� �ڿ��� ���� ����
        TRACE,//����// 2������ �Ű� �� �ڿ��� ���� ����
        FEAR//��� 
    }
    public State state = State.IDLE;



    public enum parametertype
    {
        NONE,
        SLEEP,
        HUNGRY,
        PEE,
        THIRST,
        FEAR
    }
    public parametertype parameter;
    //� ������ �ø�����

    public enum Npc_Personality
    {
        AGGESSIVE,
        Defensive
    }
    public Npc_Personality personality = Npc_Personality.AGGESSIVE;
    //���������� ���������
    


    [Header("Npc ���º� ������")]
    [Range(0, 100)]
    public float sleepy_percent;//����
    [Range(0, 100)]
    public float hungry_percent;//�����
    [Range(0, 100)]
    public float pee_percent;//ȭ���
    [Range(0, 100)]
    public float thirst_percent;//�񸶸�
    [Range(0, 100)]
    public float fear_percent;//���

    
    public void Gazechange(float value,parametertype type)
    {
        switch (type)
        {
            case parametertype.SLEEP:
                if (sleepy_percent <= 100f)
                    sleepy_percent += value * (Random.value);
                break;
            case parametertype.HUNGRY:
                if (hungry_percent <= 100f)
                    hungry_percent += value * (Random.value);
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

    public void Allup(float value,bool Fear_Check)//��赵 �ø�����??
    {
        Gazechange(value * (Random.value / 3), parametertype.SLEEP);
        Gazechange(value * (Random.value / 3), parametertype.HUNGRY);
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
                Gazechange(3, parametertype.HUNGRY);
                Gazechange(3, parametertype.PEE);
                Gazechange(3, parametertype.THIRST);
                break;
            case State.HUNGRY :
                Gazechange(3, parametertype.SLEEP);
                Gazechange(3, parametertype.PEE);
                Gazechange(3, parametertype.THIRST);
                break;
            case State.PEE :
                Gazechange(3, parametertype.HUNGRY);
                Gazechange(3, parametertype.SLEEP);
                Gazechange(3, parametertype.THIRST);
                break;
            case State.THIRST :
                Gazechange(3, parametertype.HUNGRY);
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
    

  



    [Space]
    public List<GameObject> npc_item = new List<GameObject>();//Npc�� ������ ������ ����Ʈ



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

   





    public abstract void Move();
    public abstract void Select_Personality();

    
}
