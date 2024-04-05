using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cancelOrder : MonoBehaviour
{
    public int cellIndex { get; private set; }

    public void SetCellIndex(int index)
    {
        cellIndex = index;
    }
    public void destroycell()
    {
        if (UIManager.Instance.shop == "cafe")
        {
            cafecheckEqual(questGenerator.Instance.cafequestElements, cafeOrder.Instance.orders); // �ùٸ� �ֹ��̾����� Ȯ��
            cafeOrder.Instance.deleteOrder(cellIndex); // cellIndex�� �ش��ϴ� �ֹ� ���
        }
        if (UIManager.Instance.shop == "rest")
        {
            burgercheckEqual(questGenerator.Instance.burgerquestElements, burgerOrder.Instance.orders); // �ùٸ� �ֹ��̾����� Ȯ��
            burgerOrder.Instance.deleteOrder(cellIndex); // cellIndex�� �ش��ϴ� �ֹ� ���
        }
        Destroy(gameObject); // �׸��� ���̾ƿ� ��� ����
    }

    public void cafecheckEqual(List<cafeQuestElement> quests, List<cafeOrderElement> orders)
    {

        for (int i = 0; i < quests.Count; i++)
        {
            cafeQuestElement quest = quests[i];
            for (int j = 0; j < orders.Count; j++)
            {
                cafeOrderElement order = orders[j];

                if (quest.Equals(order))
                {
                    Transform childTransform = UIManager.Instance.gridLayoutGroup.transform.GetChild(i);
                    GameObject childGameObject = childTransform.gameObject;
                    Transform check = childGameObject.transform.Find("check");
                    Transform blank = childGameObject.transform.Find("blank");

                    if (check != null)
                        check.gameObject.SetActive(false);
                    if (blank != null)
                        blank.gameObject.SetActive(true);
                }
            }

        }
    }

    public void burgercheckEqual(List<burgerQuestElement> quests, List<burgerOrderElement> orders)
    {

        for (int i = 0; i < quests.Count; i++)
        {
            burgerQuestElement quest = quests[i];

            for (int j = 0; j < orders.Count; j++)
            {
                burgerOrderElement order = orders[j];
                if (quest.Equals(order))
                {
                    Transform childTransform = UIManager.Instance.gridLayoutGroup.transform.GetChild(i);
                    GameObject childGameObject = childTransform.gameObject;
                    Transform check = childGameObject.transform.Find("check");
                    Transform blank = childGameObject.transform.Find("blank");

                    if (check != null)
                        check.gameObject.SetActive(false);
                    if (blank != null)
                        blank.gameObject.SetActive(true);
                }
            }

        }
    }
}
