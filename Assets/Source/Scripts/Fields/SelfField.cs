using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfField : Fields
{
    [SerializeField] private SavePlacedShips _playerSavedShip;

    private void Start()
    {
        LoadSelfShips();
    }

    private void LoadSelfShips()
    {
        if (_playerSavedShip != null)
        {
            var placedShips = _playerSavedShip.GetPlacedDict();

            foreach (var ship in placedShips.Keys)
            {
                Vector2 shipPosition = placedShips[ship]._position;
                Quaternion shipRotation = placedShips[ship]._isHorizontalDirection ? Quaternion.identity : Quaternion.AngleAxis(-90, Vector3.forward);

                var newShip = Instantiate(placedShips[ship]._shipTemplate, shipPosition, shipRotation, transform);
            }
        }
    }
}
