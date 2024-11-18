using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSlotController : MonoBehaviour
{
    [SerializeField] private Button[] btnChoices;
    [SerializeField] private Image[] imgChoices;

    private void Start()
    {
        for (int i = 0; i < btnChoices.Length; i++)
        {
            int index = i; 
            btnChoices[i].onClick.AddListener(() => ChangeSlot(index));
        }
    }

    private void ChangeSlot(int index)
    {
        
        switch (index)
        {
            case 0:
                SetChoice(imgChoices.Length - 1, 0);
                break;
            case 1:
                SetChoice(1, 0);
                break;
            case 2:
                SetChoice(0, 1);
                break;
            case 3:
                SetChoice(2, 1);
                break;
            case 4:
                SetChoice(1, 2);
                break;
            case 5:
                SetChoice(0, imgChoices.Length - 1);
                break;
        }
    }

    private void SetChoice(int activeChoiceIndex, int unactiveChoiceIndex)
    {
        imgChoices[activeChoiceIndex].gameObject.SetActive(true);
        imgChoices[unactiveChoiceIndex].gameObject.SetActive(false);
    }
}
