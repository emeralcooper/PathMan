using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{
    public static event Action OnPowerUpEaten;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Body>() != null)
        {
            Destroy(this.gameObject);
            OnPowerUpEaten?.Invoke();
        }
    }
}
