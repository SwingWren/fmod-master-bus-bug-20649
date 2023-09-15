using UnityEngine;

namespace Demo {
  [System.Serializable]
  public class ScreenFmodBankConfig {
    [SerializeField] private SceneId screenId;

    public SceneId ScreenId() {
      return screenId;
    }

    [SerializeField] private FmodBankId[] requiredBanksCollection;

    public FmodBankId[] Banks() {
      return requiredBanksCollection;
    }
  }
}
