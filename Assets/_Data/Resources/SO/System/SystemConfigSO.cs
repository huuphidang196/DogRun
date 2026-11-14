
using UnityEngine;

[CreateAssetMenu(
    fileName = "SystemConfig",
    menuName = "SO/SystemConfigSO"
)]
public class SystemConfigSO : ScriptableObject
{
    [Header("Language Settings")]
    [SerializeField] private GameLanguage _DefaultLanguage = GameLanguage.Vietnamese;

    [Header("Audio Settings")]
    [SerializeField, Range(0f, 1f)] private float _MusicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _SFXVolume = 1f;
    [SerializeField] private bool _MusicEnabled = true;
    [SerializeField] private bool _SFXEnabled = true;

    [Header("Gameplay Settings")]
    [SerializeField] private bool _AutoSave = true;
    [SerializeField] private bool _ShowTutorial = true;

    [Header("System Settings")]
    [SerializeField] private float _SaveInterval = 60f;

    #region Properties

    public GameLanguage DefaultLanguage => _DefaultLanguage;

    public float MusicVolume => _MusicVolume;
    public float SFXVolume => _SFXVolume;

    public bool MusicEnabled => _MusicEnabled;
    public bool SFXEnabled => _SFXEnabled;

    public bool AutoSave => _AutoSave;
    public bool ShowTutorial => _ShowTutorial;
    public float SaveInterval => _SaveInterval;

    #endregion
}
public enum GameLanguage
{
    Vietnamese,
    English
}