using UnityEngine;
using System.Collections;

public class SegmentDrawing : MonoBehaviour {

    public GameObject P1;
    public GameObject cursor;

    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;

    Vector3 va;
    Vector3 vb;
    Vector3 vc;
    Vector3 vd;

    Transform secondpoint;

    void updatev() {
        va = A.transform.position;
        vb = B.transform.position;
        vc = C.transform.position;
        vd = D.transform.position;
    }
	void Start () {

        updatev();
    }


    Vector3 endOfLine;
    Vector3 BOXPOINT;
	void Update () {
        // Vector3 pnorm = P1.GetComponent<PLANE>().PlaneNormal;
        //Draw the segment
        // Debug.DrawLine(transform.position, secondpoint.position, Color.red);

        //  Math3d.LinePlaneIntersection(out endOfLine, transform.position, secondpoint.position, pnorm, P1.transform.position);
        // Debug.DrawLine(Vector3.zero, endOfLine, Color.green);
        // Debug.DrawLine(Vector3.zero, interpoint, Color.yellow, 0.2f , false);
        //   Math3d.LineLineIntersection(out BOXPOINT, transform.position, secondpoint.position, Vector3.zero, endOfLine );
        //   Debug.DrawLine(Vector3.zero, BOXPOINT, Color.green);
        //  cursor.transform.position = endOfLine;
        //Math3d.LineLineIntersection(out BOXPOINT, other1.position, other2.position, transform.position, secondpoint.position);
        //Debug.DrawLine(Vector3.zero, BOXPOINT, Color.green);
        updatev();
        DrawAB();
        DrawCD();

        print(intersect3D_SegmentPlane(out BOXPOINT));
       // Math3d.LineLineIntersection(out BOXPOINT, va,va+vb,vc,vc+vd);
        cursor.transform.position = BOXPOINT;
    }

    void DrawAB() { Debug.DrawLine(va, vb, Color.red); }
    void DrawCD() { Debug.DrawLine(vc, vd, Color.red); }


    int intersect3D_SegmentPlane( out Vector3 I)
    {
        Vector3 u = vb-  va;
        Vector3 w = va - P1.transform.position;

        float D = Vector3.Dot(P1.GetComponent<PLANE>().PlaneNormal, u);
        float N = -Vector3.Dot(P1.GetComponent<PLANE>().PlaneNormal, w);

        if (Mathf.Abs(D) < 0.0001f)
        {           // segment is parallel to plane
            if (N == 0)
            {
                I = Vector3.zero;            // segment lies in plane
                return 2;
            }
            else
            {
                I = Vector3.zero;
                return 0;
            }                   // no intersection
        }
        // they are not parallel
        // compute intersect param
        float sI = N / D;
        if (sI < 0 || sI > 1)
        {
            I = Vector3.zero;
            return 0;
        }// no intersection

        I = va + sI * u;                  // compute segment intersect point
        return 1;
    }
}
