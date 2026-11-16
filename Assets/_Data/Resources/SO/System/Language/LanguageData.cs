using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "LanguageConfig",
    menuName = "SO/Language Config"
)]
public class LanguageConfigSO : ScriptableObject
{
    [SerializeField] private GameLanguage _Language;

    [SerializeField] private List<LanguageData> _LanguageDatas;

    public GameLanguage Language => this._Language;
    public List<LanguageData> LanguageDatas => this._LanguageDatas;
}

[Serializable]
public class LanguageData
{
    public string Key;
    [TextArea] public string Value;
}