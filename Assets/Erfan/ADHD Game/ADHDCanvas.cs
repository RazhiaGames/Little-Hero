using System;
using Joyixir.GameManager.Utils;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class ADHDCanvas : MonoBehaviour
{
    public Image profileImage;
    public RTLTextMeshPro profileName;

    public GameObject star;
    public GameObject iceCream;
    public VoidChannelEventSO onGetStar;
    public VoidChannelEventSO onGetIceCream;
    
    private void Awake()
    {
        star.gameObject.SetActive(false);
        var starCount = GMPrefs.StarCount;
        for (int i = 0; i < starCount; i++)
        {
            InstantiateStarNoAnim();
        }
    }
    
    private void OnEnable()
    {
        onGetStar.OnEventRaised += InstantiateStar;
        onGetIceCream.OnEventRaised += EnableIceCream;
    }


    private void OnDisable()
    {
        onGetStar.OnEventRaised -= InstantiateStar;
        onGetIceCream.OnEventRaised -= EnableIceCream;
    }

    private void Start()
    {
        profileImage.sprite = CrossSceneData.INS.userImagesList[GMPrefs.ProfilePicIndex];
        profileName.text = GMPrefs.ProfileName;
    }
    
    

    
    private void InstantiateStar()
    {
        GameObject starInstance = Instantiate(star, star.transform.parent);
        starInstance.gameObject.SetActive(true);
        StaticTweeners.AnimateUp(starInstance.transform, 1f, GS.INS.CBButtonsAnimateTime, GS.INS.CBButtonsOnEase);
    }
    
    private void InstantiateStarNoAnim()
    {
        GameObject starInstance = Instantiate(star, star.transform.parent);
        starInstance.gameObject.SetActive(true);
    }
    private void EnableIceCream()
    {
        iceCream.SetActive(true);
        StaticTweeners.AnimateUp(iceCream.transform, 1f, GS.INS.CBButtonsAnimateTime, GS.INS.CBButtonsOnEase);
    }
}