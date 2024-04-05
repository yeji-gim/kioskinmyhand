using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_burMedium : UI_Base
{
    enum burgerButtons
    {
        m_burcancel, m_buraccept, m_buradd, m_bursubtract, m_no, m_onion, m_pickle,
    }

    private void Start()
    {
        // Reflection ÀÌ¿כ
        Bind<Button>(typeof(burgerButtons));

        Get<Button>((int)burgerButtons.m_burcancel).onClick.AddListener(UIManager.Instance.closefullOrder);
        Get<Button>((int)burgerButtons.m_buraccept).onClick.AddListener(burgerOrder.Instance.addToOrder);
        Get<Button>((int)burgerButtons.m_buradd).onClick.AddListener(burgerOrder.Instance.addQuantity);
        Get<Button>((int)burgerButtons.m_bursubtract).onClick.AddListener(burgerOrder.Instance.subtracQuatntity);
        Get<Button>((int)burgerButtons.m_no).onClick.AddListener(burgerOrder.Instance.detailsno);
        Get<Button>((int)burgerButtons.m_onion).onClick.AddListener(burgerOrder.Instance.detailonion);
        Get<Button>((int)burgerButtons.m_pickle).onClick.AddListener(burgerOrder.Instance.detailpickle);
    }
}
