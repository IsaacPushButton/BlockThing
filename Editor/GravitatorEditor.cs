using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Gravitator))]
public class GravitatorEditor : Editor {

    SerializedProperty _globalScale;
    SerializedProperty _behaviour;
    SerializedProperty _behaviorIndex;
  //  public List<LimbBehavior>
   SerializedProperty _behaviors;// = new List<LimbBehavior>();

    private void OnEnable()
    {
        _behaviour = serializedObject.FindProperty("behavior");
        _globalScale = serializedObject.FindProperty("globalScale");
        _behaviorIndex = serializedObject.FindProperty("behaviorIndex");
        _behaviors = serializedObject.FindProperty("behaviors");
        if (_behaviors.arraySize == 0)
        {
            foreach (System.Type type in
               System.Reflection.Assembly.GetAssembly(typeof(LimbBehavior)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(LimbBehavior))))
            {
               // _behaviors.arraySize++;
                _behaviors.InsertArrayElementAtIndex(_behaviors.arraySize);
                _behaviors.GetArrayElementAtIndex(_behaviors.arraySize - 1).objectReferenceValue = (LimbBehavior)ScriptableObject.CreateInstance(type);
               

            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

       // _globalScale.floatValue = 
            EditorGUILayout.Slider(_globalScale, .01f, 5f);
        List<LimbBehavior> behaviors = new List<LimbBehavior>();
        for(int i = 0; i < _behaviors.arraySize; i++)
        {
            behaviors.Add((LimbBehavior)_behaviors.GetArrayElementAtIndex(i).objectReferenceValue);
        }
        _behaviorIndex.intValue = EditorGUILayout.Popup("Behaviors", _behaviorIndex.intValue, behaviors.Select(o=>o.GetType().Name).ToArray());

        _behaviour.objectReferenceValue = _behaviors.GetArrayElementAtIndex(_behaviorIndex.intValue).objectReferenceValue;
        // EditorGUILayout.PropertyField(_behaviour);
        Editor nested = CreateEditor(_behaviour.objectReferenceValue);
        nested.serializedObject.Update();
        nested.OnInspectorGUI();
        nested.serializedObject.ApplyModifiedProperties();
       serializedObject.ApplyModifiedProperties();
    }
}
