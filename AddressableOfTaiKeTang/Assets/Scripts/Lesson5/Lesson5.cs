using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 通过资源名或标签名动态加载单个资源
        //AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>("Cube");
        //handle.Completed += (handle) =>
        //{
        //    if (handle.Status == AsyncOperationStatus.Succeeded) {
        //        Instantiate(handle.Result);
        //    }
        //    // 释放资源
        //    /*
        //     1.Use Asset Database模式下：加载的资源会被缓存，不会被释放
        //     2.simulate groups: 加载的资源会被缓存，不会被释放
        //     3.Use Existing Build:加载的资源不会被缓存，会被释放
        //     */
        //    Addressables.Release(handle);
        //};
        //注意：
        //1.如果存在同名或同标签的同类型资源，我们无法确定加载的哪一个，它会自动加载找到的第一个满足条件的对象
        //2.如果存在同名或同标签的不同类型资源，我们可以根据泛型类型来决定加载哪一个
        #endregion

        #region 知识点二 释放资源
        //需要指定要释放哪一个返回值
        //写在这是否合理？
        //不合理
        //Addressables.Release(handle);
        #endregion

        #region 知识点三 动态加载场景
        //参数一：场景名
        //参数二：加载模式 （叠加还是单独,叠加就是两个场景一起显示,单独就是只保留新加载的场景，正常情况为单独）
        //参数三：场景加载是否激活，如果为false，加载完成后不会直接切换，需要自己使用返回值中的ActivateAsync方法
        //参数四：场景加载的异步操作优先级
        //Addressables.LoadSceneAsync("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single, true, 100);
        Addressables.LoadSceneAsync("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single, false).Completed +=(obj)=>{
            // 手动激活场景
            obj.Result.ActivateAsync().completed += (a) =>
            {
                // 会卸载场景中的物体
                Addressables.Release(obj);
            };
        };
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
