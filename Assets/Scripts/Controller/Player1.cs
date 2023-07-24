using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Controller
{
    // This script inherits properties from the Controller script


    private void Update()
    {
        HandleBombSpawn();
        UseNeedle();
    }

    protected override void HandlePlayerMovement()
    {
        base.HandlePlayerMovement();
        //Debug.Log($"Player2 - Speed: {PlayerSpeed}, Range: {PlayerRange}, MaxBomb: {MaxBomb}, ExplosionRange: {BombExplosionRange}");
    }
    protected override void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player1Bomb") && CurrentBombs < MaxBomb && !Flowed)
        {
            base.HandleBombSpawn();
        }
    }

    protected override void UseNeedle()
    {
        if (Input.GetButtonDown("Player1Needle") && this.Needle > 0 && this.Flowed)
        {
            base.UseNeedle();
        }
    }

}