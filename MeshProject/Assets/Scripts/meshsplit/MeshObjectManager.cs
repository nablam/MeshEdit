using System.Linq;
using UnityEngine;
using System.Collections;
namespace meshsplit
{

    public class MeshObjectManager : MonoBehaviour
    {

        public GameObject P1;

        PLANE pscript;
        Mesh _mesh;
        TriangleManager tm;
        public void OnGetVerts()
        {
            Debug.Log("click1 intersets");
            tm.Calc_Intersects();
            nabDrawHelper.DrawLine(P1, tm.Get_TRILIST());
        }

        public void OnShowLEftVerts() {
            Debug.Log("click find lef verts ");
            nabDrawHelper.DrawLine(P1,  tm.find_all_RightOfPlane(), Color.blue);
          
        }

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            tm = new TriangleManager(_mesh, P1); 
            pscript = P1.GetComponent<PLANE>();
        }



       
    }
}
