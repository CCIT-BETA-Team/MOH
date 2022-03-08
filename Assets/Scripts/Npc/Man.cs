using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Npc
{




    RaycastHit hit;
    public Camera cam;

    public override void Move()
    {
        this.state = State.Move;
    }

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
        if (IsVisible(cam, player))
        {
            if(Physics.Raycast(cam.transform.position,(player.transform.position - cam.transform.position),out hit, Mathf.Infinity))
            {
                Debug.DrawRay(cam.transform.position, (player.transform.position - cam.transform.position) * hit.distance, Color.yellow);
                
                if(hit.transform.gameObject.layer == 6)//player
                {
                    Vector2 player_uv = hit.textureCoord;
                    Vector2 screen_pos = cam.WorldToViewportPoint(player.transform.position);

                    player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
                 //   Debug.Log(player_texture_Color.r + ", " + player_texture_Color.g + ", " + player_texture_Color.b + "asd1");
                    //�÷��̾� ���� ���� ������Ʈ �÷�
                    screen_uv_color = uv_texture(tex).GetPixel((int)screen_pos.x, (int)screen_pos.y);
                 //   Debug.Log(screen_uv_color.r + ", " + screen_uv_color.g + ", " + screen_uv_color.b + "asd2");
                    //ȭ�鿡�� ���� �÷��̾� �÷�

                }
            }
        }
    }
    private void OnRenderObject()
    {
        if (IsVisible(cam, player))
        {
            if (Physics.Raycast(cam.transform.position, (player.transform.position - cam.transform.position), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(cam.transform.position, (player.transform.position - cam.transform.position) * hit.distance, Color.yellow);

                if (hit.transform.gameObject.layer == 6)//player
                {
                    Vector2 player_uv = hit.textureCoord;
                    Vector2 screen_pos = cam.WorldToViewportPoint(player.transform.position);

                    player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
                    //Debug.Log(player_texture_Color.r + ", " + player_texture_Color.g + ", " + player_texture_Color.b + "asd1");
                    //�÷��̾� ���� ���� ������Ʈ �÷� //����
                    screen_uv_color = uv_texture(tex).GetPixel((int)screen_pos.x, (int)screen_pos.y);
                    //Debug.Log(screen_pos.x + " , " + screen_pos.y);
                    Debug.Log(screen_uv_color.r + ", " + screen_uv_color.g + ", " + screen_uv_color.b + "asd2");
                    //ȭ�鿡�� ���� �÷��̾� �÷�

                }
            }
        }

    }
    //void cool(float cooltime)
    //{
    //    cool_t += Time.deltaTime;
    //    if (cool_t >= cooltime)
    //    {
    //        player_texture_Color = player_texture.GetPixel((int)player_uv.x, (int)player_uv.y);
    //        Debug.Log(player_texture_Color.r + ", " + player_texture_Color.g + ", " + player_texture_Color.b + "asd1");
    //        //�÷��̾� ���� ���� ������Ʈ �÷�
    //        screen_uv_color = uv_texture(tex).GetPixel((int)screen_pos.x, (int)screen_pos.y);
    //        Debug.Log(screen_uv_color.r + ", " + screen_uv_color.g + ", " + screen_uv_color.b + "asd2");
    //        cool_t = 0;
    //    }
    //}//

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
        if (this.personality == Npc_Personality.AGGESSIVE && IsVisible(cam,player))
        {
            //�������� ������ ��ġ�� �ٽ� �Ű��Ϸ� ������
        }
        else if(this.personality == Npc_Personality.AGGESSIVE && IsVisible(cam, player) == false)
        {
            this.state = State.TRACE;
        }
        ////
        if(this.personality == Npc_Personality.Defensive && IsVisible(cam, player))
        {

        }
        else if(this.personality == Npc_Personality.Defensive && IsVisible(cam, player) == false)
        {

        }
    }

    public void Go_Report_Zone()
    {

    }




    public bool IsVisible(Camera cam,GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }
  


    IEnumerator State_Gaze_Change()
    {
        yield return new WaitForSeconds(2f);
        StateGazeUp(this.state);
        StartCoroutine(State_Gaze_Change());
        //npc ����,�Ű� ���� ���¿��� �ڷ�ƾ ���ֱ�
    }

    
    
}
