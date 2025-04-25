using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

public class Range : MonoBehaviour{
    public float TooClose = 1.0f;
    public float Mele = 1.0f;
    public float CloseShooting = 1.0f;
    public float MidShooting = 1.0f;
    public float FarShooting = 1.0f;
    public float Agro = 1.0f;

    public void Update()
    {
        
    }

}

[CustomEditor(typeof(Range))]
public class DrawGizmos : Editor {


    void OnSceneGUI(){
        Range range = (Range)target;

        float size = 1f;
        float snap = 0.5f;

        #region tooClose
        Vector3 TooClosePos = new Vector3(range.transform.position.x + range.TooClose - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);

        Handles.color = Color.red;

        EditorGUI.BeginChangeCheck();
        float scale = Handles.ScaleSlider(range.TooClose, TooClosePos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), TooClosePos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.TooClose, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change TooClose Range");
            range.TooClose = scale;
            range.Update();
        }
        #endregion
    }
}
