using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using System;

/// <summary>
/// Không nên đọc hoặc sửa code trong này, hơi nát. Cần sửa gì bảo tôi.
/// </summary>
public class MapManager : SingletonBehaviour<MapManager>
{
    [SerializeField]
    MapTile mapTileTemplate;
    [SerializeField]
    MapDecorate mapDecorateTemplate;
    [SerializeField]
    List<TileData> tileDatas;
    [SerializeField]
    float tileDistance = 1, decorateDistance = 1;
    [SerializeField]
    Transform background;
    [SerializeField]
    float backgroundSpeed = 0.5f;
    [SerializeField]
    List<GameObject> mapDecorate;

    public int renderDistance = 5, decorateRenderDistance = 5;
    public int mapSeed = 100;

    int renderingPos = 0, lastRenderPos = 0;
    int decoratingPos = 0, lastDecoratePos = 0;

    public static List<int> renderingTile;
    public static List<int> renderingDecorate;
    bool inited = false;

    CameraManager cameraManager;
    public void OnLevelInit()
    {

        inited = true;

    }

    private void Start()
    {
        cameraManager = CameraManager.Instance;
        MapTile.Enqueue(renderDistance * 4, mapTileTemplate); // tạo map tile trong pool
        renderingTile = new List<int>();
        MapDecorate.Enqueue(decorateRenderDistance * 4, mapDecorateTemplate);
        renderingDecorate = new List<int>();
        InitFirstView();
    }


    private void Update()
    {
        if (inited && RenderCondition())
        {
            ReRender();
        }
        if (inited && RenderDecorateCondition())
        {
            ReDecorate();
        }
        BackroundUpdate();
    }

    void InitFirstView()
    {
        for (int i = -renderDistance; i < renderDistance; i++)
        {
            var tile = MapTile.Spawn(transform).SetData(GetTileByValue(i), i);
            if (tile != null)
            {
                tile.transform.localPosition = new Vector2(tileDistance * i, 0);
            }
        }
        for (int i = -decorateRenderDistance; i < decorateRenderDistance; i++)
        {

            GameObject dObj = GetDecorateByValue(i);
            if (dObj != null)
            {
                MapDecorate decorate = MapDecorate.Spawn(transform);
                decorate.ReInit(i);
                Instantiate(dObj, decorate.transform);
                decorate.transform.localPosition = new Vector2(decorateDistance * i, 0);

            }
        }
    }

    void BackroundUpdate()
    {
        // cái này hardcode nhé, đau lưng quá. Dù sao nếu thay bg khác thì cũng phải sửa lại nhiều, nên là hardcode luôn
        background.transform.localPosition = new Vector2(cameraManager.mainCam.transform.position.x - (cameraManager.mainCam.transform.position.x * backgroundSpeed % 9.6f), 0); //  20 là chỉnh offset
    }

    void ReDecorate()
    {
        int mulPrefix = decoratingPos > lastDecoratePos ? 1 : -1;

        int i = lastDecoratePos + decorateRenderDistance * mulPrefix;
        int des = decoratingPos + decorateRenderDistance * mulPrefix;
        while (i != des)
        {
            if (!renderingDecorate.Contains(i))
            {
                GameObject dObj = GetDecorateByValue(i);
                if (dObj != null)
                {
                    MapDecorate decorate = MapDecorate.Spawn(transform);
                    decorate.ReInit(i);
                    Instantiate(dObj, decorate.transform);
                    decorate.transform.localPosition = new Vector2(decorateDistance * i, 0);

                }
            }
            i += 1 * mulPrefix;
            if (Math.Abs(i - des) > 20)
            {
                break;

            }
        }
        lastDecoratePos = decoratingPos;
    }

    void ReRender()
    {
        int mulPrefix = renderingPos > lastRenderPos ? 1 : -1;

        int i = lastRenderPos + renderDistance * mulPrefix;
        int des = renderingPos + renderDistance * mulPrefix;
        while (i != des)
        {
            if (!renderingTile.Contains(i))
            {
                var tile = MapTile.Spawn(transform).SetData(GetTileByValue(i), i);
                if (tile != null)
                {
                    tile.transform.localPosition = new Vector2(tileDistance * i, 0);
                }

            }
            //int b = 1 * i;

            i += 1 * mulPrefix;
            if (Math.Abs(i - des) > 20)
            {
                break;

            }
        }
        lastRenderPos = renderingPos;
    }

    TileData GetTileByValue(int value)
    {
        value += mapSeed;
        int tileIndex = Mathf.RoundToInt(GetNoise(value) / (1f / tileDatas.Count));
        return tileDatas[tileIndex];
    }
    GameObject GetDecorateByValue(int value)
    {
        value += mapSeed;
        int decorateIndex = Mathf.RoundToInt(GetNoise(value) / (1f / mapDecorate.Count));
        Debug.Log(GetNoise(value));
        if (decorateIndex >= mapDecorate.Count)
            decorateIndex = 0;
        return mapDecorate[decorateIndex];
    }

    [Serializable]
    public struct TileData
    {
        public Sprite tileSprite;
        public float tileHeight;
        public float tileWidth;
        //public bool autoTiling;
    }

    float GetNoise(int value)
    {
        //todo
        // đặt noise khác vào nếu muốn thay đổi. Tạm dùng noise này thay cho perlin vì lý do hiệu suất.
        // return phải nằm trong khoảng 0 - 1;

        return (Mathf.Sin(2 * value) + Mathf.Sin(Mathf.PI * value) + 2) / 4;
    }

    bool RenderCondition()
    {
        //todo
        // đoạn này sẽ dùng để xác định xem điều kiện render của các tile ra sao.
        // Bên dưới tạm để mặc định là vị trí cam làm tròn, nếu ko thích thì về sau sửa ở đây, ko sửa ở trên

        int camPos = Mathf.RoundToInt(cameraManager.mainCam.transform.position.x / tileDistance);
        //Debug.Log(camPos +" -- "+ renderingPos);
        if (camPos != renderingPos)
        {
            renderingPos = camPos;
            return true;
        }
        return false;
    }

    bool RenderDecorateCondition()
    {
        //todo
        // đoạn này sẽ dùng để xác định điều kiện render của mấy cái trang trí
        // Nói chung là ko có quá nhiều ảnh hưởng

        int camPos = Mathf.RoundToInt(cameraManager.mainCam.transform.position.x / decorateDistance);
        //Debug.Log(camPos +" -- "+ renderingPos);
        if (camPos != decoratingPos)
        {
            decoratingPos = camPos;
            return true;
        }
        return false;
    }

}
