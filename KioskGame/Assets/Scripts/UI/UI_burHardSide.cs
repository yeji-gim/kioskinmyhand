using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_burHardSide : UI_Base
{
    enum burgerButtons
    {
        h_french, h_cheese, h_chicken
    }

    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(burgerButtons));

        Get<Button>((int)burgerButtons.h_french).onClick.AddListener(burgerOrder.Instance.frenchfries);
        Get<Button>((int)burgerButtons.h_cheese).onClick.AddListener(burgerOrder.Instance.cheesestick);
        Get<Button>((int)burgerButtons.h_chicken).onClick.AddListener(burgerOrder.Instance.chicken);
    }
}
