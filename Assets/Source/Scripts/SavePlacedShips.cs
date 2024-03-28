using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaverPlacedShips", menuName ="Save/ShipsPlaced",order = 51)]
public class SavePlacedShips : ScriptableObject
{
    //[SerializeField] private List<PlacedShip> _shipsList;

    private Dictionary<Ship, PlacedShip> _shipsDict = new Dictionary<Ship, PlacedShip>();

    public void AddPlacedShip(Ship ship, Vector2Int position, bool isHorizontal)
    {
        PlacedShip placed = new PlacedShip(/*ship.Template, */position, isHorizontal);
        _shipsDict.Add(ship, placed);
        //_shipsList.Add(placed);
    }

    public void Reset() => _shipsDict.Clear();

    //public List<PlacedShip> GetPlacedList() => _shipsList;
    public Dictionary<Ship, PlacedShip> GetPlacedDict() => _shipsDict;
}

[Serializable]
public class PlacedShip
{
    //public GameObject _shipTemplate { get; private set; }
    public Vector2Int _position { get; private set; }
    public bool _isHorizontalDirection { get; private set; }

    public PlacedShip(/*GameObject ship, */Vector2Int position, bool isHorizontalDirection)
    {
        //_shipTemplate = ship;
        _position = position;
        _isHorizontalDirection = isHorizontalDirection;
    }
}
