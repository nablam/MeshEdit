using UnityEngine;
using System.Collections;
namespace meshsplit {
    public class TriVert 
    {
        public Vector3 VV;
        public char LR;
        public int HashCode;

        int Feild = 2000000000;

        public TriVert(Vector3 vv, char lr) { VV = vv; LR = lr; HashCode =this.GetHashCode(); }

        public override int GetHashCode()
        {
            int v1 = 0;
            if (VV.x > -1) v1 =(int) VV.x*2+ (int)VV.z * 6;
            else
                v1 = (int)VV.x*3 + (int)VV.z * 7;

            int v2 = 0;
            if (VV.y > -1)
                v2 = (int)VV.y * 2 + (int)VV.x * 8;
            else
                v2 = (int)VV.y * 3 + (int)VV.x * 9;

            int v3 = 0;
            if(VV.z>-1)
                v3= (int)VV.z * 1 + (int)VV.x * 4;
            else
                v3 = (int)VV.z * 11 + (int)VV.x * 5;

            return  (Mathf.Abs(v1*v2*v3)*(int)LR)% Feild;
        }

        public override string ToString()
        {
            return "{v=" + VV + "|" + LR +"}";
        }
    }
}
