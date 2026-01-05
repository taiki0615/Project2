using UnityEditor;
using UnityEngine;

public class InspectorToJson
{
    [MenuItem("Tools/Dump Selected Object")]
    static void DumpSelected()
    {
        var obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.Log("何も選択されていません");
            return;
        }

        Debug.Log(obj.name);
    }
}
