using UnityEngine;
using System.Collections;
namespace mymesh
{
    public class VERT
    {

        public int _my_TriIndex;
        public int _my_VertsIndex; // this is the value at _triangles[_my_TriIndex]  and the indev of _Vert["this"] 
        public Vector3 _my_VertsValue;

        public VERT(int triNdx, int vertNdx, Vector3 vertVal)
        {
            _my_TriIndex = triNdx;
            _my_VertsIndex = vertNdx;
            _my_VertsValue = vertVal;
        }

        public override string ToString()
        {
            return "{v_triNdx=" + _my_TriIndex + "|" + "Vndx=" + _my_VertsIndex + "|[" + _my_VertsValue +  "]}" ;
        }
    }
}