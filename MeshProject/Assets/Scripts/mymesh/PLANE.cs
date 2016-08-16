using UnityEngine;
using System.Collections;

public class PLANE : MonoBehaviour {

    Vector3 c0;
    Vector3 cX;
    Vector3 cZ;
    public Vector3 PlaneNormal;

    void UpdateV()
    {
        c0 = transform.FindChild("c0").position;
        cX = transform.FindChild("cX").position;
        cZ = transform.FindChild("cZ").position;
    }
    void Start () {
    }

    Vector3 cY;

    void Update () {
        UpdateV();
        PlaneNormal = ( Vector3.Cross(cX - c0, cZ - c0) ).normalized;
        Debug.DrawLine(  this.transform.position, (this.transform.position + PlaneNormal)*20 , Color.black);

    }
}
