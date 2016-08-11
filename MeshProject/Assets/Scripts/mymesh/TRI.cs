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

           // Debug.Log("v1=" + _V1 + "  v2=" + _V2 + " v3" + _V3);
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