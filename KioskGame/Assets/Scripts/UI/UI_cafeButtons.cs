using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_cafeButtons : UI_Base
{
    enum cafeButtons
    {
        cafepay, cafeopenordersheet,
    }
    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(cafeButtons));

        Get<Button>((int)cafeButtons.cafepay).onClick.AddListener(UIManager.Instance.cafesubmit);
        Get<Button>((int)cafeButtons.cafeopenordersheet).onClick.AddListener(UIManager.Instance.openOrderSheet);
    }
}