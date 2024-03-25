using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaverPlacedShips", menuName ="Save/ShipsPlaced",order = 51)]
public class SavePlacedShips : ScriptableObject
{
    [SerializeField] private List<PlacedShip> _shipsList;

    public void AddPlacedShip(Ship ship, Vector2Int position, bool isHorizontal)
    {
        PlacedShip placed = new PlacedShip(ship, position, isHorizontal);
        _shipsList.Add(placed);
    }

    public void Reset() => _shipsList.Clear();

    public List<PlacedShip> GetPlacedList() => _shipsList;
}

[Serializable]
public class PlacedShip
{
    public Ship _ship { get; private set; }
    public Vector2Int _position { get; private set; }
    public bool _isHorizontalDirection { get; private set; }

    public PlacedShip(Ship ship, Vector2Int position, bool isHorizontalDirection)
    {
        _ship = ship;
        _position = position;
        _isHorizontalDirection = isHorizontalDirection;
    }
}
