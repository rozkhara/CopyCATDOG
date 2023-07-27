using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Controller
{
    // This script inherits properties from the Controller script

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleBombSpawn();
        UseNeedle();
    }

    protected override void HandlePlayerMovement()
    {
        float MoveX = Input.GetAxisRaw("Player2Horizontal");
        float MoveY = Input.GetAxisRaw("Player2Vertical");
        Move(MoveX, MoveY);
        //Debug.Log($"Player2 - Speed: {PlayerSpeed}, Range: {PlayerRange}, MaxBomb: {MaxBomb}, ExplosionRange: {BombExplosionRange}");
    }

    protected override void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player2Bomb") && CurrentBombs < MaxBomb && !this.Flowed)
        {
            base.HandleBombSpawn();
        }
    }

    protected override void UseNeedle()
    {
        if (Input.GetButtonDown("Player2Needle") && this.Needle > 0 && this.Flowed)
        {
            base.UseNeedle();
        }
            
    }
}
