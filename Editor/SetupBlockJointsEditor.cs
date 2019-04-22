using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(SetUpBlockJoints))]
public class SetupBlockJointsEditor : Editor {


    SerializedProperty _creationType;
    SerializedProperty _density;
    SerializedProperty _root;
    SerializedProperty _creationIndex;
    SerializedProperty _ctypes;
  //  int creationIndex;
   // public List<CreationType> creationTypes = new List<CreationType>();
    private void OnEnable()
    {

        _creationType = serializedObject.FindProperty("creationType");
        _density = serializedObject.FindProperty("density");
        _root = serializedObject.FindProperty("root");
        _creationIndex = serializedObject.FindProperty("creationIndex");
        _ctypes = serializedObject.FindProperty("ctypes");
        foreach (System.Type type in
     System.Reflection.Assembly.GetAssembly(typeof(CreationType)).GetTypes()
      .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(CreationType))))
        {
            _ctypes.InsertArrayElementAtIndex(_ctypes.arraySize);
            _ctypes.GetArrayElementAtIndex(_ctypes.arraySize - 1).objectReferenceValue = (CreationType)ScriptableObject.CreateInstance(type);
        }
        serializedObject.ApplyModifiedProperties();


    }
    public override void OnInspectorGUI()
    {


        serializedObject.Update();
        GameObject go = Selection.activeGameObject;

        //if (go.GetComponent<Animator>() != null && _root.objectReferenceValue == null)
        //{
        //    _root.objectReferenceValue = go.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips);


        //}
        //DrawDefaultInspector();
        List<CreationType> ctypes = new List<CreationType>();
        for (int i = 0; i < _ctypes.arraySize; i++)
        {
            ctypes.Add((CreationType)_ctypes.GetArrayElementAtIndex(i).objectReferenceValue);
        }

        _creationIndex.intValue = EditorGUILayout.Popup("Behaviors", _creationIndex.intValue, ctypes.Select(o => o.GetType().Name).ToArray());
        _creationType.objectReferenceValue = _ctypes.GetArrayElementAtIndex(_creationIndex.intValue).objectReferenceValue;
        CreateEditor(_creationType.objectReferenceValue).OnInspectorGUI();
        
        EditorGUILayout.Separator();
        _root.objectReferenceValue = EditorGUILayout.ObjectField("Root Transform",_root.objectReferenceValue,typeof(Transform),true)as Transform;
        if (_root.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("Set root transform", MessageType.Warning);
        }
        EditorGUILayout.IntSlider(_density, 1, 5);
        SetUpBlockJoints script = (SetUpBlockJoints)target;
   

        serializedObject.ApplyModifiedProperties();
        if (_root.objectReferenceValue != null)
        {
            if (GUILayout.Button("Build Object"))
            {
                script.SetUp();
            }
        }
    }


}
