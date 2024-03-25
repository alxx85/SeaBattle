using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyField : Fields
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int clickPosition = GetMouseClickPosition(Input.mousePosition, offset);

            //if (clickPosition.x >= 0 & clickPosition.x < 10)
            //    if (clickPosition.y >= 0 & clickPosition.y < 10)
                    Debug.Log($"Click point: {clickPosition}");
        }
    }

}
