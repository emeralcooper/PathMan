using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] PelletSpawner pelletSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Pellet>() != null)
        {
            pelletSpawner.totalPellets--;
            Destroy(collision.gameObject);        
    }

}
}
