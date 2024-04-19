using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_cafeEasy : UI_Base
{
    enum cafeButtons
    {
        e_cafecancel, e_cafeaccept, e_cafeadd, e_cafesubtract,
    }

    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(cafeButtons));

        Get<Button>((int)cafeButtons.e_cafecancel).onClick.AddListener(UIManager.Instance.closefullOrder);
        Get<Button>((int)cafeButtons.e_cafeaccept).onClick.AddListener(cafeOrder.Instance.addToOrder);
        Get<Button>((int)cafeButtons.e_cafeadd).onClick.AddListener(cafeOrder.Instance.addQuantity);
        Get<Button>((int)cafeButtons.e_cafesubtract).onClick.AddListener(cafeOrder.Instance.subtractQuatntity);
    }
}
