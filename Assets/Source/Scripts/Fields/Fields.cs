using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fields : MonoBehaviour
{
    [SerializeField] protected Vector2Int offset;



    protected Vector2Int GetMouseClickPosition(Vector3 mousePosition, Vector2Int offset)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
        return new Vector2Int(Mathf.RoundToInt(offset.x + position.x), Mathf.RoundToInt(offset.y - position.y));
    }

}
