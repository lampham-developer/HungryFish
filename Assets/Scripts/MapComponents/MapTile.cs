using System.Collections;
using UnityEngine;
using Commons;
using static MapManager;

public class MapTile : ObjectPool<MapTile>
{
    [SerializeField]
    BoxCollider2D boxCollider; // dung object pool nen gan luon o day chu ko gan trong start nua de toi uu
    [SerializeField]
    SpriteRenderer spriteRenderer;
    int pos;
    public override void OnSpawn()
    {
        MapManager.renderingTile.Remove(this.pos);
        gameObject.SetActive(true);
        //throw new System.NotImplementedException();
    }

    // Use this for initialization
    public MapTile SetData(TileData data, int pos)
    {
        if(this.pos == pos)
        {
            return null;
        }
        boxCollider.size = new Vector2(boxCollider.size.x, data.tileHeight);
        boxCollider.offset = new Vector2(0, data.tileHeight / 2);
        //boxCollider.autoTiling = data.autoTiling; // not working :(
        spriteRenderer.sprite = data.tileSprite;
        this.pos = pos;
        MapManager.renderingTile.Add(pos);
        return this;
    }
}
