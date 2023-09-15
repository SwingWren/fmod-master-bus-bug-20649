using Cysharp.Threading.Tasks;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo {
  public class FmodWrapper : IFmodWrapper {
    public void PlayOneShot(GUID eventReferenceGuid, Vector2 position) {
      RuntimeManager.PlayOneShot(eventReferenceGuid, position);
    }

    public void PlayOneShotAttached(GUID eventReferenceGuid, GameObject trackedObject) {
      RuntimeManager.PlayOneShotAttached(eventReferenceGuid, trackedObject);
    }

    public EventInstance CreateInstance(GUID eventReferenceGuid) {
      return RuntimeManager.CreateInstance(eventReferenceGuid);
    }

    public void AttachInstanceToGameObject(EventInstance eventReference, Transform trackedObjectTransform,
      Rigidbody2D rgb2D) {
      RuntimeManager.AttachInstanceToGameObject(eventReference, trackedObjectTransform, rgb2D);
    }

    public void DetachInstanceFromGameObject(EventInstance eventReference) {
      RuntimeManager.DetachInstanceFromGameObject(eventReference);
    }

    public void SetParameterByName(string parameterName, float value) {
      RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }

    public async UniTask LoadBank(AssetReference reference, bool loadSamples) {
      UniTaskCompletionSource completionTaskSource = new();
      void OnLoad() {
        completionTaskSource.TrySetResult();
      }
      RuntimeManager.LoadBank(reference, loadSamples, OnLoad);
      await completionTaskSource.Task;
    }

    public void UnloadBank(AssetReference reference) {
      RuntimeManager.UnloadBank(reference);
    }

    public void MuteAllEvents(bool muted) {
      RuntimeManager.StudioSystem.getBus("bus:/", out Bus masterBus);
      masterBus.setMute(muted);
    }
  }
}
