using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson3 : MonoBehaviour
{
    public AssetReference assetReference;
    public AssetReferenceAtlasedSprite asReference;
    public AssetReferenceGameObject gameobjcetReference;
    public AssetReferenceSprite spriteReference;
    public AssetReferenceTexture textureReference;

    public AssetReferenceT<AudioClip> audioReference;
    public AssetReferenceT<RuntimeAnimatorController> controller;
    public AssetReferenceT<TextAsset> textReference;

    public AssetReferenceT<Material> materialRed;

    public AssetReference sceneReference;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 资源准备
        //我们准备一些常用的各类型的资源
        //1.GameObject预设体
        //2.精灵图片
        //3.图集
        //4.贴图
        //5.材质球
        //6.配置文件（json、xml、txt、2进制）
        //7.Lua脚本
        //8.音效
        //9.Animator Controller 动画状态机控制文件
        //10.场景
        #endregion

        #region 知识点二 Addressables中的资源标识类
        //命名空间：using UnityEngine.AddressableAssets;

        //AssetReference                通用资源标识类 可以用来加载任意类型资源
        //AssetReferenceAtlasedSprite   图集资源标识类
        //AssetReferenceGameObject      游戏对象资源标识类
        //AssetReferenceSprite          精灵图片资源标识类
        //AssetReferenceTexture         贴图资源标识类
        //AssetReferenceTexture2D
        //AssetReferenceTexture3D
        //AssetReferenceT<>             指定类型标识类

        //通过不同类型标识类对象的申明 我们可以在Inspector窗口中筛选关联的资源对象
        #endregion

        #region 知识点三 加载资源
        //注意：所有Addressables加载相关都使用异步加载
        //需要引用命名空间：using UnityEngine.ResourceManagement.AsyncOperations;
        //AsyncOperationHandle<GameObject> handle = assetReference.LoadAssetAsync<GameObject>();
        //加载成功后使用
        //1.通过事件函数传入的参数判断加载是否成功 并且创建
        //2.通过资源标识类对象判断 并且创建

        //通过异步加载返回值 对完成进行事件监听
        //handle.Completed += TestFun;

        assetReference.LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            //使用传入的参数（建议）
            //判断是否加载成功
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject cube = Instantiate(handle.Result);
                //一定资源加载过后 使用完后 再去释放
                assetReference.ReleaseAsset();

                materialRed.LoadAssetAsync().Completed += (obj) =>
                {
                    cube.GetComponent<MeshRenderer>().material = obj.Result;
                    //这样会造成使用这个资源的对象 资源丢失
                    materialRed.ReleaseAsset();

                    //这个异步加载传入对象的资源
                    print(obj.Result);
                    //这个是 资源标识类的资源
                    print(materialRed.Asset);
                };
            }
           
            //使用标识类创建
            //if(assetReference.IsDone)
            //{
            //    Instantiate(assetReference.Asset);
            //}
        };

        audioReference.LoadAssetAsync().Completed += (handle) =>
        {
            //使用音效
        };

        #endregion

        #region 知识点四 加载场景
        //sceneReference.LoadSceneAsync().Completed += (handle) =>
        //{
        //    //初始化场景的一些信息
        //    print("场景加载结束");
        //};
        #endregion

        #region 知识点五 释放资源
        //释放资源相关API
        //ReleaseAsset
        //写在这不合理
        //assetReference.ReleaseAsset();
        //1.释放资源方法后,资源标识类中的资源会置空，但是AsyncOperationHandle类中的对象不为空
        //2.释放资源不会影响场景中被实例化出来的对象，但是会影响使用的资源
        #endregion

        #region 知识点六 直接实例化对象
        //只适用于 想要实例化的 对象 才会直接使用该方法 一般都是GameObject预设体
        gameobjcetReference.InstantiateAsync();
        #endregion

        #region 知识点七 自定义标识类
        //自定义类 继承AssetReferenceT<Material>类 即可自定义一个指定类型的标识类
        //该功能主要用于Unity2020.1之前，因为之前的版本不能直接使用AssetReferenceT泛型字段
        #endregion

        #region 知识点八 总结
        //1.我们可以根据自己的需求选择合适的标识类进行资源加载
        //2.资源加载和场景加载都是通过异步进行加载
        //3.需要注意异步加载资源使用时必须保证资源已经被加载成功了，否则会报错
        #endregion
    }

    private void TestFun(AsyncOperationHandle<GameObject> handle)
    {
        //加载成功后 使用加载的资源嘛
        //判断是否加载成功
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
