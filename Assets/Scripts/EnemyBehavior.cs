using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBehavior : Mover
{
    [SerializeField] float franticTimer = 5f;
    [SerializeField] Color franticColor;
    [SerializeField] float delayMovementAtStart;
    [SerializeField] float delayExitingGhostHouseAtStart;
    [SerializeField] float timeInGhostHouse;
    [SerializeField] Vector3 ghostHouseExitPosition;
    [SerializeField] int enemyValue = 50;

    private float gameTimer = 0;
    private bool isInGhostHouse = true;
    private bool isEdible;
    private bool allowBackwardMov;
    private bool isMovingUp, isMovingLeft, isMovingDown, isMovingRight;
    private int upMoveAttempts, leftMoveAttemps, downMoveAttempts, rightMoveAttempts;
    private bool isMoving;
    bool gameHasStarted;

    private SpriteRenderer mySpriteRenderer;
    private Color myStartingColor;

    public static event Action OnPlayerCollided;

    public delegate void passPointsOnEnemyEaten(int value);
    public static event passPointsOnEnemyEaten OnEnemyEaten;

    IEnumerator StopFranticMode;

    protected override void Start()
    {
        directionAvailability = wallChecker.getDirectionAvailability();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myStartingColor = mySpriteRenderer.color;

        PowerUp.OnPowerUpEaten += StartFranticModeWithTimer;
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

        if(delayExitingGhostHouseAtStart > gameTimer)
        {
            return;
        }
        
        if (isInGhostHouse && gameHasStarted)
        {
            StartCoroutine(ExitGhostHouse(timeInGhostHouse));
        }
        
        if (isInGhostHouse && !gameHasStarted)
        {
            StartCoroutine(ExitGhostHouse(0));
            gameHasStarted = true;
        }
    }

    private void OnDisable()
    {
        PowerUp.OnPowerUpEaten -= StartFranticModeWithTimer;
    }

    IEnumerator ExitGhostHouse(float time)
    {
        yield return new WaitForSeconds(time);
        StopAllCoroutines();
        isInGhostHouse = false;
        isMoving = true;
        transform.position = ghostHouseExitPosition;
        StartCoroutine(Move(new Vector3(0,0,0)));
        isMoving = false;
        StartCoroutine(MoveRandomly());
    }

    IEnumerator FadeTo( float targetOpacity, float duration)
    {
        Color color = mySpriteRenderer.color;
        float startOpacity = color.a;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);
            mySpriteRenderer.color = color;
            yield return null;
        }                 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Body>() != null)
        {
            if (isEdible)
            {
                GetEatenAndResetToGhostHouse();
                OnEnemyEaten?.Invoke(enemyValue);
            }
            else
            {
                OnPlayerCollided?.Invoke();
            }
        }
    }

    private void GetEatenAndResetToGhostHouse()
    {
        StopAllCoroutines();
        isMoving = true;
        transform.position = new Vector3(0, 0, 0);
        mySpriteRenderer.color = myStartingColor;
        isMoving = false;
        isInGhostHouse = true;
        allowBackwardMov = false;
        isEdible = false;
        StartCoroutine(Move(new Vector3(0, 0, 0)));
        StartCoroutine(MoveRandomly());
    }

    void StartFranticModeWithTimer()
    {       

        if (StopFranticMode !=null)
        {
            StopCoroutine(StopFranticMode);            
        }

        if (!isInGhostHouse)
        {
            isEdible = true;
            mySpriteRenderer.color = franticColor;
            allowBackwardMov = true; 
            StopFranticMode = StopFranticModeCoroutine();
            StartCoroutine(StopFranticMode);
        }       
    }

    IEnumerator StopFranticModeCoroutine()
    {
        yield return new WaitForSeconds(franticTimer);
        isEdible = false;
        mySpriteRenderer.color = myStartingColor;
        allowBackwardMov = false;
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
                    if (directionAvailability[WallChecker.Direction.left]  && leftMoveAttemps < 1)
                    {
                        StartCoroutine(Move(Vector3.left));
                        ResetAllMoveAttempts();
                    }
                    else
                        leftMoveAttemps++;
                    break;
                case 2:
                    if (directionAvailability[WallChecker.Direction.down]&& downMoveAttempts < 1)
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
