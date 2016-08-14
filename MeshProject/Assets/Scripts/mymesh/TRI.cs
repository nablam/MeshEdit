using System.Text;
using System.Linq;
using UnityEngine;
using System.Collections;
namespace mymesh {
    public class TRI  
    {

        public int id;
        public VERT[] my3Verts;
        public VERT _V0;
        public VERT _V1;
        public VERT _V2;
        public Vector3 _myNormal;

   

        public Vector3 x_01;
        public Vector3 x_02;
        public Vector3 x_12;


        Vector3 _myCenter;
        

        public TRI(int ID) {
            id = ID;
            my3Verts = new VERT[3];
            InitIntersects();
        }

        public TRI(int ID, VERT[]passedVi)
        {
            //Debug.Log("p1=" + passedVi[0].ToString() + "  p2=" + passedVi[1].ToString() + " p3" + passedVi[2].ToString());
            id = ID;
            my3Verts = passedVi.Take(3).ToArray();
            _V0 = my3Verts[0];
            _V1 = my3Verts[1];
            _V2 = my3Verts[2];
            InitIntersects();

            CalCulate_mycenter();
           // Debug.Log("v1=" + _V1 + "  v2=" + _V2 + " v3" + _V3);
        }
        //just to have something in the intersetct
        void InitIntersects() { x_01 = _V0._Vv; x_02 = _V1._Vv; x_12 = _V2._Vv; }

        public void Draw_mynormal() {
            Debug.Log("drawing normals");
            Debug.DrawLine(_myCenter, (_myCenter + _myNormal*3), Color.red,10, false);
        }
        
        Vector3 p1;
        Vector3 p2;
        public void Draw_Plane_segment_intersection(Vector3 p2_NormalZero, Vector3 plane2RealPos)
        {
           // DoAllIntersects_aandDraw(p2_NormalZero, plane2RealPos);

          //  Debug.Log("drawing Intersection with plane");
          //  Debug.DrawLine(_myCenter, (_myCenter + _myNormal * 3), Color.red, 10, false);
          // Math3d.PlanePlaneIntersection(out p1, out p2, _myNormal, _myCenter, p2_NormalZero, plane2RealPos);
          // Debug.DrawLine(p1, p2, Color.cyan, 2, false);
        }

        public  void DoAllIntersects_aandDraw(Vector3 p2_NormalZero, GameObject P1) {
            DoIntersect_01(_V0._Vv, _V1._Vv, p2_NormalZero, P1);
            DoIntersect_02(_V0._Vv, _V2._Vv, p2_NormalZero, P1);
            DoIntersect_12(_V1._Vv, _V2._Vv, p2_NormalZero, P1);

            //Debug.DrawLine(P1.transform.position, x_01, Color.red);
            //Debug.DrawLine(P1.transform.position, x_02, Color.green);
            //Debug.DrawLine(P1.transform.position, x_12, Color.blue   );
        }

        void DoIntersect_01(Vector3 va, Vector3 vb, Vector3 p2_NormalZero, GameObject P1) {
            if (DoINtersect_VaVb_Plane(out x_01,  va,  vb,  p2_NormalZero,  P1) == 1) {
                Debug.Log("yes 01 of TYIid" + this.id + " intersects");
                Debug.DrawLine(P1.transform.position, x_01, Color.red,2);
            }
        }
        void DoIntersect_02(Vector3 va, Vector3 vb, Vector3 p2_NormalZero, GameObject P1)
        {
            if (DoINtersect_VaVb_Plane(out x_02, va, vb, p2_NormalZero, P1) == 1)
            {
                Debug.Log("yes 02 of TYIid" + this.id + " intersects @ +"+x_02);
                Debug.DrawLine(P1.transform.position, x_02, Color.green,2);
            }
        }
        void DoIntersect_12(Vector3 va, Vector3 vb, Vector3 p2_NormalZero, GameObject P1)
        {
            if (DoINtersect_VaVb_Plane(out x_12, va, vb, p2_NormalZero, P1) == 1)
            {
                Debug.Log("yes 12 of TYIid" + this.id + " intersects");
                Debug.DrawLine(P1.transform.position, x_12, Color.blue,2);
            }
        }


        int DoINtersect_VaVb_Plane(out Vector3 I, Vector3 va, Vector3 vb, Vector3 p2_NormalZero, GameObject P1) {
                Vector3 u = vb - va;
                Vector3 w = va - P1.transform.position;
                float D = Vector3.Dot(P1.GetComponent<PLANE>().PlaneNormal, u);
                float N = -Vector3.Dot(P1.GetComponent<PLANE>().PlaneNormal, w);

                if (Mathf.Abs(D) < 0.0001f)
                {           // segment is parallel to plane
                    if (N == 0)
                    {
                        I = Vector3.zero;            
                        return 2; // segment lies in plane
                }
                    else
                    {
                        I = Vector3.zero;
                        return 0;//no intersect
                    } 
                }
 
                float sI = N / D;
                if (sI < 0 || sI > 1)
                {
                    I = Vector3.zero;
                    return 0;
                }// no intersection
                I = va + sI * u;      
                return 1;          
        }

        void CalCulate_mycenter()
        {                     
            Vector3 temp1 = ( _V1._Vv +  _V0._Vv) / 2;
            _myCenter = ( temp1 +  _V2._Vv) / 2;           
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_V0.ToString());
            sb.Append(_V1.ToString());
            sb.Append(_V2.ToString());
            return sb.ToString();
      
        }



    }
}