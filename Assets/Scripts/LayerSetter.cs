﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSetter {
    public static void SetAllLayer(GameObject go, int layer)
    {
        foreach (Transform tran in go.GetComponentsInChildren<Transform>())
        {
            tran.gameObject.layer = layer;
        }
    }

    public static void SetLayer(GameObject go, int layer)
    {
        go.layer = layer;
    }

    public static string EnemyBaseLayerName(string layerName)
    {
        if(layerName == "A" || layerName == "AP")
        {
            return "B";
        }else if (layerName == "B" || layerName == "BP")
        {
            return "A";
        }else
        {
            return "Terrain";
        }
    }
}
