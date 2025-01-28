using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MovableObjectScript initialises a few values and does some simple checks. This would've been useful if we did
/// more with the game but time ran out and so this is just left as is.
/// </summary>
public class MovableObjectScript : MonoBehaviour
{
    public bool IsSymmetrical;
    public float OffsetX, OffsetY;
    private BoxCollider2D _boxCollider2D;

    /// <summary>
    /// Initialising some data in Start and checking if the movable object's collider is symmetrical.
    /// </summary>
    void Start()
    {
        try
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }
        catch
        {
            Debug.Log("MovableObjectScript failed to find a BoxCollider2D component.");
        }
        if (_boxCollider2D.size.x == _boxCollider2D.size.y)
        {
            IsSymmetrical = true;
            OffsetX = _boxCollider2D.size.x;
            OffsetY = OffsetX;
        }
        else
        {
            IsSymmetrical = false;
            OffsetX = _boxCollider2D.size.x;
            OffsetY = _boxCollider2D.size.y;
        }
    }
}
