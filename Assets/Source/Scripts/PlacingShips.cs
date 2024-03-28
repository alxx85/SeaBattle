using System.Collections.Generic;
using UnityEngine;

public class PlacingShips : MonoBehaviour
{
    [SerializeField] private List<Ship> _placingShips;
    [SerializeField] private Transform _placeHolder;
    [SerializeField] private Transform _place;

    private int maxCount;
    private int current = 0;
    private List<Vector2Int> _usedSeaPositions = new List<Vector2Int>();


    private void Start()
    {
        maxCount = _placingShips.Count;
    }

    public void StartPlacing()
    {
        Vector2 position = Vector2.zero;

        if (current < maxCount)
        {
            var selectedShip = Instantiate(_placingShips[current], _placeHolder);

            do
            {
                position = new Vector2(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));

                if (UnityEngine.Random.Range(0, 2) == 1)
                    selectedShip.ChangeDirection();

                selectedShip.transform.position = position;
            }
            while (TryPlacing(position, selectedShip) == false);

            AddUsedSeaPosition(selectedShip, position);
            current++;
        }
    }

    private void AddUsedSeaPosition(Ship selectedShip, Vector2 position)
    {
        for (int x = -1; x < selectedShip.Size.x + 1; x++)
                for (int y = -1; y < selectedShip.Size.y + 1; y++)
                    AddUsedPosition(x, y, selectedShip.transform.position);
    }

    private void AddUsedPosition(int x, int y, Vector2 shipPosition)
    {
        Vector2Int position = new Vector2Int((int)shipPosition.x, (int)shipPosition.y);
        position += new Vector2Int(x, -y);

        if (_usedSeaPositions.Contains(position) == false)
        {
            _usedSeaPositions.Add(position);
            var newPosition = new Vector2(position.x, position.y);
            Instantiate(_place, newPosition, Quaternion.identity);
        }
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

        changedPosition -= ship.Size.y - 1;

        if (position.x >= 0 && position.x + ship.Size.x <= 10)
            if (changedPosition >= 0 && position.y < 10)
                return true;

        return false;
    }

}
