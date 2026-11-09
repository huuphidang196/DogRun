using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ContentPurchasingManagerAbstract : StandMonoBehaviour
{
    [Header("References_Abstract")]
    [SerializeField] protected ContentPurchasingManager _ContentPurchasingManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContentPurchasingManager();
    }

    protected virtual void LoadContentPurchasingManager()
    {
        if (this._ContentPurchasingManager != null) return;
        if (transform.parent == null) return;

        this._ContentPurchasingManager =
            transform.parent.GetComponent<ContentPurchasingManager>();
    }
}
