using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Demo {
  public class FmodAudioBankLoader : MonoBehaviour {
    [SerializeField] private FmodBankDataLibrary bankDataLibrary;
    [SerializeField] private ScreenFmodBankConfig[] bankConfig;

    private HashSet<FmodBankId> secondaryBanksLoadedAndLoading;
    private HashSet<FmodBankId> loadedBanks;

    private Dictionary<SceneId, ScreenFmodBankConfig> bankConfigBySceneId;
    private bool hasLoadedMainBanks;

    private IFmodWrapper fmodWrapper;

    public async UniTask LoadBanksFor(SceneId screenId) {
      await LoadBanksTracked(screenId);
    }

    private async UniTask LoadMainBanks() {
      await LoadBank(FmodBankId.Master);
    }

    private async UniTask LoadBanksTracked(SceneId screenId) {
      await InternalLoadBanks(screenId);
    }

    private async UniTask InternalLoadBanks(SceneId screenId) {
      if (!hasLoadedMainBanks)
      {
        await LoadMainBanks();
        hasLoadedMainBanks = true;
      }

      ScreenFmodBankConfig config = bankConfigBySceneId[screenId];
      List<UniTask> loadingBankTaskCollection = new();

      HashSet<FmodBankId> newBanks = new(config.Banks());

      foreach (FmodBankId bankId in secondaryBanksLoadedAndLoading.ToArray())
      {
        if (!newBanks.Contains(bankId))
        {
          secondaryBanksLoadedAndLoading.Remove(bankId);
          UnloadBank(bankId);
        }
      }

      foreach (FmodBankId bankId in newBanks)
      {
        if (secondaryBanksLoadedAndLoading.Contains(bankId))
        {
          continue;
        }

        secondaryBanksLoadedAndLoading.Add(bankId);
        loadingBankTaskCollection.Add(LoadBank(bankId));
      }

      await UniTask.WhenAll(loadingBankTaskCollection);
    }

    private async UniTask LoadBank(FmodBankId bankId) {
      FmodBankData bankData = bankDataLibrary.BankDataBy(bankId);
      foreach (FmodSubBankData dubBankData in bankData.SubBankDataCollection())
      {
        await fmodWrapper.LoadBank(dubBankData.Reference(), dubBankData.LoadSamples());
      }

      loadedBanks.Add(bankId);
    }

    public bool IsBankLoaded(FmodBankId bankId) {
      return loadedBanks.Contains(bankId);
    }

    private void UnloadBank(FmodBankId bankId) {
      loadedBanks.Remove(bankId);
      FmodBankData bankData = bankDataLibrary.BankDataBy(bankId);
      foreach (FmodSubBankData dubBankData in bankData.SubBankDataCollection().Reverse())
      {
        fmodWrapper.UnloadBank(dubBankData.Reference());
      }
    }

    public void UnloadAllBanks() {
      foreach (FmodBankId bankId in secondaryBanksLoadedAndLoading)
      {
        UnloadBank(bankId);
      }
      UnloadBank(FmodBankId.Master);
    }

    public void Initialize(IFmodWrapper fmodWrapper) {
      this.fmodWrapper = fmodWrapper;
      secondaryBanksLoadedAndLoading = new HashSet<FmodBankId>();
      loadedBanks = new HashSet<FmodBankId>();
      bankConfigBySceneId = bankConfig.ToDictionary(config => config.ScreenId());
      bankDataLibrary.Initialize();
    }
  }
}