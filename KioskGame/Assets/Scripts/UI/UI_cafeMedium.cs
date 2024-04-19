using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_cafeMedium : UI_Base
{
    enum cafeButtons
    {
        m_cafecancel, m_cafeaccept, m_cafeadd, m_cafesubtract, m_caferegular, m_cafelarge
    }

    private void Start()
    {
        Bind<Button>(typeof(cafeButtons));

        Get<Button>((int)cafeButtons.m_cafecancel).onClick.AddListener(UIManager.Instance.closefullOrder);
        Get<Button>((int)cafeButtons.m_cafeaccept).onClick.AddListener(cafeOrder.Instance.addToOrder);
        Get<Button>((int)cafeButtons.m_cafeadd).onClick.AddListener(cafeOrder.Instance.addQuantity);
        Get<Button>((int)cafeButtons.m_cafesubtract).onClick.AddListener(cafeOrder.Instance.subtractQuatntity);
        Get<Button>((int)cafeButtons.m_caferegular).onClick.AddListener(cafeOrder.Instance.regularSize);
        Get<Button>((int)cafeButtons.m_cafelarge).onClick.AddListener(cafeOrder.Instance.LargeSize);
    }
}
