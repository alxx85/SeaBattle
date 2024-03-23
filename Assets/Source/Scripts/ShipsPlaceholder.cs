using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsPlaceholder : MonoBehaviour
{
    [SerializeField] private Ship _template;
    [SerializeField] private GameObject _ship_3;

    [SerializeField] private List<Vector2Int> _usedSeaPositions;
    private Ship _selectedShip;
    private bool _shipIsHorizontalDirection;
    private bool _canPlace;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _ship_3.transform.position = new Vector3(9, 9, 0);

        if (_selectedShip != null)
        {
            PlacingView();

            if (Input.GetMouseButtonDown(1))
                _selectedShip.ChangeDirection();

            if (Input.GetMouseButtonDown(0) && _canPlace)
            {
                _selectedShip.ChangeColor(Color.white);
                AddUsedSeaPosition(_selectedShip);
                _selectedShip = null;
            }
        }
    }

    private void AddUsedSeaPosition(Ship selectedShip)
    {
        for (int x = -1; x < selectedShip.Size.x + 1; x++)
        {
            if (selectedShip.IsHorizontalDirection())
            {
                for (int y = -1; y < selectedShip.Size.y + 1; y++)
                    AddUsedPosition(selectedShip, x, y);
            }
            else 
            {
                for (int y = 0; y < selectedShip.Size.y + 2; y++)
                    AddUsedPosition(selectedShip, x, y);
            }
            
        }
    }

    private void AddUsedPosition(Ship selectedShip, int x, int y)
    {
        Vector2Int position = new Vector2Int((int)selectedShip.transform.position.x, (int)selectedShip.transform.position.y);
        position += new Vector2Int(x, -y);

        if (_usedSeaPositions.Contains(position) == false)
            _usedSeaPositions.Add(position);
    }

    private void PlacingView()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = new Vector2((int)position.x, (int)position.y);

        if (_selectedShip.IsHorizontalDirection())
            _selectedShip.transform.position = currentPosition;
        else
            _selectedShip.transform.position = currentPosition + Vector2.up;

        _canPlace = TryPlacing(currentPosition, _selectedShip);
        _selectedShip.ChangeColor(_canPlace ? Color.green : Color.red);
    }

    public void SelectedShip()
    {
        var ship = Instantiate(_template.gameObject);
        _selectedShip = ship.GetComponent<Ship>();
    }

    private bool TryPlacing(Vector2 position, Ship ship)
    {
        var changedPosition = position.y;

        for (int i = 0; i < ship.Size.x; i++)
        {
            for (int j = 0; j < ship.Size.y; j++)
            {
                Vector2Int currentPosition = new Vector2Int((int)position.x + i, (int)position.y - j);

                if (_usedSeaPositions.Contains(currentPosition))
                    return false;
            }
        }

        if (ship.IsHorizontalDirection() == false)
        {
            changedPosition -= ship.Size.y - 1;
        }

        if (position.x >= 0 && position.x + ship.Size.x <= 10)
            if (changedPosition >= 0 && position.y < 10)
                return true;

        return false;
    }
}
