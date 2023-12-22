using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson17 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 目录的作用
        //目录文件的本质是Json文件和一个Hash文件
        //其中记录的主要内容有
        //Json文件中记录的是：
        //1.加载AB包、图集、资源、场景、实例化对象所用的脚本（会通过反射去加载他们来使用）
        //2.AB包中所有资源类型对应的类（会通过反射去加载他们来使用）
        //3.AB包对应路径
        //4.资源的path名
        //等等
        //Hash文件中记录的是：
        //目录文件对应hash码（每一个文件都有一个唯一码，用来判断文件是否变化）
        //更新时本地的文件hash码会和远端目录的hash码进行对比
        //如果发现不一样就会更新目录文件

        //当我们使用远端发布内容时，在资源服务器也会有一个目录文件
        //Addressables会在运行时自动管理目录
        //如果远端目录发生变化了(他会通过hash文件里面存储的数据判断是否是新目录)
        //它会自动下载新版本并将其加载到内存中
        #endregion
        #region 知识点二 手动更新目录
        // 1.如果要手动更新目录 建议在设置中关闭自动更新
        // 2.自动检查所有目录是否由更新，并更新目录API
        //Addressables.UpdateCatalogs().Completed += (obj) =>
        //{
        //    Debug.Log(obj.DebugName);
        //    Addressables.Release(obj);
        //};
        // 3.获取目录列表，再更新目录
        // 参数bool就是加载结束后，不会自动释放异步加载得句柄
        //Addressables.CheckForCatalogUpdates(true).Completed += (obj) =>
        //{
        //    // 如果列表里面内容大于0，证明可以更新得目录
        //    if (obj.Result.Count > 0)
        //    {
        //        Debug.Log(obj.DebugName);
        //        // 根据别列表更新目录
        //        Addressables.UpdateCatalogs(obj.Result, true).Completed += (handle) =>
        //        {
        //            Debug.Log(obj.DebugName);
        //            // 自动释放
        //            //Addressables.Release(handle);
        //            //Addressables.Release(obj);
        //        };
        //    }
        //};
        #endregion

        #region 知识点三 预加载包
        //建议通过协程来加载
        StartCoroutine(LoadAsset());
        //1.首先获取下载包的大小

        //2.预加载

        //3.加载进度
        #endregion
    }
    IEnumerator LoadAsset()
    {
        //1.首先获取下载包的大小
        AsyncOperationHandle<long> handleSize = Addressables.GetDownloadSizeAsync(new List<string>() { "Cube", "Sphere", "SD" });
        yield return handleSize;

        Debug.Log($"handleSize.Result {handleSize.Result}");
        //2.预加载
        if (handleSize.Result > 0)
        {
            Debug.Log(handleSize.Result);
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(new List<string>() { "Cube", "Sphere", "SD" }, Addressables.MergeMode.Union);
            while (!handle.IsDone)
            {
                // 3.加载进度
                DownloadStatus info = handle.GetDownloadStatus();
                Debug.Log(info.Percent);
                Debug.Log(info.DownloadedBytes + "/" + info.TotalBytes);
                yield return 0;
            }
            Debug.Log(handle.Result.ToString());// 是个IList
            // 可恶，加载不出来
            //var res = handle.Result.Convert<IList<Object>>();
            //Instantiate(res);
            //var gh = handle.Convert<IList<GameObject>>();
            //foreach (var go in handle.Result)
            //{
            //    Instantiate(handle.Result);
            //}
            Addressables.Release(handle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
