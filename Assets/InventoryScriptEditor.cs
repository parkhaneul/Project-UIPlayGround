using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryUI))]
public class InventoryScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InventoryUI _iUI = (InventoryUI) target;
        
        if (GUILayout.Button("Make Inventory"))
        {
            _iUI.settings();
        }
    }
}
