using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Lesson18 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private List<AsyncOperationHandle<GameObject>> list = new List<AsyncOperationHandle<GameObject>>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>("Cube");
            handle.Completed += (obj) =>
            {
                Instantiate(obj.Result);
            };
            list.Add(handle);

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(list.Count > 0)
            {
                Debug.Log($"remove {list.Count}");
                Addressables.Release(list[0]);
                list.RemoveAt(0);
            }
        }
    }
}
