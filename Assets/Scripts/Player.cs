using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 playerStartingPos;
    [SerializeField] float invincibilityTime = 3f;
    [SerializeField] Color invinsibilityColor;

    int totalLives = 3;
    public bool isInvincible { get; private set; }
    Color myStartingColor;

    SpriteRenderer mySpriteRenderer;

    public static event Action OnLifeLost;


    void Start()
    {        
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myStartingColor = mySpriteRenderer.color;

        movePlayerToStartingPos();
    }

    public void movePlayerToStartingPos()
    {
        transform.position = playerStartingPos;
    }

    public Vector3 GetStartingPosition()
    {
        return playerStartingPos;
    }

    public void UpdateTotalLives()
    {
        totalLives--;
        OnLifeLost?.Invoke();    
    }

    public IEnumerator GoInvincible()
    {
        isInvincible = true;
        mySpriteRenderer.color = invinsibilityColor;
        yield return new WaitForSeconds(invincibilityTime);
        mySpriteRenderer.color = myStartingColor;
        isInvincible = false;
    }
}
