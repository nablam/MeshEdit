using System.Text;
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

        public TRI() {
            my3Verts = new VERT[3];
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