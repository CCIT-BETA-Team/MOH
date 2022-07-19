using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    //public Player player;
    public Camera[] cam;
    public GameObject[] main_ui;
    public RawImage[] fade_image;
    public float speed;

    public enum Fade_State { IN, OUT}

    public IEnumerator CamearaSwitch(Fade_State fade_state, Camera out_cam, Camera in_cam, RawImage out_image, RawImage in_image)
    {
        float alpha = (fade_state == Fade_State.OUT) ? 1 : 0;
        float end_value = (fade_state == Fade_State.OUT) ? 0 : 1;
        if (fade_state == Fade_State.OUT)
        {
            in_image.enabled = true;
            while (alpha >= end_value)
            {
                SetColorImage(ref alpha, fade_state, in_image);
                yield return null;
            }
            in_image.enabled = false;
        }
        else
        {
            in_image.enabled = true;
            while (alpha <= end_value)
            {
                SetColorImage(ref alpha, fade_state, in_image);
                yield return null;
            }
            in_cam.enabled = false;
            out_cam.enabled = true;
            StartCoroutine(CamearaSwitch(Fade_State.OUT, in_cam, out_cam, in_image, out_image));
            in_image.enabled = false;
        }
    }

    void SetColorImage(ref float alpha, Fade_State fade_state, RawImage Image)
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / speed) * ((fade_state == Fade_State.OUT) ? -1 : 1);
    }

    public void LobieToSite()
    {
        StartCoroutine(CamearaSwitch(Fade_State.IN, GameManager.instance.player_comp.cam, cam[1], fade_image[0], fade_image[1]));
        PopupManager.instance.current_popup.SetActive(true);
        PopupManager.instance.ip_.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.player_comp.freeze = false;
        PopupManager.instance.cross_head.enabled = true;
    }
}
