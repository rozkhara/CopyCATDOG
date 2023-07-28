using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Controller
{
    // This script inherits properties from the Controller script

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.timeScale > 0f)
        {
            HandleBombSpawn();
            UseNeedle();
        }
    }

    protected override void HandlePlayerMovement()
    {
        base.HandlePlayerMovement();
    }
    protected override void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player1Bomb") && CurrentBombs < BombCount && !Flowed)
        {
            base.HandleBombSpawn();
        }
    }

    protected override void UseNeedle()
    {
        if (Input.GetButtonDown("Player1Needle") && Needle > 0 && Flowed)
        {
            base.UseNeedle();
        }
    }

}