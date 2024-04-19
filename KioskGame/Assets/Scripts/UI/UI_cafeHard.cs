using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_cafeHard : UI_Base
{
    enum cafeButtons
    {
        h_cafecancel, h_cafeaccept, h_cafeadd, h_cafesubtract, h_caferegular, h_cafelarge, h_sweet, h_fruit, h_decaffine, h_1shot, h_2shot
    }

    private void Start()
    {
        Bind<Button>(typeof(cafeButtons));

        Get<Button>((int)cafeButtons.h_cafecancel).onClick.AddListener(UIManager.Instance.closefullOrder);
        Get<Button>((int)cafeButtons.h_cafeaccept).onClick.AddListener(cafeOrder.Instance.addToOrder);
        Get<Button>((int)cafeButtons.h_cafeadd).onClick.AddListener(cafeOrder.Instance.addQuantity);
        Get<Button>((int)cafeButtons.h_cafesubtract).onClick.AddListener(cafeOrder.Instance.subtractQuatntity);
        Get<Button>((int)cafeButtons.h_caferegular).onClick.AddListener(cafeOrder.Instance.regularSize);
        Get<Button>((int)cafeButtons.h_cafelarge).onClick.AddListener(cafeOrder.Instance.LargeSize);
        Get<Button>((int)cafeButtons.h_sweet).onClick.AddListener(cafeOrder.Instance.cocoacaffeine);
        Get<Button>((int)cafeButtons.h_fruit).onClick.AddListener(cafeOrder.Instance.fruitcaffeine);
        Get<Button>((int)cafeButtons.h_decaffine).onClick.AddListener(cafeOrder.Instance.decaffeine);
        Get<Button>((int)cafeButtons.h_1shot).onClick.AddListener(cafeOrder.Instance.oneshot);
        Get<Button>((int)cafeButtons.h_2shot).onClick.AddListener(cafeOrder.Instance.twoshot);
    }
}
