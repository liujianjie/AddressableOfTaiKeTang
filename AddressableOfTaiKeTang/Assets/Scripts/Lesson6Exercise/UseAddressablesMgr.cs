using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UseAddressablesMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region Lesson5 练习题
        //尝试自己写一个Addressables资源管理器
        //帮助我们通过名字加载单个资源或场景，并管理资源相关内容

        //AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>("Cube");
        //handle.Completed += (obj) => {
        //    //加载成功后的逻辑处理

        //    Addressables.Release(obj);
        //};
        //分析：
        //1.资源动态加载可以通过返回的对象AsyncOperationHandle<>（异步操作句柄） 添加完成监听来处理加载后的资源
        //2.释放资源时也是通过释放返回的对象AsyncOperationHandle<>（异步操作句柄） 来进行释放
        //3.如果分散在各脚本中自己管理资源难免显得太过凌乱，所以我们可以通过一个资源管理器来管理所有的异步加载返回对象AsyncOperationHandle<>（异步操作句柄）

        //所以如果我们要自己写一个Addressables资源管理器，主要就是用来管理AsyncOperationHandle<>对象的

        AddressablesMgr.Instance.LoadAssetAsync<GameObject>("Cube", (obj) =>
        {
            Instantiate(obj.Result);
        });

        AddressablesMgr.Instance.LoadAssetAsync<GameObject>("Cube", (obj) =>
        {
            Instantiate(obj.Result, Vector3.right * 5, Quaternion.identity);
            //使用完资源后 移除资源
            AddressablesMgr.Instance.Release<GameObject>("Cube");
        });
        #endregion


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
