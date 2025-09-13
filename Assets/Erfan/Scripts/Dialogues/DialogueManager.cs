using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    public List<DialoguePlayer> dialoguePlayers = new List<DialoguePlayer>();
    public List<DialogueSO> dialogues = new List<DialogueSO>();

    protected override void Awake()
    {
        base.Awake();
        foreach (var dialogue in dialogues)
        {
            dialogue.isConversationPlayed = false;
        }
    }


    [Button]
    public async UniTask StartConversation(DialogueSO conversation)
    {
        if (PlayerPrefs.HasKey($"{conversation.conversationKey}_{GMPrefs.ProfileName}")) return;
        if (conversation.isConversationPlayed) return;
        foreach (var dialogue in conversation.dialogues)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(dialogue.delayBefore));
            dialoguePlayers[(int)dialogue.character].ReadDialogue(dialogue.sentence);
            await UniTask.Delay(System.TimeSpan.FromSeconds(dialogue.delayAfter));
        }

        conversation.isConversationPlayed = true;
        PlayerPrefs.SetInt($"{conversation.conversationKey}_{GMPrefs.ProfileName}", 1);
    }
}