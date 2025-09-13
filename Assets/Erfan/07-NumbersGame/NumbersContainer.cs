using System;
using DG.Tweening;
using RTLTMPro;
using UnityEngine;

public class NumbersContainer : MonoBehaviour
{
    public Common.NumbersGameItemType itemType;
    public RTLTextMeshPro ContainerText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NumbersGameDragObject>(out var dragObject))
        {
            other.transform.DOKill();

            other.gameObject.transform.DOScale(Vector3.one * 0.35f, 0.5f).SetEase(GS.INS.CBButtonsOnEase);
            var zone = NumbersGameHandler.Instance._zoneDConfig;
            if (zone.location == Common.Location.School) //SCHOOL
            {
                var mDragObject = (NumbersGameDragObjectSchool)dragObject;
                if (zone.isRange)
                {
                    if (!(mDragObject.number >= zone.rangeMin && mDragObject.number <= zone.rangeMax))
                    {
                        NumbersGameHandler.Instance.wrongInBoxCount++;
                        NumbersGameHandler.Instance.UpdateScore();
                        return;
                    }
                }

                if (mDragObject.numbersGameItemType == itemType)
                {
                    NumbersGameHandler.Instance.rightInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                }
                else
                {
                    NumbersGameHandler.Instance.wrongInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                }
            }

            else if (zone.location == Common.Location.AmusementPark) //SCHOOL
            {
                var mDragObject = (NumbersGameDragObjectAmusement)dragObject;
                if (mDragObject.isBiggerThanHuman)
                {
                    NumbersGameHandler.Instance.wrongInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                    return;
                }
                if (mDragObject.numbersGameItemType == itemType)
                {
                    NumbersGameHandler.Instance.rightInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                }
                else
                {
                    NumbersGameHandler.Instance.wrongInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                }
            }
            
            else if (zone.location == Common.Location.Hospital) //HOSPITAL
            {
                var mDragObject = (NumbersGameDragObjectHospital)dragObject;
                if (mDragObject.isFalse)
                {
                    NumbersGameHandler.Instance.wrongInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                    return;
                }
                if (mDragObject.numbersGameItemType == itemType)
                {
                    NumbersGameHandler.Instance.rightInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                }
                else
                {
                    NumbersGameHandler.Instance.wrongInBoxCount++;
                    NumbersGameHandler.Instance.UpdateScore();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NumbersGameDragObject>(out var dragObject))
        {
            other.transform.DOKill();

            other.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(GS.INS.CBButtonsOnEase);

            var zone = NumbersGameHandler.Instance._zoneDConfig;
            if (zone.location == Common.Location.School) //SCHOOL
            {
                var mDragObject = (NumbersGameDragObjectSchool)dragObject;
                if (zone.isRange)
                {
                    if (!(mDragObject.number >= zone.rangeMin && mDragObject.number <= zone.rangeMax))
                    {
                        NumbersGameHandler.Instance.wrongInBoxCount--;
                        NumbersGameHandler.Instance.UpdateScore();
                        return;
                    }
                }

                if (mDragObject.numbersGameItemType == itemType)
                {
                    NumbersGameHandler.Instance.rightInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                }
                else
                {
                    NumbersGameHandler.Instance.wrongInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                }
            }
            else if (zone.location == Common.Location.AmusementPark) //SCHOOL
            {
                var mDragObject = (NumbersGameDragObjectAmusement)dragObject;
                if (mDragObject.isBiggerThanHuman)
                {
                    NumbersGameHandler.Instance.wrongInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                    return;
                }

                if (mDragObject.numbersGameItemType == itemType)
                {
                    NumbersGameHandler.Instance.rightInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                }
                else
                {
                    NumbersGameHandler.Instance.wrongInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                }
            }
            else if (zone.location == Common.Location.Hospital) //HOSPITAL
            {
                var mDragObject = (NumbersGameDragObjectHospital)dragObject;
                if (mDragObject.isFalse)
                {
                    NumbersGameHandler.Instance.wrongInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                    return;
                }
                if (mDragObject.numbersGameItemType == itemType)
                {
                    NumbersGameHandler.Instance.rightInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                }
                else
                {
                    NumbersGameHandler.Instance.wrongInBoxCount--;
                    NumbersGameHandler.Instance.UpdateScore();
                }
            }
        }
    }


    public void Initialize(string text, Common.NumbersGameItemType type)
    {
        SetContainerText(text);
        SetContainerType(type);
    }

    private void SetContainerText(string text)
    {
        ContainerText.text = text;
    }

    public void SetContainerType(Common.NumbersGameItemType type)
    {
        itemType = type;
    }
}