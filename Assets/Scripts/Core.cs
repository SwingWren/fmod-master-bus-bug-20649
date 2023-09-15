using Cysharp.Threading.Tasks;
using FMOD.Studio;
using NUnit.Framework;
using UnityEngine;

  
namespace Demo {
  public class Core : MonoBehaviour {
    [SerializeField] FMODUnity.EventReference  musicReference;
    
    private void Initialize() {
      FmodAudioBankLoader audioBankLoader = GetComponent<FmodAudioBankLoader>();
      IFmodWrapper fmodWrapper = new FmodWrapper();
      audioBankLoader.Initialize(fmodWrapper);
      LoadStuff(audioBankLoader, fmodWrapper).Forget();
    }

    private async UniTaskVoid LoadStuff(FmodAudioBankLoader audioBankLoader, IFmodWrapper fmodWrapper) {
      await audioBankLoader.LoadBanksFor(SceneId.Demo);
      EventInstance instance = fmodWrapper.CreateInstance(musicReference.Guid);
      instance.start();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void CreateCore() {
      GameObject corePrefab = Resources.Load("Core") as GameObject;
      Assert.IsNotNull(corePrefab, "could not load core prefab, do you have it the Resources folder?");
      GameObject coreGameObject = Instantiate(corePrefab);
      coreGameObject.name = "Core";
      DontDestroyOnLoad(coreGameObject);
      Core core = coreGameObject.GetComponent<Core>();
      coreGameObject.transform.SetAsFirstSibling();
      core.Initialize();
    }
  }
}