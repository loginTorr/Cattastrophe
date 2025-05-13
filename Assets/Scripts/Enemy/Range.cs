using UnityEngine;
using UnityEditor;

public class Range : MonoBehaviour{
    public bool HasTooClose = true;
    public float TooClose = 1.0001f;
    public bool HasMele = true;
    public float Mele = 1.5f;
    public bool HasCloseShooting = true;
    public float CloseShooting = 2.0f;
    public bool HasMidShooting = true;
    public float MidShooting = 2.5f;
    public bool HasFarShooting = true;
    public float FarShooting = 3.0f;
    public bool HasAgro = true;
    public float Agro = 3.5f;

    public bool CanDeagro = true;


}

[CustomEditor(typeof(Range))]
public class DrawGizmos : Editor {


    void OnSceneGUI(){
        Range range = (Range)target;

        float size = 1f;
        float snap = 0.5f;

        #region tooClose
        if (range.HasTooClose){
            Vector3 TooClosePos = new Vector3(range.transform.position.x + range.TooClose - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);
            Vector3 TooCloseDirection = Vector3.Normalize(new Vector3(TooClosePos.x - range.transform.position.x, 0, TooClosePos.z - range.transform.position.z));

            Handles.color = Color.red;

            EditorGUI.BeginChangeCheck();
            float TooClosescale = Handles.ScaleSlider(range.TooClose, TooClosePos, TooCloseDirection, range.transform.rotation, size, snap);
            Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), TooClosePos, size * 3);
            Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.TooClose, size * 3);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change TooClose Range");
                range.TooClose = TooClosescale;
            }
        }
        #endregion

        #region Mele
        if (range.HasMele){
            Vector3 MelePos = new Vector3(range.transform.position.x + range.Mele - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);
            Vector3 MeleDirection = Vector3.Normalize(new Vector3(MelePos.x - range.transform.position.x, 0, MelePos.z - range.transform.position.z));

            Handles.color = Color.yellow;

            EditorGUI.BeginChangeCheck();
            float Melescale = Handles.ScaleSlider(range.Mele, MelePos, MeleDirection, range.transform.rotation, size, snap);
            Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), MelePos, size * 3);
            Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.Mele, size * 3);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Mele Range");
                range.Mele = Melescale;
            }
        }
        #endregion

        #region CloseShooting
        if (range.HasCloseShooting){
            Vector3 CloseShootingPos = new Vector3(range.transform.position.x + range.CloseShooting - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);
            Vector3 CloseShootingDirection = Vector3.Normalize(new Vector3(CloseShootingPos.x - range.transform.position.x, 0, CloseShootingPos.z - range.transform.position.z));

            Handles.color = Color.cyan;

            EditorGUI.BeginChangeCheck();
            float CloseShootingscale = Handles.ScaleSlider(range.CloseShooting, CloseShootingPos, CloseShootingDirection, range.transform.rotation, size, snap);
            Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), CloseShootingPos, size * 3);
            Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.CloseShooting, size * 3);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change CloseShooting Range");
                range.CloseShooting = CloseShootingscale;
            }
        }
        #endregion

        #region MidShooting
        if (range.HasMidShooting){
            Vector3 MidShootingPos = new Vector3(range.transform.position.x + range.MidShooting - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);
            Vector3 MidShootingDirection = Vector3.Normalize(new Vector3(MidShootingPos.x - range.transform.position.x, 0, MidShootingPos.z - range.transform.position.z));


            Handles.color = Color.blue;

            EditorGUI.BeginChangeCheck();
            float MidShootingscale = Handles.ScaleSlider(range.MidShooting, MidShootingPos, MidShootingDirection, range.transform.rotation, size, snap);
            Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), MidShootingPos, size * 3);
            Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.MidShooting, size * 3);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change MidShooting Range");
                range.MidShooting = MidShootingscale;
            }
        }
        #endregion

        #region FarShooting
        if (range.HasFarShooting){
            Vector3 FarShootingPos = new Vector3(range.transform.position.x + range.FarShooting - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);
            Vector3 FarShootingDirection = Vector3.Normalize(new Vector3(FarShootingPos.x - range.transform.position.x, 0, FarShootingPos.z - range.transform.position.z));

            Handles.color = Color.white;

            EditorGUI.BeginChangeCheck();
            float FarShootingscale = Handles.ScaleSlider(range.FarShooting, FarShootingPos, FarShootingDirection, range.transform.rotation, size, snap);
            Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), FarShootingPos, size * 3);
            Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.FarShooting, size * 3);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change FarShooting Range");
                range.FarShooting = FarShootingscale;
            }
        }
        #endregion

        #region Agro
        if (range.HasAgro){
            Vector3 AgroPos = new Vector3(range.transform.position.x + range.Agro - size, range.transform.position.y - range.transform.localScale.y, range.transform.position.z);
            Vector3 AgroDirection = Vector3.Normalize(new Vector3(AgroPos.x - range.transform.position.x, 0, AgroPos.z - range.transform.position.z));

            Handles.color = Color.black;

            EditorGUI.BeginChangeCheck();
            float Agroscale = Handles.ScaleSlider(range.Agro, AgroPos, AgroDirection, range.transform.rotation, size, snap);
            Handles.DrawLine(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), AgroPos, size * 3);
            Handles.DrawWireDisc(new Vector3(range.transform.position.x, range.transform.position.y - range.transform.localScale.y, range.transform.position.z), Vector3.up, range.Agro, size * 3);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Agro Range");
                range.Agro = Agroscale;
            }
        }
        #endregion
    }
}
