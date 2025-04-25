using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

public class Range : MonoBehaviour{
    public float TooClose = 1.0f;
    public float Mele = 1.5f;
    public float CloseShooting = 2.0f;
    public float MidShooting = 2.5f;
    public float FarShooting = 3.0f;
    public float Agro = 3.5f;


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
        float TooClosescale = Handles.ScaleSlider(range.TooClose, TooClosePos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), TooClosePos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.TooClose, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change TooClose Range");
            range.TooClose = TooClosescale;
        }
        #endregion

        #region Mele
        Vector3 MelePos = new Vector3(range.transform.position.x + range.Mele - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);

        Handles.color = Color.yellow;

        EditorGUI.BeginChangeCheck();
        float Melescale = Handles.ScaleSlider(range.Mele, MelePos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), MelePos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.Mele, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Mele Range");
            range.Mele = Melescale;
        }
        #endregion

        #region CloseShooting
        Vector3 CloseShootingPos = new Vector3(range.transform.position.x + range.CloseShooting - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);

        Handles.color = Color.cyan;

        EditorGUI.BeginChangeCheck();
        float CloseShootingscale = Handles.ScaleSlider(range.CloseShooting, CloseShootingPos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), CloseShootingPos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.CloseShooting, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change CloseShooting Range");
            range.CloseShooting = CloseShootingscale;
        }
        #endregion

        #region MidShooting
        Vector3 MidShootingPos = new Vector3(range.transform.position.x + range.MidShooting - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);

        Handles.color = Color.blue;

        EditorGUI.BeginChangeCheck();
        float MidShootingscale = Handles.ScaleSlider(range.MidShooting, MidShootingPos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), MidShootingPos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.MidShooting, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change MidShooting Range");
            range.MidShooting = MidShootingscale;
        }
        #endregion

        #region FarShooting
        Vector3 FarShootingPos = new Vector3(range.transform.position.x + range.FarShooting - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);

        Handles.color = Color.white;

        EditorGUI.BeginChangeCheck();
        float FarShootingscale = Handles.ScaleSlider(range.FarShooting, FarShootingPos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), FarShootingPos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.FarShooting, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change FarShooting Range");
            range.FarShooting = FarShootingscale;
        }
        #endregion

        #region Agro
        Vector3 AgroPos = new Vector3(range.transform.position.x + range.Agro - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);

        Handles.color = Color.black;

        EditorGUI.BeginChangeCheck();
        float Agroscale = Handles.ScaleSlider(range.Agro, AgroPos, range.transform.right, range.transform.rotation, size, snap);
        Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), AgroPos, size * 3);
        Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.Agro, size * 3);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Agro Range");
            range.Agro = Agroscale;
        }
        #endregion
    }
}
