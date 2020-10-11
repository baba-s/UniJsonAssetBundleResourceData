# UniJsonAssetBundleResourceData

Addressable の AssetBundleResource の情報を JSON 形式で出力できるようにするクラス

## 使用例

```cs
public sealed class CustomAssetBundleResource : IAssetBundleResource
{
    //...

    internal void Start( ProvideHandle provideHandle )
    {
        var jsonData = new JsonAssetBundleResourceData( provideHandle );
        var json     = jsonData.ToPrettyJson();

        Debug.Log( json );

        //...
```

```json
{
    "url": "http://hoge/hoge.unity_scenes_all.bundle",
    "hash": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    "crc": 0000000000,
    "timeout": 30,
    "redirectLimit": -1,
    "retryCount": 2,
    "bundleName": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    "bundleSize": 313180,
    "useCrcForCachedBundle": true,
    "internalId": "Android/hoge.unity_scenes_all.bundle",
    "providerId": "XXXXXXXX",
    "dependencyHashCode": 0,
    "hasDependencies": false,
    "primaryKey": "hoge.unity_scenes_all.bundle",
    "resourceTypeName": "IAssetBundleResource",
    "isCached": true
}
```

例えば Addressable の AssetBundleResource を自作する時に Start 関数で  
ProvideHandle から JsonAssetBundleResourceData を生成することで  
今からダウンロードを開始するアセットバンドルの情報を JSON 形式でログ出力できます  
