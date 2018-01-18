﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public class PlayerStats
    {
        public int Health = 100;

    }

    public PlayerStats playerStats = new PlayerStats();

    public void DamagePlayer (int damage)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

    

}
