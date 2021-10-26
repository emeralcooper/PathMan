using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] protected float timeToMove;
    [SerializeField] protected WallChecker wallChecker;

    protected bool isLerping;
    protected Vector3 originalPos, targetPos;
    protected Dictionary<WallChecker.Direction, bool> directionAvailability;

    protected virtual void Start()
    {
        directionAvailability = wallChecker.getDirectionAvailability();
    }

    protected IEnumerator FreezeMovment(float time)
    {
        isLerping = true;
        yield return new WaitForSeconds(time);
        isLerping = false;
    }

    protected IEnumerator Move(Vector3 direction)
    {
        isLerping = true;

        float elapsedTime = 0f;

        originalPos = transform.position;
        targetPos = originalPos + direction*.4f;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isLerping = false;
    }
}
