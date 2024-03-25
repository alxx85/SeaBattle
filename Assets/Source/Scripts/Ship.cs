using System;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = Vector2Int.one;

    private SpriteRenderer _render;
    private Color _defaultColor;

    public Vector2Int Size => _size;

    private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
        _defaultColor = _render.color;
    }

    public void ChangeDirection()
    {
        int tempX = _size.x;

        if (transform.rotation.z == 0)
            transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        else
            transform.rotation = Quaternion.identity;

        _size.x = _size.y;
        _size.y = tempX;
    }

    public void ChangeColor(Color color)
    {
        if (color == Color.white)
            _render.color = _defaultColor;

        _render.color = color;
    }

    public bool IsHorizontalDirection()
    {
        if (transform.rotation.z == 0)
            return true;

        return false;
    }
}
