using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIStandard : StandMonoBehaviour
{
    [SerializeField] protected Button _btnExect;
    public Button BtnExect => _btnExect;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtonExecutive();
    }

    protected virtual void LoadButtonExecutive()
    {
        if (this._btnExect != null) return;
        this._btnExect = GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        this._btnExect.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
      
    }

}
