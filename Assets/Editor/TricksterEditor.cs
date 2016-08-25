using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Trickster))]
public class TricksterEditor : Editor {

    public override void OnInspectorGUI()
    {
        Trickster trickster = (Trickster)target;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.HelpBox("Check if the object should be included in the selection.\nLeave un-checked if only the Children should be included.", MessageType.Info);
        EditorGUILayout.LabelField("Include self?");
        trickster.m_includeSelf = EditorGUILayout.Toggle(trickster.m_includeSelf);

        EditorGUILayout.LabelField("Select way of activation for the object");
        trickster.m_activationMode = (ActivationMode)EditorGUILayout.EnumPopup(trickster.m_activationMode);

        if (trickster.m_activationMode == ActivationMode.external)
        {
            EditorGUILayout.HelpBox("Activate the object from another script or animation using the ActivateExternally() method of the Trickster.cs class.", MessageType.Info);
        }

        if (trickster.m_activationMode == ActivationMode.turnBased)
        {
            EditorGUILayout.HelpBox("Select how many turns it takes to activate the object:", MessageType.Info);
            trickster.m_roundsToActivate = EditorGUILayout.IntField(trickster.m_roundsToActivate);
        }

        if (trickster.m_activationMode == ActivationMode.timeBased)
        {
            EditorGUILayout.HelpBox("Select how many seconds it takes to activate the object:", MessageType.Info);
            trickster.m_secondsToActivate = EditorGUILayout.FloatField(trickster.m_secondsToActivate);
        }

        EditorGUILayout.LabelField("Select what happens when the object has finished changing");
        trickster.m_expiringOption = (ExpireOptions)EditorGUILayout.EnumPopup(trickster.m_expiringOption);

        if(trickster.m_expiringOption == ExpireOptions.Destroy)
        {
            EditorGUILayout.HelpBox("Drag here a list of objects you would like to broadcast a message to, if this object expires.", MessageType.Info);

            SerializedProperty tps = serializedObject.FindProperty("m_ObjectsToNotifyOnExpire");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(tps, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            if (trickster.m_ObjectsToNotifyOnExpire.Length != 0)
            {
                EditorGUILayout.HelpBox("Insert below a message you would like to broadcast to the objects.\nIf left empty, no message will be broadcasted.", MessageType.Info);
                trickster.m_messageToSend = EditorGUILayout.TextField(trickster.m_messageToSend);
            }
        }

        EditorGUILayout.EndVertical();

        serializedObject.Update();

        if (trickster.m_objects.Length != 0)
        {
            EditorGUILayout.HelpBox("Below is a list of the objects that will change during gameplay.", MessageType.Info);
            SerializedProperty tps = serializedObject.FindProperty("m_objects");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(tps, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
        //EditorGUILayout.("Objects to change", trickster.m_ObjectsToNotifyOnExpire);
        //base.OnInspectorGUI();
    }
}
