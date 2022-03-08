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
        //생성하고 Npc가 공격적인지 방어적인지 정해줄거임
        //Start에서 한번만 돌려주자고~
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
                    //플레이어 감지 레이 오브젝트 컬러
                    screen_uv_color = uv_texture(tex).GetPixel((int)screen_pos.x, (int)screen_pos.y);
                 //   Debug.Log(screen_uv_color.r + ", " + screen_uv_color.g + ", " + screen_uv_color.b + "asd2");
                    //화면에서 보는 플레이어 컬러

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
                    //플레이어 감지 레이 오브젝트 컬러 //고정
                    screen_uv_color = uv_texture(tex).GetPixel((int)screen_pos.x, (int)screen_pos.y);
                    //Debug.Log(screen_pos.x + " , " + screen_pos.y);
                    Debug.Log(screen_uv_color.r + ", " + screen_uv_color.g + ", " + screen_uv_color.b + "asd2");
                    //화면에서 보는 플레이어 컬러

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
    //        //플레이어 감지 레이 오브젝트 컬러
    //        screen_uv_color = uv_texture(tex).GetPixel((int)screen_pos.x, (int)screen_pos.y);
    //        Debug.Log(screen_uv_color.r + ", " + screen_uv_color.g + ", " + screen_uv_color.b + "asd2");
    //        cool_t = 0;
    //    }
    //}//

    public void Fear_Check()//경계도 100 이상 되엇을 때 
    {
        if (fear_percent == 100)
        {
            this.state = State.REPORT;
            Report_Pattern();
        }
    }

    public void Report_Pattern()//신고 했을 떄 성격 확인
    {
        if (this.personality == Npc_Personality.AGGESSIVE && IsVisible(cam,player))
        {
            //오래동안 도둑을 놓치면 다시 신고하러 가도록
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
        //npc 기절,신고 등의 상태에서 코루틴 꺼주기
    }

    
    
}
