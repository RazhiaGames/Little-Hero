using System;
using Cysharp.Threading.Tasks;

using RTLTMPro;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    public RTLTextMeshPro dialogueText;
    private void Awake()
    {
        // transform.localScale = Vector3.zero;
    }

    public async UniTask ReadDialogue(string sentence)
    {
        dialogueText.text = sentence;
    }
}