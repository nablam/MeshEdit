using System.Text;
using System.Linq;
using UnityEngine;
using System.Collections;
namespace mymesh {
    public class TRI  
    {

        public int id;
        public VERT[] my3Verts;
        public VERT _V1;
        public VERT _V2;
        public VERT _V3;
        public Vector3 _myNormal;

        Vector3 _myCenter;
        

        public TRI(int ID) {
            id = ID;
            my3Verts = new VERT[3];
        }

        public TRI(int ID, VERT[]passedVi)
        {
            //Debug.Log("p1=" + passedVi[0].ToString() + "  p2=" + passedVi[1].ToString() + " p3" + passedVi[2].ToString());
            id = ID;
            my3Verts = passedVi.Take(3).ToArray();
            _V1 = my3Verts[0];
            _V2 = my3Verts[1];
            _V3 = my3Verts[2];

            CalCulate_mycenter();
           // Debug.Log("v1=" + _V1 + "  v2=" + _V2 + " v3" + _V3);
        }

        public void Draw_mynormal() {
            Debug.Log("drawing normals");
            Debug.DrawLine(_myCenter, (_myCenter + _myNormal*3), Color.red,10, false);
        }
        
        Vector3 p1;
        Vector3 p2;
        public void Draw_Plane_segment_intersection(Vector3 p2_NormalZero, Vector3 plane2RealPos)
        {
            Debug.Log("drawing Intersection with plane");
            Debug.DrawLine(_myCenter, (_myCenter + _myNormal * 3), Color.red, 10, false);
            Math3d.PlanePlaneIntersection(out p1, out p2, _myNormal, _myCenter, p2_NormalZero, plane2RealPos);
            Debug.DrawLine(p1, p2, Color.cyan, 2, false);
        }

        void CalCulate_mycenter()
        {                     
            Vector3 temp1 = ( _V2._Vv +  _V1._Vv) / 2;
            _myCenter = ( temp1 +  _V3._Vv) / 2;           
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_V1.ToString());
            sb.Append(_V2.ToString());
            sb.Append(_V3.ToString());
            return sb.ToString();
      
        }
    }
}