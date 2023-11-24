using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Lesson13 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetsAsync<GameObject>(new List<string>() { "Cube", "SD" }, (obj) =>
        {
            Instantiate(obj, new Vector3(0,0,0), Quaternion.identity);
        }, Addressables.MergeMode.Intersection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
