using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] int pointValue = 10;

    [SerializeField] PelletSpawner pelletSpawner;

    public delegate void passPointVallueWithOnPelletEaten (int value);
    public static event passPointVallueWithOnPelletEaten OnPelletEaten;

    private void Start()
    {
        pelletSpawner = FindObjectOfType<PelletSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Body>() != null)
        {
            pelletSpawner.totalPellets--;
            OnPelletEaten?.Invoke(pointValue);
            Destroy(this.gameObject);
        }
    }
}
