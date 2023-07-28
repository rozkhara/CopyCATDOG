using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Controller
{
    // This script inherits properties from the Controller script

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        Debug.Log($"Player2 - Speed: {PlayerSpeedInit}, Range: {PlayerRange}, BombCount: {BombCount}, MaxSpeed: {MaxSpeed}, MaxRange: {MaxRange}, MaxBomb: {MaxBomb}");
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
        //Debug.Log($"Player2 - Speed: {PlayerSpeed}, Range: {PlayerRange}, BombCount: {BombCount}, ExplosionRange: {BombExplosionRange}");
    }

    protected override void HandleBombSpawn()
    {
        if (Input.GetButtonDown("Player2Bomb") && CurrentBombs < BombCount && !Flowed)
        {
            Debug.Log(Flowed);
            base.HandleBombSpawn();
        }
    }

    protected override void UseNeedle()
    {
        if (Input.GetButtonDown("Player2Needle") && Needle > 0 && Flowed)
        {
            base.UseNeedle();
        }
            
    }
}
