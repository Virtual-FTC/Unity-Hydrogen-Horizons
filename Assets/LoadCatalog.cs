using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using static System.Net.WebRequestMethods;
using UnityEngine.Events;

public class LoadCatalog : MonoBehaviour
{
    public UnityEvent<IResourceLocator> OnCatalogLoaded;
    // Start is called before the first frame update
    public string address = "https://powerplay.vrobotsim.online/catalogs/testcatalog.json";
    void Start()
    {
        StartCoroutine("StartLoad");
    }

    public IEnumerator StartLoad()
    {
        Addressables.ClearResourceLocators();
        //Load a catalog and automatically release the operation handle.
        AsyncOperationHandle<IResourceLocator> handle = Addressables.LoadContentCatalogAsync(address, true);
        yield return handle;

        var loadResourceLocationsHandle
        = Addressables.LoadResourceLocationsAsync("robot", typeof(GameObject));

        if (!loadResourceLocationsHandle.IsDone)
            yield return loadResourceLocationsHandle;

        IResourceLocator log = handle.Result;
        Debug.Log("found remote catalog:" + handle.Result);
        foreach (IResourceLocation location in loadResourceLocationsHandle.Result)
        {
            Debug.Log(location);
        }
        OnCatalogLoaded.Invoke(handle.Result);

        Debug.Log(handle);
    }

}
