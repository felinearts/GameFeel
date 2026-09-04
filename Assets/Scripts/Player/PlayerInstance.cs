using System;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    Player player;
    private static PlayerInstance instance;
    
    private void Awake()
    {
        player = GetComponent<Player>();
        instance = this;
    }

    public static Player GetPlayer()
    {
        return instance.player;
    }
}
