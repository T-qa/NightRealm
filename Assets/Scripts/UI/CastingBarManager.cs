using Tqa.DungeonQuest.AbilitySystem;
using Tqa.DungeonQuest.ObjectPooling;
using UnityEngine;

public class CastingBarManager : GlobalReference<CastingBarManager>
{
    [SerializeField]
    private Prefab castingBarPrefab;

    [SerializeField]
    private Transform worldSpaceCanvas;

    public void ShowCastingBar(AbilityCaster caster)
    {
        if (PoolManager.Get<CastingBar>(castingBarPrefab, out var castingBar))
        {
            castingBar.transform.SetParent(worldSpaceCanvas);
            castingBar.ShowBar(caster);
        }
    }
}
