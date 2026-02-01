using HKMirror.Hooks.OnHooks;
using UnityEngine;

namespace FastWorld;

public class TimescaleController
{
    private float currentTimeScale = 1f;

    public void SetTimeScale(float timescale)
    {
        currentTimeScale = timescale;
    }

    public void IncreaseTimeScale(float amount)
    {
        currentTimeScale += amount;
    }

    public void DecreaseTimeScale(float amount)
    {
        currentTimeScale -= amount;
    }

    public void ApplyTimeScaleToGame()
    {
        ApplyEngineTimeScale();
        ModifyGameSetTimeScaleFunc();
    }

    private void ApplyEngineTimeScale()
    {
        Time.timeScale = currentTimeScale;
    }

    private void ModifyGameSetTimeScaleFunc()
    {
        OnGameManager.AfterOrig.SetTimeScale_float += args => { 
            if(args.newTimeScale == 1f)
                ApplyEngineTimeScale(); 
        };
    }
}