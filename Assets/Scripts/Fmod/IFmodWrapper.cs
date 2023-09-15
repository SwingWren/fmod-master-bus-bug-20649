using Cysharp.Threading.Tasks;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo {
  public interface IFmodWrapper {
    void PlayOneShot(GUID eventReferenceGuid, Vector2 position);
    void PlayOneShotAttached(GUID eventReferenceGuid, GameObject trackedObject);
    EventInstance CreateInstance(GUID eventReferenceGuid);
    void AttachInstanceToGameObject(EventInstance eventReference, Transform trackedObjectTransform, Rigidbody2D getComponent);
    void DetachInstanceFromGameObject(EventInstance eventReference);
    void SetParameterByName(string parameterName, float value);
    UniTask LoadBank(AssetReference reference, bool loadSamples);
    void UnloadBank(AssetReference reference);
    void MuteAllEvents(bool muted);
  }
}
