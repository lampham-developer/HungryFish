using System.Collections;
using UnityEngine;
using Commons;

public class Level : SingletonBehaviour<Level>
{
    [SerializeField]
    Transform InstancePool;
    private void Start()
    {
        Init();

    }



    void Init()
    {
        ObjectPool.InstancePool = InstancePool;

        //todo
        // đặt đoạn init main character vào đây


        MapManager.Instance.OnLevelInit();
    }
}
