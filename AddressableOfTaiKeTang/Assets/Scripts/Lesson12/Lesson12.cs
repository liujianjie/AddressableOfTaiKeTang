using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Lesson12 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("开始加载。。。");
        Addressables.LoadAssetsAsync<GameObject>(new List<string>() { "Cube", "SD" }, (obj) =>
        {
            var newObj = Instantiate(obj);
            newObj.transform.position = new Vector3(0, 0, 0);
        }, Addressables.MergeMode.Intersection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
