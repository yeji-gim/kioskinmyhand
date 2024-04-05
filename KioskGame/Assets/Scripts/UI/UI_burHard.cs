using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_burHard : UI_Base
{
    enum burgerButtons
    {
        h_burcancel, h_buraccept, h_buradd, h_bursubtract, h_single, h_set
    }

    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(burgerButtons));

        Get<Button>((int)burgerButtons.h_burcancel).onClick.AddListener(UIManager.Instance.closefullOrder);
        Get<Button>((int)burgerButtons.h_buraccept).onClick.AddListener(burgerOrder.Instance.addToOrder);
        Get<Button>((int)burgerButtons.h_buradd).onClick.AddListener(burgerOrder.Instance.addQuantity);
        Get<Button>((int)burgerButtons.h_bursubtract).onClick.AddListener(burgerOrder.Instance.subtracQuatntity);
        Get<Button>((int)burgerButtons.h_single).onClick.AddListener(burgerOrder.Instance.isnoSet);
        Get<Button>((int)burgerButtons.h_set).onClick.AddListener(burgerOrder.Instance.isSet);
    }
}
