using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Linq;

public class AvatarSelectorController : MonoBehaviour {

    public GameObject avatarEntryPrefab;
    public Transform avatarListRoot;
    public Camera m_ThumbnailCamera;

    
    public enum AvatarSource
    {
        LocalResources, // loads prefabs from ../Assets/Resources/Avatars
        LocalAssetBundle, // loads AssetBundle from StreamingAssets/AssetBundles folder
        RemoteAssetBundle // loads AssetBundle from URL
    }
    [Space(10)]
    [Tooltip("LocalResources: loads prefabs from ../Assets/Resources/Avatars;\n\nLocalAssetBundle: loads AssetBundle from StreamingAssets/AssetBundles folder;\n\nRemoteAssetBundle: loads AssetBundle from URL;")]
    public AvatarSource m_AvatarSource = AvatarSource.LocalResources;

    [Tooltip("The link must directly go to the AssetBundle (if the link is opened, it initiates the file download immediately, without pressing anything).")]
    public string m_RemoteAvatarAssetBundleLink = "https://drive.google.com/uc?export=download&id=1adLLi_QTR05RrK12lXOgvGhEbbmaRSco";
    public string m_LocalAvatarAssetBundleName = "avatars";

    //[Space(10)]
    //[SerializeField]
    //public List<AvatarItem> m_AvatarList;

    //private GameObject[] m_avatarObjects;


    private Color m_ThumbnailBackColor = new Color(1f, 1f, 1f, 0f);

    // Start is called before the first frame update
    void Start () {
        switch (m_AvatarSource)
        {
            case AvatarSource.LocalResources:
                LoadLocalAvatarsFromResources();
                return;
            case AvatarSource.LocalAssetBundle:
                StartCoroutine(LoadLocalAvatarsFromAssetBundle());
                return;
            case AvatarSource.RemoteAssetBundle:
                StartCoroutine(LoadRemoteAvatarsFromAssetBundle());
                return;
            default:
                return;
        }
    }

    // Update is called once per frame
    void Update () {

    }

    void LoadLocalAvatarsFromAssetBundleN() // old 
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = Path.Combine(filePath, m_LocalAvatarAssetBundleName);
        var assetBundle = AssetBundle.LoadFromFile(filePath);

        if (assetBundle == null)
        {
            Debug.LogWarning("Specified AssetBundle not found.");
            return;
        }

        GameObject[] avatarObjects = assetBundle.LoadAllAssets<GameObject>();

        CreateAvatarItems(avatarObjects);
    }

    IEnumerator LoadLocalAvatarsFromAssetBundle()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = Path.Combine(filePath, m_LocalAvatarAssetBundleName);

        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return assetBundleCreateRequest;

        AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;

        AssetBundleRequest assets = assetBundle.LoadAllAssetsAsync<GameObject>();
        yield return assets;

        GameObject[] avatarObjects = assets.allAssets.Cast<GameObject>().ToArray();

        StartCoroutine(CreateAvatarItems(avatarObjects));
    }

    void LoadLocalAvatarsFromResources()
    {
        GameObject[] avatarObjects = Resources.LoadAll("Avatars", typeof(GameObject)).Cast<GameObject>().ToArray();

        StartCoroutine(CreateAvatarItems(avatarObjects));
    }

    IEnumerator LoadRemoteAvatarsFromAssetBundle() // create async download and load
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(m_RemoteAvatarAssetBundleLink);

        uwr.SendWebRequest();
        
        while(uwr.isDone == false) { yield return null; }

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
            AssetBundleRequest assets = bundle.LoadAllAssetsAsync<GameObject>();
            yield return assets;
            GameObject[] avatarObjects = assets.allAssets.Cast<GameObject>().ToArray();
            StartCoroutine(CreateAvatarItems(avatarObjects));
        }
        
    }

    IEnumerator CreateAvatarItems(GameObject[] avatarObjects)
    {
        if(avatarObjects == null)
        {
            Debug.LogWarning("No valid Avatar objects loaded.");
            yield return null;
        }
        foreach (var aObj in avatarObjects)
        {
            AvatarItem a = new AvatarItem();
            a.label = aObj.name;
            a.avatar = aObj;
            a.thumbnail = GenerateAvatarThumbnail(aObj);
            yield return null;
            GenerateAvatarItem(a);
            yield return null;
        }
    }

    //void GenerateAvatarItems ()
    //{
    //    for (int i = 0; i < m_AvatarList.Count; i++)
    //    {
    //        GameObject go = Instantiate (avatarEntryPrefab);
    //        go.transform.position = Vector3.zero;
    //        go.transform.rotation = Quaternion.identity;
    //        go.transform.SetParent(avatarListRoot, false);
    //        go.GetComponent<AvatarSelectItem> ().SetupAvatarItem (m_AvatarList[i]);
    //    }
    //}
    void GenerateAvatarItem(AvatarItem _a)
    {

        GameObject go = Instantiate(avatarEntryPrefab);
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.SetParent(avatarListRoot, false);
        go.GetComponent<AvatarSelectItem>().SetupAvatarItem(_a);
    
    }

    Texture2D GenerateAvatarThumbnail(GameObject _gameObject)
    {
        RuntimePreviewGenerator.PreviewRenderCamera = m_ThumbnailCamera;
        RuntimePreviewGenerator.BackgroundColor = m_ThumbnailBackColor;
        Texture2D tex = RuntimePreviewGenerator.GenerateModelPreview(_gameObject.transform, 256, 256, true);
        return tex;
    }
}

[System.Serializable]
public class AvatarItem{
    public string label = "Avatar";
    public GameObject avatar;
    public Texture2D thumbnail;
}