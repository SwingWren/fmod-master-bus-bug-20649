using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo {
  [CreateAssetMenu(menuName = "Create FmodBankDataLibrary", fileName = "FmodBankDataLibrary", order = 0)]
  public class FmodBankDataLibrary : ScriptableObject {
    [SerializeField] private FmodBankData[] bankDataCollection;
    [NonSerialized] private Dictionary<FmodBankId, FmodBankData> bankDataById;
    
    public FmodBankData BankDataBy(FmodBankId bankId) {
      return bankDataById[bankId];
    }

    public void Initialize() {
      bankDataById = bankDataCollection.ToDictionary(data => data.Id());
    }
  }
}
