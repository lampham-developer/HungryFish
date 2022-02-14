using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;

public class MapDecorate : ObjectPool<MapDecorate>
{
    int pos;
    public override void OnSpawn()
    {
        MapManager.renderingDecorate.Remove(pos);
        gameObject.SetActive(true);
    }

    public MapDecorate ReInit(int pos)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        this.pos = pos;
        MapManager.renderingDecorate.Add(pos);
        return this;
    }

}
