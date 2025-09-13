using System;
using DG.Tweening;
using RTLTMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Common.ArrangingGameItemType itemType;
    public RTLTextMeshPro ContainerText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DragObject>(out var dragObject))
        {
            other.transform.DOKill();
            other.gameObject.transform.DOScale(dragObject.defaultScale * 0.45f, 0.5f).SetEase(GS.INS.CBButtonsOnEase);


            if (dragObject.arrangingGameItemType == itemType || dragObject.secondArrangingGameItemType == itemType)
            {
                ArrangingGameHandler.Instance.inBoxCount++;
                ArrangingGameHandler.Instance.UpdateScore();
            }
            else
            {

                ArrangingGameHandler.Instance.wrongInBoxCount++;
                ArrangingGameHandler.Instance.UpdateScore();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DragObject>(out var dragObject))
        {
            other.transform.DOKill();

            other.gameObject.transform.DOScale(dragObject.defaultScale, 0.5f).SetEase(GS.INS.CBButtonsOnEase);
            
            if (dragObject.arrangingGameItemType == itemType || dragObject.secondArrangingGameItemType == itemType)
            {                

                ArrangingGameHandler.Instance.inBoxCount--;
                ArrangingGameHandler.Instance.UpdateScore();
            }
            else
            {                

                ArrangingGameHandler.Instance.wrongInBoxCount--;
                ArrangingGameHandler.Instance.UpdateScore();
            }
        }
    }

    public void Initialize(string text, Common.ArrangingGameItemType type)
    {
        SetContainerText(text);
        SetContainerType(type);
    }
    
    private void SetContainerText(string text)
    {
        ContainerText.text = text;
    }
    
    public void SetContainerType(Common.ArrangingGameItemType type)
    {
        itemType = type;
    }
}