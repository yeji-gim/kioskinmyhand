using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_burgerButtons : UI_Base
{
    enum burgerButtons
    {
        burgerpay, burgeropenordersheet
    }
    // Update is called once per frame
    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(burgerButtons));

        Get<Button>((int)burgerButtons.burgerpay).onClick.AddListener(UIManager.Instance.burgersubmit);
        Get<Button>((int)burgerButtons.burgeropenordersheet).onClick.AddListener(UIManager.Instance.openOrderSheet);
    }
}
