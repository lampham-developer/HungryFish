using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AccessoryContainer : MonoBehaviour
{

    private void OnEnable()
    {
        foreach(var kv in Main.wearingList)
        {
            var acc = Main.Instance.accessoryList.Find(x => x.name == kv.Value);
            Instantiate(acc.prefab, transform);

        };
    }

}
