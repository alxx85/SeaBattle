using System;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    private const float YSize = 0.1f;

    [SerializeField] private Vector2Int _size = Vector2Int.one;
    [SerializeField] private bool _isVerticalDirection = false;

    private Button _button;

    public Vector2Int Size => _size;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickedButtonShip);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickedButtonShip);
    }

    private void OnClickedButtonShip()
    {
        Debug.Log($"You selected {gameObject.name} ship");
    }

    public void ChangeDirection()
    {
        int tempX = _size.x;

        _isVerticalDirection = !_isVerticalDirection;
        _size.x = _size.y;
        _size.y = tempX;
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.DrawCube(transform.position, new Vector3(x, YSize, y));
            }
        }
    }
}
