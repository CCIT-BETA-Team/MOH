using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenShot : MonoBehaviour
{
    public Player player;
    public Texture2D screen_texture;
    public bool take_screen = false;

    void Screen_Shot()
    {
        screen_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect texture_area = new Rect(0f, 0f, Screen.width, Screen.height);
        screen_texture.ReadPixels(texture_area, 0, 0);
        screen_texture.Apply();
        GameManager.instance.screen_sprite = Sprite.Create(screen_texture, texture_area, new Vector2(0.5f, 0.5f));
    }

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    public void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    public void OnPostRender()
    {
        if (take_screen)
        {
            PopupManager.instance.Popup_On(2);
            Screen_Shot();
            player.Pick_Up();
            take_screen = false;
        }
    }
}
