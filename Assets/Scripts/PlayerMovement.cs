using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Mover
{
    Player player;

    protected override void Start()
    {
        EnemyBehavior.OnPlayerCollided += HandlePlayerEatenAttempt;

        directionAvailability = wallChecker.getDirectionAvailability();

        player = GetComponent<Player>();
    }
    void FixedUpdate()
    {
        PlayerMovementControls();
    }

    private void OnDisable()
    {
        EnemyBehavior.OnPlayerCollided -= HandlePlayerEatenAttempt;
    }

    public void HandlePlayerEatenAttempt()
    {
        if (player.isInvincible == false)
        {
            StopAllCoroutines();        
            player.UpdateTotalLives();            
            StartCoroutine(player.GoInvincible());
            player.movePlayerToStartingPos();
            StartCoroutine(FreezeMovment(.5f));                        
        }       
    }


    private void PlayerMovementControls()
    {
        if (!isLerping && directionAvailability[WallChecker.Direction.up] && Input.GetKey(KeyCode.W))
            StartCoroutine(Move(Vector3.up));
        if (!isLerping && directionAvailability[WallChecker.Direction.left] && Input.GetKey(KeyCode.A))
            StartCoroutine(Move(Vector3.left));
        if (!isLerping && directionAvailability[WallChecker.Direction.down] && Input.GetKey(KeyCode.S))
            StartCoroutine(Move(Vector3.down));
        if (!isLerping && directionAvailability[WallChecker.Direction.right] && Input.GetKey(KeyCode.D))
            StartCoroutine(Move(Vector3.right));
    }
}
