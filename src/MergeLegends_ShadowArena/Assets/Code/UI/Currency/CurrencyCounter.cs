using System;
using TMPro;
using UnityEngine;

namespace Code.UI.Currency
{
    public class CurrencyCounter : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI CurrentValue { get; private set; }
        
        //TODO: Дописать сюда когда будет что. 
        // protected IWorldData WorldData;

        // private void Start()
        // {
        //     UpdateCounter();
        // }

        // protected int Score => WorldData.CurrencyCountValue;
        
        // [Inject]
        // public void Construct(IWorldData worldData)
        // {
        //   WorldData = worldData;
        //   WorldData.CurrecnyData.Changed += UpdateCounter;
        // }

        // private void UpdateCounter()
        // {
        //     CurrentValue.text = $"{WoroldData.CurrecnyData.Collected}"
        // }
    }
}