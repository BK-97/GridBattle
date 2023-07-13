using UnityEditor;
using UnityEngine;

namespace GridSystem.GridEditor
{
    [CustomEditor(typeof(GridCreator))]
    public class ButtonEditorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();

            if (GUILayout.Button("Create Grid"))
            {
                ((GridCreator)target).CreateGrid();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
