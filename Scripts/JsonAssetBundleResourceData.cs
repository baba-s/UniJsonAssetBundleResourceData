using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace UniAddressLib
{
	/// <summary>
	/// アセットバンドルダウンロード時の情報を JSON の文字列に変換するためのクラス
	/// </summary>
	public readonly struct JsonAssetBundleResourceData
	{
		//================================================================================
		// 構造体
		//================================================================================
		[Serializable]
		private struct JsonData
		{
			[SerializeField][UsedImplicitly] private string url;
			[SerializeField][UsedImplicitly] private string hash;
			[SerializeField][UsedImplicitly] private uint   crc;
			[SerializeField][UsedImplicitly] private int    timeout;
			[SerializeField][UsedImplicitly] private int    redirectLimit;
			[SerializeField][UsedImplicitly] private int    retryCount;
			[SerializeField][UsedImplicitly] private string bundleName;
			[SerializeField][UsedImplicitly] private long   bundleSize;
			[SerializeField][UsedImplicitly] private bool   useCrcForCachedBundle;
			[SerializeField][UsedImplicitly] private string internalId;
			[SerializeField][UsedImplicitly] private string providerId;
			[SerializeField][UsedImplicitly] private int    dependencyHashCode;
			[SerializeField][UsedImplicitly] private bool   hasDependencies;
			[SerializeField][UsedImplicitly] private string primaryKey;
			[SerializeField][UsedImplicitly] private string resourceTypeName;
			[SerializeField][UsedImplicitly] private bool   isCached;

			public JsonData( JsonAssetBundleResourceData data )
			{
				url                   = data.Url;
				hash                  = data.Hash;
				crc                   = data.Crc;
				timeout               = data.Timeout;
				redirectLimit         = data.RedirectLimit;
				retryCount            = data.RetryCount;
				bundleName            = data.BundleName;
				bundleSize            = data.BundleSize;
				useCrcForCachedBundle = data.UseCrcForCachedBundle;
				internalId            = data.InternalId;
				providerId            = data.ProviderId;
				dependencyHashCode    = data.DependencyHashCode;
				hasDependencies       = data.HasDependencies;
				primaryKey            = data.PrimaryKey;
				resourceTypeName      = data.ResourceTypeName;
				isCached              = data.IsCached;
			}
		}

		//================================================================================
		// プロパティ
		//================================================================================
		public string Url                   { get; }
		public string Hash                  { get; }
		public uint   Crc                   { get; }
		public int    Timeout               { get; }
		public int    RedirectLimit         { get; }
		public int    RetryCount            { get; }
		public string BundleName            { get; }
		public long   BundleSize            { get; }
		public bool   UseCrcForCachedBundle { get; }
		public string InternalId            { get; }
		public string ProviderId            { get; }
		public int    DependencyHashCode    { get; }
		public bool   HasDependencies       { get; }
		public string PrimaryKey            { get; }
		public string ResourceTypeName      { get; }

		public Hash128 Hash128 => Hash128.Parse( Hash );

		public bool IsCached
		{
			get
			{
				var cachedBundle = new CachedAssetBundle( BundleName, Hash128 );
				return Caching.IsVersionCached( cachedBundle );
			}
		}

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public JsonAssetBundleResourceData( ProvideHandle provideHandle ) : this
		(
			resourceManager: provideHandle.ResourceManager,
			options: ( AssetBundleRequestOptions ) provideHandle.Location.Data,
			location: provideHandle.Location
		)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public JsonAssetBundleResourceData( [NotNull] IResourceLocation location ) : this
		(
			resourceManager: Addressables.ResourceManager,
			options: ( AssetBundleRequestOptions ) location.Data,
			location: location
		)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public JsonAssetBundleResourceData
		(
			[NotNull] ResourceManager           resourceManager,
			[NotNull] AssetBundleRequestOptions options,
			[NotNull] IResourceLocation         location
		)
		{
			Url                   = resourceManager.TransformInternalId( location );
			Hash                  = options.Hash;
			Crc                   = options.Crc;
			Timeout               = options.Timeout;
			RedirectLimit         = options.RedirectLimit;
			RetryCount            = options.RetryCount;
			BundleName            = options.BundleName;
			BundleSize            = options.BundleSize;
			UseCrcForCachedBundle = options.UseCrcForCachedBundle;
			InternalId            = location.InternalId;
			ProviderId            = location.ProviderId;
			DependencyHashCode    = location.DependencyHashCode;
			HasDependencies       = location.HasDependencies;
			PrimaryKey            = location.PrimaryKey;
			ResourceTypeName      = location.ResourceType.Name;
		}

		/// <summary>
		/// JSON の文字列に変換して返します
		/// </summary>
		public string ToJson()
		{
			var jsonData = CreateJsonData();
			return JsonUtility.ToJson( jsonData );
		}

		/// <summary>
		/// 読みやすい JSON の文字列に変換して返します
		/// </summary>
		public string ToPrettyJson()
		{
			var jsonData = CreateJsonData();
			return JsonUtility.ToJson( jsonData, true );
		}

		/// <summary>
		/// JSON 変換用のインスタンスを作成して返します
		/// </summary>
		private JsonData CreateJsonData()
		{
			return new JsonData( this );
		}
	}
}