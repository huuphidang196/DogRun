using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseText : StandMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _Text;
    public TextMeshProUGUI Text => this._Text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this._Text != null) return;
        this._Text = GetComponent<TextMeshProUGUI>();
    }

    // 👉 Hàm chính để set nội dung text, các class con dùng lại thay vì tự động this._Text.text
    public virtual void SetText(string content)
    {
        if (this._Text == null) return;
        this._Text.text = content;
    }

    // 👉 Overload tiện dụng cho các trường hợp set số (count, HP, gold...)
    public virtual void SetText(int value)
    {
        this.SetText(value.ToString());
    }

    public virtual void SetText(float value, string format = "F1")
    {
        this.SetText(value.ToString(format));
    }

    public virtual string GetText()
    {
        return this._Text != null ? this._Text.text : string.Empty;
    }

    public virtual void SetColor(Color color)
    {
        if (this._Text == null) return;
        this._Text.color = color;
    }

    public virtual void SetVisible(bool visible)
    {
        if (this._Text == null) return;
        this._Text.gameObject.SetActive(visible);
    }
}