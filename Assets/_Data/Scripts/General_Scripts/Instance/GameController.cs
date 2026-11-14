using System;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("System Config")]
    [SerializeField] private SystemConfigSO _SystemConfig;

    public SystemConfigSO SystemConfig => this._SystemConfig;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadSystemConfig();
    }

    protected virtual void LoadSystemConfig()
    {
        if (this._SystemConfig != null) return;

        this._SystemConfig =
            Resources.Load<SystemConfigSO>("SO/System/SystemConfig");

        if (this._SystemConfig == null)
        {
            Debug.LogError("Không tìm thấy SystemConfigSO!");
            return;
        }

        Debug.Log("Load SystemConfigSO thành công!");
    }
}
