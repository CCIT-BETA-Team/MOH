using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimeEvent : MonoBehaviour
{
    public Player player;

    void On_Off_Attack()
    {
        player.is_attack = player.is_attack ? player.is_attack = false : player.is_attack = true;
    }
}
