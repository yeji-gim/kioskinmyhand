using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_burEasy : UI_Base
{
    enum burgerButtons
    {
        e_burcancel, e_buraccept, e_buradd, e_bursubtract
    }

    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(burgerButtons));

        Get<Button>((int)burgerButtons.e_burcancel).onClick.AddListener(UIManager.Instance.closefullOrder);
        Get<Button>((int)burgerButtons.e_buraccept).onClick.AddListener(burgerOrder.Instance.addToOrder);
        Get<Button>((int)burgerButtons.e_buradd).onClick.AddListener(burgerOrder.Instance.addQuantity);
        Get<Button>((int)burgerButtons.e_bursubtract).onClick.AddListener(burgerOrder.Instance.subtractQuatntity);
    }
}
