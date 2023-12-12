using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson15 : MonoBehaviour
{
    AsyncOperationHandle<GameObject> handle;
    // Start is called before the first frame update
    void Start()
    {
        // 知识点一: 回顾目前动态异步加载的使用方式
        var handle = Addressables.LoadAssetAsync<GameObject>("Cube");
        handle.Completed += (obj) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("事件创建对象");
                Instantiate(obj.Result);
            }
        };
        // 知识点二：3种使用异步加载资源的方式
        // 2.1事件监听
        // 2.2协同程序
        // 2.3异步函数

        // 知识点三：协程使用异步加载
        StartCoroutine(LoadAsset());

        // 知识点四：异步函数
        // webgl平台不支持 async 和 await加载
        Load();
    }
    IEnumerator LoadAsset()
    {
        handle = Addressables.LoadAssetAsync<GameObject>("Cube");
        if (!handle.IsDone)
        {
            yield return handle;
        }
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("协同程序创建对象");
            Instantiate(handle.Result);
        }
        else
        {
            Addressables.Release(handle);
        }
    }
    async void Load()
    {
        handle = Addressables.LoadAssetAsync<GameObject>("Cube");
        AsyncOperationHandle<GameObject> handle2 = Addressables.LoadAssetAsync<GameObject>("Sphere");

        //await handle.Task;
        //await handle2.Task;
        await Task.WhenAll(handle.Task, handle2.Task);

        Debug.Log("异步函数的形式加载的资源");
        Instantiate(handle.Result);
        Instantiate(handle2.Result);
    }
}
