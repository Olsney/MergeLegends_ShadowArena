using Code.StaticData.Tutorial;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "StaticData/Tutorial")]
public class TutorialStaticData : ScriptableObject
{
    [Header("Buy steps")] 
    public int MeleePurchasesRequired;
    public int RangedPurchasesRequired;

    [Header("Spotlight")] 
    public float PurchaseButtonsRadius;
    public float UnitsInCellRadius;
    public float StartFightButtonRadius;

    [Header("Cell sequences")] 
    public CellStep[] MergeSteps;
    public CellStep[] MoveSteps;
    
    [Header("Overlay")]
    [Range(0.1f, 1f)] public float RaycastRadiusMultiplier;

    [Header("Pointer")]
    public float PointerMoveDuration = 0.6f;
    public float PointerMoveDistance = 15f;
}