using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_burHardDrinks : UI_Base
{
    enum burgerButtons
    {
       h_cola, h_soda, h_orange, h_water
    }

    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(burgerButtons));

        Get<Button>((int)burgerButtons.h_cola).onClick.AddListener(burgerOrder.Instance.cola);
        Get<Button>((int)burgerButtons.h_soda).onClick.AddListener(burgerOrder.Instance.soda);
        Get<Button>((int)burgerButtons.h_orange).onClick.AddListener(burgerOrder.Instance.orangejuice);
        Get<Button>((int)burgerButtons.h_water).onClick.AddListener(burgerOrder.Instance.water);
    }
}
