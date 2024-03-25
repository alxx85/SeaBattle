using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewLine : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private Ship _template;
    [SerializeField] private int _count;

    private UIViewer _viewer;

    public Ship ShipTemplate => _template;

    private void OnDisable()
    {
        _button.onClick.RemoveListener(StartPlacingShip);
    }

    public void ShipPlaced()
    {
        _count--;
        _textCount.text = _count.ToString();
    }

    public void Init(UIViewer viewer)
    {
        _viewer = viewer;
        _button.onClick.AddListener(StartPlacingShip);
    }

    private void StartPlacingShip()
    {
        if (_count > 0)
            _viewer.StartPlacing(_template);
    }
}

