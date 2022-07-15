using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Effect : MonoBehaviour
{
    public Animator ani;
    public GameObject gun_effect;
    public Transform effect_trans;

    /// <summary>
    /// 
    /// </summary>
    readonly int Shot = Animator.StringToHash("Shot");


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject gun_light;
    public float kang_seong_jun;
    public void Gun_Light_On()
    {
        //if(gun_light == null)
        //gun_light = Instantiate(gun_effect, effect_trans.position , Quaternion.Euler(0, 0, 0));
        ////gun_light.transform.localRotation = Quaternion.Euler(0, 90 + transform.root.rotation.y, 0);

        ////gun_light.transform.parent = effect_trans;
        //Debug.Log(23);
        gun_effect.SetActive(true);
        ani.SetTrigger(Shot);
        GameManager.instance.Player.GetComponent<Player>().health -= 30; 
        Invoke("Destroy_Light",0.1f);
    }

    void Destroy_Light()
    {
        gun_effect.SetActive(false);
    }
}
