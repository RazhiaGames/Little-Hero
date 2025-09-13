using System;
using DG.Tweening;
using RTLTMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Butterfly : MonoBehaviour
{
    private float offset;
    public Button button;
    public int butterflyNumber;
    public RTLTextMeshPro numberText;
    public Image numberBg;

    public UnityEvent onSelectRightFlower;
    public UnityEvent onSelectWrongFlower;
    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }



    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    void Start()
    {
        offset = Random.Range(-1f, 1f);
        transform.DOMoveY(offset, 1f).SetRelative().SetLoops(-1, LoopType.Yoyo);
        numberText.gameObject.SetActive(false);
    }

    public void Initialize(int number)
    {
        butterflyNumber = number;
    }
    
    
    private void OnClick()
    {
        var viewIns = UIManager.Instance.ShowSelectNumber();
        viewIns.onClick.RemoveAllListeners();
        viewIns.onClick.AddListener(OnSelect);
        button.enabled = false;
    }
    
    private void OnSelect(int index)
    {
        if (index == butterflyNumber)
        {
            numberText.text = (butterflyNumber + 1).ToString();
            numberText.gameObject.SetActive(true);
            numberBg.color = Color.green;
            onSelectRightFlower?.Invoke();
            Debug.Log("<color=green>correct</color>");
        }
        else
        {
            Debug.Log("<color=red>wrong</color>");
            onSelectWrongFlower?.Invoke();

            numberBg.color = Color.red;

        }
    }


}
