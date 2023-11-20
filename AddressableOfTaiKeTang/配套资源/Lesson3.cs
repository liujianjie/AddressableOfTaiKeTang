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
        #region ֪ʶ��һ ��Դ׼��
        //����׼��һЩ���õĸ����͵���Դ
        //1.GameObjectԤ����
        //2.����ͼƬ
        //3.ͼ��
        //4.��ͼ
        //5.������
        //6.�����ļ���json��xml��txt��2���ƣ�
        //7.Lua�ű�
        //8.��Ч
        //9.Animator Controller ����״̬�������ļ�
        //10.����
        #endregion

        #region ֪ʶ��� Addressables�е���Դ��ʶ��
        //�����ռ䣺using UnityEngine.AddressableAssets;

        //AssetReference                ͨ����Դ��ʶ�� ����������������������Դ
        //AssetReferenceAtlasedSprite   ͼ����Դ��ʶ��
        //AssetReferenceGameObject      ��Ϸ������Դ��ʶ��
        //AssetReferenceSprite          ����ͼƬ��Դ��ʶ��
        //AssetReferenceTexture         ��ͼ��Դ��ʶ��
        //AssetReferenceTexture2D
        //AssetReferenceTexture3D
        //AssetReferenceT<>             ָ�����ͱ�ʶ��

        //ͨ����ͬ���ͱ�ʶ���������� ���ǿ�����Inspector������ɸѡ��������Դ����
        #endregion

        #region ֪ʶ���� ������Դ
        //ע�⣺����Addressables������ض�ʹ���첽����
        //��Ҫ���������ռ䣺using UnityEngine.ResourceManagement.AsyncOperations;
        //AsyncOperationHandle<GameObject> handle = assetReference.LoadAssetAsync<GameObject>();
        //���سɹ���ʹ��
        //1.ͨ���¼���������Ĳ����жϼ����Ƿ�ɹ� ���Ҵ���
        //2.ͨ����Դ��ʶ������ж� ���Ҵ���

        //ͨ���첽���ط���ֵ ����ɽ����¼�����
        //handle.Completed += TestFun;

        assetReference.LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            //ʹ�ô���Ĳ��������飩
            //�ж��Ƿ���سɹ�
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject cube = Instantiate(handle.Result);
                //һ����Դ���ع��� ʹ����� ��ȥ�ͷ�
                assetReference.ReleaseAsset();

                materialRed.LoadAssetAsync().Completed += (obj) =>
                {
                    cube.GetComponent<MeshRenderer>().material = obj.Result;
                    //���������ʹ�������Դ�Ķ��� ��Դ��ʧ
                    materialRed.ReleaseAsset();

                    //����첽���ش���������Դ
                    print(obj.Result);
                    //����� ��Դ��ʶ�����Դ
                    print(materialRed.Asset);
                };
            }
           
            //ʹ�ñ�ʶ�ഴ��
            //if(assetReference.IsDone)
            //{
            //    Instantiate(assetReference.Asset);
            //}
        };

        audioReference.LoadAssetAsync().Completed += (handle) =>
        {
            //ʹ����Ч
        };

        #endregion

        #region ֪ʶ���� ���س���
        //sceneReference.LoadSceneAsync().Completed += (handle) =>
        //{
        //    //��ʼ��������һЩ��Ϣ
        //    print("�������ؽ���");
        //};
        #endregion

        #region ֪ʶ���� �ͷ���Դ
        //�ͷ���Դ���API
        //ReleaseAsset
        //д���ⲻ����
        //assetReference.ReleaseAsset();
        //1.�ͷ���Դ������,��Դ��ʶ���е���Դ���ÿգ�����AsyncOperationHandle���еĶ���Ϊ��
        //2.�ͷ���Դ����Ӱ�쳡���б�ʵ���������Ķ��󣬵��ǻ�Ӱ��ʹ�õ���Դ
        #endregion

        #region ֪ʶ���� ֱ��ʵ��������
        //ֻ������ ��Ҫʵ������ ���� �Ż�ֱ��ʹ�ø÷��� һ�㶼��GameObjectԤ����
        gameobjcetReference.InstantiateAsync();
        #endregion

        #region ֪ʶ���� �Զ����ʶ��
        //�Զ����� �̳�AssetReferenceT<Material>�� �����Զ���һ��ָ�����͵ı�ʶ��
        //�ù�����Ҫ����Unity2020.1֮ǰ����Ϊ֮ǰ�İ汾����ֱ��ʹ��AssetReferenceT�����ֶ�
        #endregion

        #region ֪ʶ��� �ܽ�
        //1.���ǿ��Ը����Լ�������ѡ����ʵı�ʶ�������Դ����
        //2.��Դ���غͳ������ض���ͨ���첽���м���
        //3.��Ҫע���첽������Դʹ��ʱ���뱣֤��Դ�Ѿ������سɹ��ˣ�����ᱨ��
        #endregion
    }

    private void TestFun(AsyncOperationHandle<GameObject> handle)
    {
        //���سɹ��� ʹ�ü��ص���Դ��
        //�ж��Ƿ���سɹ�
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
