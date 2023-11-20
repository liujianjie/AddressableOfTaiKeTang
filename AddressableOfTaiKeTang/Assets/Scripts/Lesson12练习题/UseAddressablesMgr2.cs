using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UseAddressablesMgr2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region Lesson6 练习题
        // 批量或指定加载释放资源
        //List<string> strs = new List<string>() { "Cube", "HD" };
        //Addressables.LoadAssetsAsync<Object>(strs, (obj) =>
        //{
        //    Debug.Log("1 " + obj.name);
        //}, Addressables.MergeMode.Intersection);

        AddressablesMgr2.Instance.LoadAssetAsync<Object>(Addressables.MergeMode.Intersection, (obj) =>
        {
            Debug.Log("1" + obj.name);
        }, "Cube", "SD");
        AddressablesMgr2.Instance.LoadAssetAsync<Object>(Addressables.MergeMode.Intersection, (obj) =>
        {
            Debug.Log("2" + obj.name);
        }, "Cube", "SD");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
