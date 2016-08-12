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
        // a good test for normal , try getting cross of this and CZ// Debug.DrawLine(c0, (cX + Vector3.right) * 2, Color.red);

       // Debug.DrawLine(c0, cX, Color.red);
       // Debug.DrawLine(c0-c0, cX-c0, Color.red);
       // Debug.DrawLine(c0, cZ, Color.blue);
       // Debug.DrawLine(c0 - c0, cZ - c0, Color.blue);
        cY  = Vector3.Cross(cX - c0, cZ - c0);
      //  Debug.DrawLine(c0, cY, Color.green);
       // Debug.DrawLine(c0 - c0, cY - c0, Color.green);

        PlaneNormal = cY - c0;
 

    }
}
