using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson6加载多个对象 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 根据资源名或标签名加载多个对象
        //加载资源
        //参数一：资源名或标签名
        //参数二：加载结束后的回调函数
        //参数三：如果为true表示当资源加载失败时，会自动将已加载的资源和依赖都释放掉；如果为false，需要自己手动来管理释放
        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>("Cube", (obj) =>
        {
            Debug.Log(obj.name);
        }, true);
        //如果要进行资源释放管理 那么我们需要使用这种方式 要方便一些
        //因为我们得到了返回值对象 就可以释放资源了
        handle.Completed += (obj) =>
        {
            foreach (var item in obj.Result)
            {
                Debug.Log(item.name);
            }
            //释放资源
            Addressables.Release(handle);
        };
        //注意：我们还是可以通过泛型类型，来筛选资源类型
        #endregion
        #region 知识点二 根据多种信息加载对象
        //参数一：想要加载资源的条件列表（资源名、Lable名）
        //参数二：每个加载资源结束后会调用的函数，会把加载到的资源传入该函数中
        //参数三：可寻址的合并模式，用于合并请求结果的选项。
        //如果键（Cube，Red）映射到结果（[1,2,3]，[1,3,4]），数字代表不同的资源
        //None：不发生合并，将使用第一组结果 结果为[1, 2, 3]
        //UseFirst：应用第一组结果 结果为[1, 2, 3]
        //Union：合并所有结果 结果为[1, 2, 3, 4]
        //Intersection：使用相交结果 结果为[1, 3]
        //参数四：如果为true表示当资源加载失败时，会自动将已加载的资源和依赖都释放掉
        //      如果为false，需要自己手动来管理释放
        List<string> strs = new List<string>() { "Cube", "Red" };
        Addressables.LoadAssetsAsync<Object>(strs, (obj) =>
        {
            Debug.Log(obj.name);
        }, Addressables.MergeMode.Intersection);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
