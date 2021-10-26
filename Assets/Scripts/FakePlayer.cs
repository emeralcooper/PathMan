using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FakePlayer : Mover
{
    [SerializeField] float delayMovementAtStart = 1f;

    private float gameTimer = 0;
    private bool allowBackwardMov;
    private bool isMovingUp, isMovingLeft, isMovingDown, isMovingRight;
    private int upMoveAttempts, leftMoveAttemps, downMoveAttempts, rightMoveAttempts;
    private bool isMoving;
    bool gameHasStarted;

    public static event Action OnPlayerCollided;
    IEnumerator StopFranticMode;

    protected override void Start()
    {
        directionAvailability = wallChecker.getDirectionAvailability();
    }

    void Update()
    {
        gameTimer += Time.deltaTime;


        if (delayMovementAtStart > gameTimer)
        {
            return;
        }

        if (!isMoving && !allowBackwardMov)
        {
            isMoving = true;
            StartCoroutine(MoveRandomly());
        }

        if (!isMoving && allowBackwardMov)
        {
            isMoving = true;
            StartCoroutine(MoveRandomlyAllowBackwards());
        }      
    }
  

    private IEnumerator MoveRandomly()
    {
        if (!isLerping)
        {
            int a = UnityEngine.Random.Range(0, 4);
            switch (a)
            {
                case 0:
                    if (directionAvailability[WallChecker.Direction.up] && !isMovingDown && upMoveAttempts < 1)
                    {
                        isMovingUp = true;
                        isMovingDown = false;
                        isMovingLeft = false;
                        isMovingRight = false;
                        StartCoroutine(Move(Vector3.up));
                        ResetAllMoveAttempts();
                    }
                    else
                        upMoveAttempts++;

                    break;
                case 1:
                    if (directionAvailability[WallChecker.Direction.left] && !isMovingRight && leftMoveAttemps < 1)
                    {
                        isMovingUp = false;
                        isMovingDown = false;
                        isMovingLeft = true;
                        isMovingRight = false;
                        StartCoroutine(Move(Vector3.left));
                        ResetAllMoveAttempts();
                    }
                    else
                        leftMoveAttemps++;
                    break;
                case 2:
                    if (directionAvailability[WallChecker.Direction.down] && !isMovingUp && downMoveAttempts < 1)
                    {
                        isMovingUp = false;
                        isMovingDown = true;
                        isMovingLeft = false;
                        isMovingRight = false;
                        StartCoroutine(Move(Vector3.down));
                        ResetAllMoveAttempts();
                    }
                    else
                        downMoveAttempts++;
                    break;
                case 3:
                    if (directionAvailability[WallChecker.Direction.right] && !isMovingLeft && rightMoveAttempts < 1)
                    {
                        isMovingUp = false;
                        isMovingDown = false;
                        isMovingLeft = false;
                        isMovingRight = true;
                        StartCoroutine(Move(Vector3.right));
                        ResetAllMoveAttempts();
                    }
                    else
                        rightMoveAttempts++;
                    break;
            }
        }

        yield return null;
        if (!allowBackwardMov)
        {
            StartCoroutine(MoveRandomly());
        }
        else
        {
            isMoving = false;
        }
    }

    private IEnumerator MoveRandomlyAllowBackwards()
    {
        if (!isLerping)
        {
            int a = UnityEngine.Random.Range(0, 4);
            switch (a)
            {
                case 0:
                    if (directionAvailability[WallChecker.Direction.up] && upMoveAttempts < 1)
                    {
                        StartCoroutine(Move(Vector3.up));
                        ResetAllMoveAttempts();
                    }
                    else
                        upMoveAttempts++;

                    break;
                case 1:
                    if (directionAvailability[WallChecker.Direction.left] && leftMoveAttemps < 1)
                    {
                        StartCoroutine(Move(Vector3.left));
                        ResetAllMoveAttempts();
                    }
                    else
                        leftMoveAttemps++;
                    break;
                case 2:
                    if (directionAvailability[WallChecker.Direction.down] && downMoveAttempts < 1)
                    {
                        StartCoroutine(Move(Vector3.down));
                        ResetAllMoveAttempts();
                    }
                    else
                        downMoveAttempts++;
                    break;
                case 3:
                    if (directionAvailability[WallChecker.Direction.right] && rightMoveAttempts < 1)
                    {
                        StartCoroutine(Move(Vector3.right));
                        ResetAllMoveAttempts();
                    }
                    else
                        rightMoveAttempts++;
                    break;
            }
        }

        yield return null;
        if (allowBackwardMov)
        {
            StartCoroutine(MoveRandomlyAllowBackwards());
        }
        else
        {
            isMoving = false;
        }
    }

    void ResetAllMoveAttempts()
    {
        upMoveAttempts = 0;
        leftMoveAttemps = 0;
        downMoveAttempts = 0;
        rightMoveAttempts = 0;
    }
}
