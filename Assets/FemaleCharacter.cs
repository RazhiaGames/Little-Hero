using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using TMPro;
using UnityEngine;

public class FemaleCharacter : MonoBehaviour
{
        // public RTLTextMeshPro GirlDialog;
        // public RTLTextMeshPro PlayerDialog;
        public DialogueSO girlStartDialogue;
        public DialogueSO girlEndDialogue;
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DialogueManager.Instance.StartConversation(girlStartDialogue);
            }
        }

        // IEnumerator UpdateTextCoroutine()
        // {
        //     GirlDialog.text = "سلام خوبی؟";
        //     yield return new WaitForSeconds(1);
        //     PlayerDialog.text = "سلام ممنون";
        //     yield return new WaitForSeconds(1);
        //     GirlDialog.text = "تو باید از خط کشی عابر پیاده رد شی!";
        //     yield return new WaitForSeconds(2);
        //     PlayerDialog.text = "ممنون از راهنماییت";
        //     yield return new WaitForSeconds(2);
        //     PlayerDialog.text = "";
        //     GirlDialog.text = "";
        // }
        //
        // IEnumerator UpdateByeTextCoroutine()
        // {
        //     PlayerDialog.text = "خدا نگهدار";
        //     GirlDialog.text = "خدا حافظ"; 
        //     yield return new WaitForSeconds(2);
        //     PlayerDialog.text = "";
        //     GirlDialog.text = "";
        // }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                DialogueManager.Instance.StartConversation(girlEndDialogue);
            }
        }
    }

