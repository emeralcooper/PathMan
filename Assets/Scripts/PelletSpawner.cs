using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PelletSpawner : MonoBehaviour
{
    [SerializeField] Pellet pellet;
    [SerializeField] GameGrid gameGrid;
    [SerializeField] SceneController sceneController;
    [SerializeField] Vector3[] spawnPointsToOmit;



    public int totalPellets { get; set; }
    void Start()
    {
        SpawnPellets();
    }

    private void Update()
    {
        CheckIfLastPelletEatenAndHandle();
    }

    private void LateUpdate()
    {

    }

    private void CheckIfLastPelletEatenAndHandle()
    {
        if (totalPellets <= 0)
        {
            StartCoroutine(sceneController.PauseGameAndLoadSeneWithDelay(SceneManager.GetActiveScene().buildIndex +1));
        }
    }

    public void SpawnPellets()
    {
        int arrayWidth = gameGrid.gridPoints.GetLength(0);
        int arrayHeight = gameGrid.gridPoints.GetLength(1);

        for(int i=0; i< arrayWidth; i++)
        {
            for(int j=0; j<arrayHeight; j++)
            {
                Pellet newPellet = Instantiate(pellet, gameGrid.gridPoints[i, j], Quaternion.identity);
                totalPellets++;
                for(int k=0; k<spawnPointsToOmit.Length; k++)
                {
                    if(Vector3.Distance(newPellet.transform.position,spawnPointsToOmit[k]) < .01f)
                    {
                        totalPellets--;
                        Destroy(newPellet.gameObject);
                    }
                }
            }
        }

    }

}
