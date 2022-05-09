using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Light light;
    public GameObject[] material_target;
    List<Material> light_material = new List<Material>();
    bool electricity = true;
    public bool broken = false;
    public bool on_off = false;
    public float cool = 0.0f;
    public List<Animation> light_ani = new List<Animation>();
    public bool player_lighting = false;
    private float light_range;
    

    public bool broke_property
    {
        get
        { return broken; }
        set
        { broken = value; Light_Update(); }
    }
    public bool electricity_property { get { return electricity; } set { electricity = value; Light_Update(); } }
    private void Start()
    {
    
       
        light_range = light.range;
        if(GetComponent<SphereCollider>()==null)
        {
            gameObject.AddComponent<SphereCollider>().isTrigger=true;  
        }
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = light_range;
        col.center = new Vector3(0, 0, 0);
        foreach (GameObject a in material_target)
        {
            light_material.Add(a.GetComponent<MeshRenderer>().material);
        }
    }
    private void Update()
    {
        if (broke_property)
        {
            light.enabled = false;
            emission(0);
        }
        else
        {
            if (electricity)
            {
                if (on_off)
                {
                    light.enabled = true;
                    emission(1);
                }
                else
                {
                    light.enabled = false;
                    emission(0);
                }
            }
            else
            {
                light.enabled = false;
                emission(0);
            }
        }
        if(cool>0 && broke_property)
        {
            cool = Mathf.Clamp(cool - Time.deltaTime, 0, 600);
        }
        if(cool == 0 && broke_property)
        {
            broke_property = false;
        }
    }



    public void Light_Update()
    {
        if (broke_property)
        {
            light.enabled = false;
        }
        else
        {
            if (on_off)
            {
                light.enabled = true;
                
            }
            else
            {
                light.enabled = false;
                emission(0);
            }
        }
    }

    public void emission(int on_off)
    {
    if(light_material!=null)
    {

            foreach (Material m in light_material)
            {
                if(on_off==1)
                {
                    m.EnableKeyword("_EMISSION");
                }
                else
                {
                    m.DisableKeyword("_EMISSION");
                }
             
          }
        }
  
    }




    public void Break_Light(float time)
    {
        broke_property = true;
        if(cool != -1)
        {
            cool = time;
        }
        //플레이어에 라이트 오브젝트 확인후 제거
    }
    /// <summary>
    /// 영구파괴
    /// </summary>
    public void Break_Light()
   {
        broke_property = true;
        cool = -1;
        //플레이어에 라이트 오브젝트 확인후 제거
    }




    private void OnCollisionEnter(Collision collision)
    {
        //수정필요
        if(collision.transform.tag=="Player"&& on_off&&!broken)
        {
            collision.transform.GetComponent<p_Player>().Enter_Light(this);
        }
        if(collision.gameObject.GetComponent<Item>()!=null&& player_lighting)
        {
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //수정필요
        if (collision.transform.tag == "Player" && on_off && !broken)
        {
            collision.transform.GetComponent<p_Player>().Exit_Light(this);
        }
    }
}
