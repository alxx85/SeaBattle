using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipsPlaceholder : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> _usedSeaPositions;
    [SerializeField] private SavePlacedShips _savePlayerPlaced;

    private Ship _selectedShip;
    private bool _canPlace;

    public event Action PlacedShip;

    private void Start()
    {
        _savePlayerPlaced.Reset();
    }

    private void Update()
    {
        if (_selectedShip != null)
        {
            PlacingView();

            if (Input.GetMouseButtonDown(1))
                _selectedShip.ChangeDirection();

            if (Input.GetMouseButtonDown(0) && _canPlace)
            {
                _selectedShip.ChangeColor(Color.white);
                AddUsedSeaPosition(_selectedShip);
                PlacedShip.Invoke();
                Vector2Int ShipPosition = new Vector2Int((int)_selectedShip.transform.position.x, (int)_selectedShip.transform.position.y);
                _savePlayerPlaced.AddPlacedShip(_selectedShip, ShipPosition, _selectedShip.IsHorizontalDirection());
                _selectedShip = null;
            }
        }
    }

    public void SelectedShip(Ship _template)
    {
        if (_selectedShip != null)
            Destroy(_selectedShip.gameObject);

        var ship = Instantiate(_template.gameObject);
        _selectedShip = ship.GetComponent<Ship>();
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
