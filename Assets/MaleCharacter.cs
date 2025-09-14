using System.Collections;
using Joyixir.GameManager.Utils;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaleCharacter : Singleton<MaleCharacter>
{
    [InlineEditor] public DialogueSO firstDialogue;
    [InlineEditor] public DialogueSO doNotPassRoadDialog;
    [InlineEditor] public DialogueSO prizeDialog;
    [InlineEditor] public DialogueSO trashDialogue;
    [InlineEditor] public DialogueSO trashPrize;
    [InlineEditor] public DialogueSO iceCreamDialogue;


    public VoidChannelEventSO onGetStar;
    public VoidChannelEventSO onIceCream;
    public Zone currentZone;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerPrefs.HasKey($"GM-PlayerPosX_{GMPrefs.ProfileName}"))
        {
            transform.position = GMPrefs.GetPlayerPosition();
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, GMPrefs.PlayerYRotation, transform.rotation.z));
        }
    }

    void Start()
    {
        DialogueManager.Instance.StartConversation(firstDialogue);
    }

    async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            DialogueManager.Instance.StartConversation(doNotPassRoadDialog);
        }

        if (other.gameObject.CompareTag("point"))
        {
            await StaticTweeners.AnimateDown(other.gameObject.transform);
            other.gameObject.SetActive(false);
            await DialogueManager.Instance.StartConversation(prizeDialog);
            onGetStar.RaiseEvent();
            GMPrefs.StarCount++;
        }

        if (other.gameObject.CompareTag("Trash"))
        {
            await DialogueManager.Instance.StartConversation(trashDialogue);
            await StaticTweeners.AnimateDown(other.gameObject.transform);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("garbage"))
        {
            await DialogueManager.Instance.StartConversation(trashPrize);
            onGetStar.RaiseEvent();
            GMPrefs.StarCount++;
        }

        if (other.gameObject.CompareTag("Icecream"))
        {
            await DialogueManager.Instance.StartConversation(iceCreamDialogue);
            onIceCream.RaiseEvent();
        }

        if (other.gameObject.CompareTag("Zone"))
        {
            GameManager.Instance.DisableController();
            var zone = other.gameObject.GetComponent<Zone>();
            currentZone = zone;
            GameManager.Instance.currentLocation = zone.currentLocation;
            GameManager.Instance.currentLocationName = zone.zoneName;
            UIManager.Instance.ShowChooseGameView();
        }
    }




}