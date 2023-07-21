using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Controller
{
    // This script inherits properties from the Controller script

    private void Start()
    {
        PlayerSpeed = 5.0f;
        MaxBomb = 4;
    }

    private void Update()
    {
        HandleBombSpawn();
    }
    protected override void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player1Bomb") && CurrentBombs < MaxBomb)
        {
            base.HandleBombSpawn();
        }
    }

}