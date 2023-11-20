using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson5 : MonoBehaviour
{
    AsyncOperationHandle<GameObject> handle;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 通过资源名或标签名动态加载单个资源
        //命名空间：
        //UnityEngine.AddressableAssets 和 UnityEngine.ResourceManagement.AsyncOperations
        handle = Addressables.LoadAssetAsync<GameObject>("Cube");
        handle.Completed += (handle) =>
        {
            //判断加载成功
            if (handle.Status == AsyncOperationStatus.Succeeded)
                Instantiate(handle.Result);

            //一定要是 加载完成后 使用完毕后 再去释放
            //不管任何资源 只要释放后 都会影响之前在使用该资源的对象
            Addressables.Release(handle);
        };

        //Addressables.LoadAssetAsync<GameObject>("Red").Completed += (handle) =>
        //{
        //    //判断加载成功
        //    if (handle.Status == AsyncOperationStatus.Succeeded)
        //        Instantiate(handle.Result);
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
        //Addressables.LoadSceneAsync("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single, false).Completed += (obj)=> {
        //    //比如说 手动激活场景
        //    obj.Result.ActivateAsync().completed += (a) =>
        //    {
        //        //然后再去创建场景上的对象

        //        //然后再去隐藏 加载界面

        //        //注意：场景资源也是可以释放的，并不会影响当前已经加载出来的场景，因为场景的本质只是配置文件
        //        Addressables.Release(obj);
        //    };
        //};


        #endregion

        #region 知识点四 总结
        //1.根据名字或标签加载单个资源相对之前的指定加载资源更加灵活
        //  主要通过Addressables类中的静态方法传入资源名或标签名进行加载
        //  注意：
        //  1-1.如果存在同名或同标签的同类型资源，我们无法确定加载的哪一个，它会自动加载找到的第一个满足条件的对象
        //  1-2.如果存在同名或同标签的不同类型资源，我们可以根据泛型类型来决定加载哪一个

        //2.释放资源时需要传入之前记录的AsyncOperationHandle对象
        //  注意：一定要保证资源使用完毕过后再释放资源

        //3.场景异步加载可以自己手动激活加载完成的场景
        #endregion
    }

    private void OnDestroy()
    {
        //Addressables.Release(handle);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
