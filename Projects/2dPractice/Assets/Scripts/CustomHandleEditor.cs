using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CustomHandle))]
public class CustomHandleEditor : Editor
{

    CustomHandle mObject;
    Vector3 handlePosition;
    Vector3 lastClonePosition = new Vector3();
    List<GameObject> xclones = new List<GameObject>();
    List<GameObject> yclones = new List<GameObject>();

    private void OnEnable()
    {
        mObject = (CustomHandle)target;
        handlePosition = new Vector3(mObject.transform.position.x, mObject.transform.position.y - 0.1f, mObject.transform.position.z);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    void OnSceneGUI()
    {
        Handles.Label(handlePosition + new Vector3(0.025f, 0f, 0), "Cloner");

        handlePosition =
        Handles.PositionHandle(  //Position
        handlePosition,
        Quaternion.identity);     //Rotation
        //0.05f,                   //Size
        //new Vector3(0.1f, 0, 0), //Snap Values
        //Handles.CubeHandleCap);  //Cap Type
        //handlePosition = new Vector3(handlePosition.x, mObject.transform.position.y, mObject.transform.position.z);

        float xDifference = Mathf.Abs(handlePosition.x - mObject.transform.position.x) + mObject.cloneWidth / 2;
        float yDifference = Mathf.Abs(handlePosition.y - mObject.transform.position.y) + mObject.cloneWidth / 2;
        int numberOfXClones = (int)(xDifference / mObject.cloneWidth);
        int numberOfYClones = (int)(yDifference / mObject.cloneWidth);

        Debug.Log("Number of X Clones:" + numberOfXClones);
        if (xclones.Count < numberOfXClones)
        {
            int sign = handlePosition.x < mObject.transform.position.x ? -1 : 1;
            GameObject newClone = Instantiate(mObject.clone,
                new Vector3(mObject.transform.position.x + (mObject.cloneWidth * xclones.Count) * sign,
                            mObject.transform.position.y,
                            mObject.transform.position.z), Quaternion.identity);
            xclones.Add(newClone);
        }
        else if (xclones.Count > numberOfXClones)
        {
            DestroyImmediate(xclones[xclones.Count - 1]);
            xclones.RemoveAt(xclones.Count - 1);
        }
        Debug.Log("Number of Y Clones:" + numberOfYClones);
        if (yclones.Count < numberOfYClones)
        {
            int sign = handlePosition.y < mObject.transform.position.y ? -1 : 1;
            GameObject newClone = Instantiate(mObject.clone,
                new Vector3(mObject.transform.position.x,
                            mObject.transform.position.y + (mObject.cloneWidth * yclones.Count) * sign,
                            mObject.transform.position.z), Quaternion.identity);
            yclones.Add(newClone);
        }
        else if (yclones.Count > numberOfYClones)
        {
            DestroyImmediate(yclones[yclones.Count - 1]);
            yclones.RemoveAt(yclones.Count - 1);
        }
    }
}
