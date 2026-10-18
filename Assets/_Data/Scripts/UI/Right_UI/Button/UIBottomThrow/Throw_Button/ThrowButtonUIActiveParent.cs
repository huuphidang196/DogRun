using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowButtonUIActiveParent : ButtonUIActiveParent
{
    [SerializeField] protected Image _Image_UsingThrow;
    public Image Image_UsingThrow => _Image_UsingThrow;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadImageUsingThrow();
    }

    protected virtual void LoadImageUsingThrow()
    {
        if (this._Image_UsingThrow != null) return;

        this._Image_UsingThrow = transform.Find("Sprite_Rep_Throw").GetComponent<Image>();
    }

    public virtual void ChangeItemToThrow(ItemType itemType, Sprite rep)
    {
        this._Image_UsingThrow.sprite = rep;
    }

}
