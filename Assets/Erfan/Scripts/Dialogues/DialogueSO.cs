using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Dialogue", menuName = "Erfan/Dialogue", order = 1)]
public class DialogueSO : ScriptableObject
{
    public List<Dialogue> dialogues = new List<Dialogue>();
    public bool isConversationPlayed;
    public string conversationKey;
}

[Serializable]
public class Dialogue
{
    public Character character;
    public float delayBefore;
    public string sentence;
    [NonSerialized] public string farsi;
    public float delayAfter;
}


public enum Character
{
    boy,
    girl,
    prizeTrashGuy,
    trashcan,
    IceCreamGuy
}


#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSO))]
public class DialogueSOEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        DialogueSO stringTarget = (DialogueSO)target;

        // Draw default inspector for other fields (like isConversationPlayed)
        DrawDefaultInspector();

        GUILayout.Space(10);
        GUILayout.Label("Localized Sentences (Editable)", EditorStyles.boldLabel);

        for (int i = 0; i < stringTarget.dialogues.Count; i++)
        {
            Dialogue dialogue = stringTarget.dialogues[i];

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Character: {dialogue.character}", EditorStyles.boldLabel);


            // Show and edit the localized sentence
            string fixedText = StaticUtils.GetFixedRtlText(dialogue.sentence);
            dialogue.farsi = EditorGUILayout.TextArea(fixedText);
            //
            // string editedText = EditorGUILayout.TextArea(fixedText);
            // dialogue.sentence = StaticUtils.GetUnfixedRtlText(editedText); // Optional: reverse-fix if needed


            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
        }

        // Mark the object as dirty so Unity saves the changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(stringTarget);
        }
    }
}
#endif