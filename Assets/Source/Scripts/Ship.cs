using System;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = Vector2Int.one;
    [SerializeField] private Transform _viewModel;

    private SpriteRenderer _render;
    private Color _defaultColor;

    public Vector2Int Size => _size;

    private void Awake()
    {
        _render = GetComponentInChildren<SpriteRenderer>();
        _defaultColor = _render.color;
    }

    public void ChangeDirection()
    {
        int tempX = _size.x;

        if (_viewModel.rotation.z == 0)
        {
            _viewModel.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            _viewModel.localPosition = Vector2.up;
        }
        else
        {
            _viewModel.rotation = Quaternion.identity;
            _viewModel.localPosition = Vector2.zero;
        }

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
        if (_viewModel.rotation.z == 0)
            return true;

        return false;
    }
}
