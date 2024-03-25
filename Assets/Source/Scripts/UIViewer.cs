using System;
using System.Collections.Generic;
using UnityEngine;

public class UIViewer : MonoBehaviour
{
    [SerializeField] private ShipsPlaceholder _placeholder;

    private Dictionary<Ship, ViewLine> _viewer = new Dictionary<Ship, ViewLine>();
    private Ship _selectedShip;

    private void OnEnable()
    {
        _placeholder.PlacedShip += OnShipPlaced;
    }

    private void OnDisable()
    {
        _placeholder.PlacedShip += OnShipPlaced;
    }

    private void Start()
    {
        var lines = GetComponentsInChildren<ViewLine>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            _viewer.Add(lines[i].ShipTemplate, lines[i]);
            lines[i].Init(this);
        }
    }

    public void StartPlacing(Ship ship)
    {
        _placeholder.SelectedShip(ship);
        _selectedShip = ship;
    }

    private void OnShipPlaced()
    {
        _viewer[_selectedShip].ShipPlaced();
    }
}