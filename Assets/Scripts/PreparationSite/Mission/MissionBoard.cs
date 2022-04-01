using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionBoard : Item
{
    public Player player;
    public Camera ply_cam;
    public Camera mission_cam;

    public Vector3[] mission_board_io;
    public Vector3[] borad_to_board;

    public Animation mission_cam_anime;
    public MissionCameraEvents mce;
    public Canvas mission_board_canvas;

    public bool mission_board_in;
    public bool board_state;

    public Button[] btn_list;
    public Image[] btn_image_list;

    enum Animation_Events_State { BOARD_IN, BOARD_OUT }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mission_board_canvas.enabled = false;
            mission_board_in = false;
            board_state = false;
            Camera_Move(mission_cam_anime, mission_cam.transform.position, ply_cam.transform.position, mission_cam.transform.eulerAngles, player.transform.eulerAngles, 1.2f, Animation_Events_State.BOARD_OUT); ;
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && board_state && mission_board_in)
        {
            board_state = false;
            Camera_Move(mission_cam_anime, borad_to_board[2], borad_to_board[0], borad_to_board[3], borad_to_board[1], 0.5f);
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !board_state && mission_board_in)
        {
            board_state = true;
            Camera_Move(mission_cam_anime, borad_to_board[0], borad_to_board[2], borad_to_board[1], borad_to_board[3], 0.5f);
        }
    }

    public override void interaction()
    {
        Camera_Switch();
    }

    void Camera_Switch()
    {
        ply_cam.enabled = false;
        mission_cam.enabled = true;
        mission_board_in = true;
        Camera_Move(mission_cam_anime, ply_cam.transform.position, mission_board_io[0], player.transform.eulerAngles, mission_board_io[1], 1.2f, Animation_Events_State.BOARD_IN);
        player.freeze = true;
    }

    void Camera_Move(Animation anime, Vector3 start_pos, Vector3 end_pos, Vector3 start_rotation, Vector3 end_rotation, float time)
    {
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        AnimationCurve curve = AnimationCurve.Linear(0.0f, start_pos.x, time, end_pos.x);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

        curve = AnimationCurve.Linear(0.0f, start_pos.y, time, end_pos.y);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        curve = AnimationCurve.Linear(0.0f, start_pos.z, time, end_pos.z);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

        curve = AnimationCurve.Linear(0.0f, start_rotation.x, time, end_rotation.x);
        clip.SetCurve("", typeof(Transform), "localRotation.x", curve);

        curve = AnimationCurve.Linear(0.0f, start_rotation.y, time, end_rotation.y);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);

        curve = AnimationCurve.Linear(0.0f, start_rotation.z, time, end_rotation.z);
        clip.SetCurve("", typeof(Transform), "localRotation.z", curve);

        anime.AddClip(clip, clip.name);
        anime.Play(clip.name);
    }

    void Camera_Move(Animation anime, Vector3 start_pos, Vector3 end_pos, Vector3 start_rotation, Vector3 end_rotation, float time, Animation_Events_State aes)
    {
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        AnimationCurve curve = AnimationCurve.Linear(0.0f, start_pos.x, time, end_pos.x);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

        curve = AnimationCurve.Linear(0.0f, start_pos.y, time, end_pos.y);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        curve = AnimationCurve.Linear(0.0f, start_pos.z, time, end_pos.z);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

        curve = AnimationCurve.Linear(0.0f, start_rotation.x, time, end_rotation.x);
        clip.SetCurve("", typeof(Transform), "localRotation.x", curve);

        curve = AnimationCurve.Linear(0.0f, start_rotation.y, time, end_rotation.y);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);

        curve = AnimationCurve.Linear(0.0f, start_rotation.z, time, end_rotation.z);
        clip.SetCurve("", typeof(Transform), "localRotation.z", curve);

        AnimationEvent evt = new AnimationEvent();
        evt.time = time;
        evt.functionName = "Camera_Event";
        Event_Delegate(aes);

        clip.AddEvent(evt);

        anime.AddClip(clip, clip.name);
        anime.Play(clip.name);
    }

    void Event_Delegate(Animation_Events_State aes)
    {
        switch (aes)
        {
            case Animation_Events_State.BOARD_IN:
                mce.AE = Board_In_Event;
                break;
            case Animation_Events_State.BOARD_OUT:
                mce.AE = Board_Out_Event;
                break;
        }
    }

    void Board_In_Event()
    {
        mission_board_canvas.enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Board_Out_Event()
    {
        ply_cam.enabled = true;
        mission_cam.enabled = false;
        player.freeze = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
