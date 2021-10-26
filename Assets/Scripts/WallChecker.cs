using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField] Collider2D wallsCollider;
    [SerializeField] BoxCollider2D upCollider, leftCollider, downCollider, rightCollider;

    public enum Direction { up, left, down, right};

    private Dictionary<Direction, bool> directionAvailability = new Dictionary<Direction, bool>()
    {
        {Direction.up, true }, {Direction.left, true }, {Direction.down, true }, {Direction.right, true }
    };

    private void Update()
    {
        updateDirectionAvailability(upCollider, Direction.up);
        updateDirectionAvailability(leftCollider, Direction.left);
        updateDirectionAvailability(downCollider, Direction.down);
        updateDirectionAvailability(rightCollider, Direction.right);
    }

    private void updateDirectionAvailability(Collider2D collider, Direction direction)
    {
        if (collider.IsTouching(wallsCollider))
        {
            directionAvailability[direction] = false;
        }
        else
        {
            directionAvailability[direction] = true;
        }
    }

    public Dictionary<Direction, bool> getDirectionAvailability()
    {
        return directionAvailability;
    }
}
