using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Npc
{
    RaycastHit hit;//����
    public Camera cam;//Npc ��

    
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
        //�����ϰ� Npc�� ���������� ��������� �����ٰ���
        //Start���� �ѹ��� �������ڰ�~
    }

    //��ũ�� �������� ���ϴ� �� Update�� ����ؼ� �޾ƿ����� �ѹ� �����غ�����;;

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
            }
        }
    }
    State? next_state;

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
                    //npc_ghost = Instantiate(ghost,new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z),Quaternion.identity); 
                    //npc_ghost.GetComponent<Ghost>().parent_npc = this;
                    //npc_ghost.transform.position = this.transform.position;
                    //npc_ghost.GetComponent<Ghost>().Move_Point(npc_room);
                    //agent.SetDestination(player.transform.position);

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
                    this.state = State.HUNGRY;
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




    private void Sleep()
    {
        //�ڱ� �� ħ��� �̵�

        //�̵�


        //����


        //������ ���� ���� üũ

        sleepy_percent = 0;
        if (next_state != null) 
        {
            this.state = next_state.Value;
            next_state = null;
        }
        else { this.state = State.Move; }
        

        //���� �������� ���� ���� �������� ���� ��ȭ





        //������ Move�� ��ȭ ����

    }
    private void Hungry()
    {
        hungry_percent = 0;
        hungry_percent_check = hungry_percent;
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



    void Update()
    {
        state_check = this.state;
      

        if (Check_Unit())
        {
            if(Physics.Raycast(cam.transform.position,(player.transform.position - cam.transform.position),out hit, Mathf.Infinity))
            {
                Debug.DrawRay(cam.transform.position, (player.transform.position - cam.transform.position) * hit.distance, Color.yellow);
                
                if(hit.transform.gameObject.layer == 6)//player
                {
                    Vector2 player_uv = hit.textureCoord;
                    Vector2 screen_pos = cam.WorldToViewportPoint(player.transform.position);
                    
                    player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
                    screen_uv_color = uv_texture(tex).GetPixel((int)(Screen.width * screen_pos.x),(int)(Screen.height * screen_pos.y));



                    //Debug.Log("Pixel position   :   "+screen_pos.x +"   "+screen_pos.y);

                    //Debug.Log("Color R : " + player_texture_Color.r + " , " + "Color G : " + +player_texture_Color.g + " , " + "Color B : " + +player_texture_Color.b + " , " + "Texture uv Pixel Color");


                    //Debug.Log("Color R : " + screen_uv_color.r + " , " + "Color G : " + + screen_uv_color.g + " , "+"Color B : " + + screen_uv_color.b + " , "+ "Scene uv Pixel Color");
                    ////ȭ�鿡�� ���� �÷��̾� �÷�

                }
            }
        }//�÷��̾� ī�޶� �� �ȿ� ���� ���� Ȯ��
    }
 
    public void Fear_Check()//��赵 100 �̻� �Ǿ��� �� 
    {
        if (fear_percent == 100)
        {
            this.state = State.REPORT;
            Report_Pattern();
        }
    }

    public void Report_Pattern()//�Ű� ���� �� ���� Ȯ��
    {
        if (this.personality == Npc_Personality.AGGESSIVE && Check_Unit())
        {
            //�������� ������ ��ġ�� �ٽ� �Ű��Ϸ� ������
        }
        else if(this.personality == Npc_Personality.AGGESSIVE && Check_Unit() == false)
        {
            this.state = State.TRACE;
        }
        ////
        if(this.personality == Npc_Personality.Defensive && Check_Unit())
        {

        }
        else if(this.personality == Npc_Personality.Defensive && Check_Unit() == false)
        {

        }
    }

    public bool Check_Unit()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(player.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
    public void Go_Report_Zone()
    {

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
