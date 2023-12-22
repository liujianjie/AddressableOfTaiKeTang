using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesMgr3 : MonoBehaviour
{
    private static AddressablesMgr3 instance = new AddressablesMgr3();
    public static AddressablesMgr3 Instance => instance;

    // 由于AsyncOperationHandle继承IEnumerator，所以可以用父类表示子类，这样就不需要声明时指定泛型（里式变换）
    public Dictionary<string, AsyncOperationHandle> resDic = new Dictionary<string, AsyncOperationHandle>();

    private AddressablesMgr3()
    {

    }

    public void LoadAssetAsync<T>(string name, Action<AsyncOperationHandle<T>> callBack)
    {
        string keyName = name + "_" + typeof(T).Name;
        AsyncOperationHandle<T> handle;
        if (resDic.ContainsKey(keyName))
        {
            handle = resDic[keyName].Convert<T>();
            if (handle.IsDone)
            {
                callBack(handle);
            }
            else
            {
                handle.Completed += (obj) =>
                {
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                    {
                        callBack(obj);
                    }
                };
            }
            return;
        }
        handle = Addressables.LoadAssetAsync<T>(name);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                callBack(obj);
            }
            else
            {
                Debug.LogError(keyName + "资源加载失败");
                if (resDic.ContainsKey(keyName))
                {
                    resDic.Remove(keyName);
                }
            }
        };
        resDic.Add(keyName, handle);
    }
    // 卸载单个资源
    public void Release<T>(string name)
    {
        string keyName = name + "_" + typeof(T).Name;
        if (resDic.ContainsKey(keyName))
        {
            AsyncOperationHandle<T> handle = resDic[keyName].Convert<T>();
            Addressables.Release(handle);
            resDic.Remove(keyName);
        }
    
    }
    public void LoadAssetAsync<T>(Addressables.MergeMode mode, Action<T> callBack, params string[] keys)
    {
        List<string> list = new List<string>(keys);
        string keyName = "";
        foreach(var key  in list)
        {
            keyName += key + "_";
        }
        keyName += typeof(T).Name;
        AsyncOperationHandle<IList<T>> handle;
        if (resDic.ContainsKey(keyName))
        {
            handle = resDic[keyName].Convert<IList<T>>();
            // 异步加载是否结束
            if (handle.IsDone)
            {
                foreach (T item in handle.Result)
                {
                    callBack(item);
                }
            }
            else
            {
                // 当前帧异步未结束，只能靠Completed回传
                handle.Completed += (obj) =>
                {
                    // 加载成功才执行回调,
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                    {
                        foreach(T item in handle.Result)
                        {
                            callBack(item);
                        }
                    }
                };
            }
            return;
        }
        // 不存在
        handle = Addressables.LoadAssetsAsync<T>(list, callBack, mode);
        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("资源加载失败 " +keyName);
                if (resDic.ContainsKey(keyName))
                {
                    resDic.Remove(keyName);
                }
            }
        };
        resDic.Add(keyName, handle);
    }
    // 卸载多个资源
    public void Releases<T>(params string[] keys)
    {
        List<string> list = new List<string>(keys);
        string keyName = "";
        foreach (string key in list)
        {
            keyName += key + "_";
        }
        if (resDic.ContainsKey(keyName))
        {
            // 取出字典里面的对象
            AsyncOperationHandle<IList<T>> handle = resDic[keyName].Convert<IList<T>>();
            Addressables.Release(handle);
            resDic.Remove(keyName);
        }
    }

    public void Clear()
    {
        foreach (var item in resDic.Values)
        {
            Addressables.Release(item);
        }
        resDic.Clear();
        AssetBundle.UnloadAllAssetBundles(true);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
