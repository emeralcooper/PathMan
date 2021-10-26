using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void LateUpdate()
    {
        if(transform.position.x < -6f)
        {
            transform.position = new Vector3(6f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 6f)
        {
            transform.position = new Vector3(-6f, transform.position.y, transform.position.z);
        }

    }
}
