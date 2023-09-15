using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo {
  [System.Serializable]
  public class FmodSubBankData {
    [SerializeField] private AssetReference assetReference;

    public AssetReference Reference() {
      return assetReference;
    }

    [SerializeField] private bool loadSamples;

    public bool LoadSamples() {
      return loadSamples;
    }
  }
}
