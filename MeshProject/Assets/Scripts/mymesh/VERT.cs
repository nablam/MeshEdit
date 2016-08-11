using UnityEngine;
using System.Collections;
namespace mymesh
{
    public class VERT
    {

        public int _Ti;
        public int _Tv;
        public int _Vi; // this is the value at _triangles[_my_TriIndex]  and the indev of _Vert["this"] 
        public Vector3 _Vv;

        public VERT(int triNdx, int triV, int vertNdx, Vector3 vertVal)
        {
            _Ti = triNdx;
            _Tv = triV;
            _Vi = vertNdx;
            _Vv = vertVal;
        }

        public override string ToString()
        {
            return "{v_triNdx=" + _Ti + "|"+_Tv + "Vndx=" + _Vi + "|[" + _Vv +  "]}" ;
        }
    }
}