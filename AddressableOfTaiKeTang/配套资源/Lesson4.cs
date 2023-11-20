using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Lesson4 : MonoBehaviour
{
    [AssetReferenceUILabelRestriction("SD","HD","FHD")]
    public AssetReference assetReference;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 我们会经常使用指定资源加载的方式加载资源吗？
        //并不会经常使用，要根据实际情况

        //我们上节课学了加载指定资源
        //但是这种模式我们必须在脚本中声明各种标识类来指定加载的资源,不够灵活,做一些小项目没问题
        //但是在实际商业项目开发中，很多时候加载什么资源都是根据配置文件决定的，往往都是动态加载
        //所以我们需要学习根据名字或标签去加载对应的资源，这样我们就可以读表进行加载
        #endregion

        #region 知识点二 添加标签

        #endregion

        #region 知识点三 标签的作用
        //首先需要强调
        //我们之后学习动态加载资源时
        //是可以通过名和标签来加载资源的

        //举例说明 1
        //游戏装备中有一顶帽子：Hat
        //但是它可以有不同的品质，比如：红、绿、白、蓝
        //那么我们可以为这个帽子添加多个材质球资源（或者贴图资源）
        //这些图都可以叫做：Hat_Mat（或者Hat_Tex）
        //他们可以同名，我们可以通过标签Label来区分他们
        //他们的Label可以是：Red、Green、White、Blue

        //举例说明 2
        //游戏中我们经常根据设备好坏来选择不同质量的图片或者模型
        //比如：高清、标清、超清
        //不同标准下的资源会有所不同，比如模型面数更低、贴图质量更低
        //但是在不同标准下，这些模型的命名应该是同样的
        //比如角色1，在高清、标清、超清下它的资源名都是角色1
        //它的Label可以是：HD、SD、FHD

        //举例说明 3
        //游戏中我们经常在逢年过节时更换游戏中模型和UI的显示
        //比如：中秋节、春节、圣诞节
        //不同节日时角色或者UI等等资源看起来是不同的
        //但是在不同节日下，这些资源的命名应该都是同样的规范
        //比如登录面板，在中秋节、春节、圣诞节时它的资源名都是 登录面板
        //它的Label可以是：MidAutumn、Spring、Christmas

        //总结：相同作用的不同资源（模型、贴图、材质、UI等等）
        //我们可以让他们的资源名相同
        //通过标签Label区分他们来加载使用
        #endregion

        #region 知识点四 通过标签相关特性约束标识类对象
        //特性[AssetReferenceUILabelRestriction()]
        #endregion

        #region 知识点五 总结
        //相同作用的不同资源
        //我们可以让他们的资源名相同
        //通过标签Label区分他们来用途
        //用于之后的动态加载

        //利用名字和标签可以单独动态加载某个资源
        //也可以利用它们共同决定加载哪个资源
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
