using UnityEngine;

namespace Demo {
  [System.Serializable]
  public class FmodBankData {
    [SerializeField] private FmodBankId id;

    public FmodBankId Id() {
      return id;
    }

    [SerializeField] private FmodSubBankData[] subBankDataCollection;

    public FmodSubBankData[] SubBankDataCollection() {
      return subBankDataCollection;
    }
  }
}
