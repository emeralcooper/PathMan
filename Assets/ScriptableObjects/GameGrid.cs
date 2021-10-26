using UnityEngine;
using System.Collections;
using UnityEditor;


[CreateAssetMenu()]
public class GameGrid : ScriptableObject
{
   
    [SerializeField] Vector3 anchorPoint;
    [SerializeField] float worldGridSize = .4f;
    
    int rows = 26;
    int columns = 26;
    public Vector3[,] gridPoints;

    private void OnEnable()
    {


        gridPoints = new Vector3[columns, rows];

        for(int i=0; i<columns; i++)
        {
            for(int j=0; j<rows; j++)
            {
                gridPoints[i, j] = new Vector3(anchorPoint.x+ i*worldGridSize,anchorPoint.y + j*worldGridSize,anchorPoint.z);
            }
        }
    }
}