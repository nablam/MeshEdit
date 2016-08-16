using System.Linq;
using UnityEngine;
using System.Collections;
namespace meshsplit
{
    public class Triangle 
    {
        //###########################
        //
        //    Triangle Structure
        //       
        //            0
        //          /   \
        //       x_20    x_01
        //        /       \
        //      2---x_12---1
        //
        //###########################

        public TriVert[] TVarra;
        public int ID;
        public char objLR;
        public Vector3 x_01; public bool seg01_crossed;
        public Vector3 x_12; public bool seg12_crossed;
        public Vector3 x_20; public bool seg20_crossed;
        public bool IsOnCutPath;

        Vector3 _myCenter;
        GameObject __P1;
        public Vector3 _myNormal;

        public TriVert newHEAD;

        public Triangle(int id, TriVert[] passedTriVertarra, GameObject p1) {
            TVarra = new TriVert[3];
            TVarra[0] = passedTriVertarra[0];
            TVarra[1] = passedTriVertarra[1];
            TVarra[2] = passedTriVertarra[2];
            ID = id;
            objLR = 'L';
            x_01 = Vector3.zero;
            x_12 = Vector3.zero;
            x_20 = Vector3.zero;
            Calculate_myCenter();
            __P1 = p1;
            seg01_crossed = false;
            seg12_crossed = false;
            seg20_crossed = false;
            IsOnCutPath = false;
        }

        void Calculate_myCenter() {
            Vector3 temp1 = (TVarra[0].VV + TVarra[1].VV) / 2;
            _myCenter = (temp1 + TVarra[2].VV) / 2;
        }

        int Generic_VaVb_PlaneIntersect(out Vector3 I, Vector3 va, Vector3 vb)
        {
            Vector3 u = vb - va;
            Vector3 w = va - __P1.transform.position;
            float D = Vector3.Dot(__P1.GetComponent<PLANE>().PlaneNormal, u);
            float N = -Vector3.Dot(__P1.GetComponent<PLANE>().PlaneNormal, w);

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

        void Check_intersect01()
        {
            Vector3 va = TVarra[0].VV;
            Vector3 vb=  TVarra[1].VV;
            if (Generic_VaVb_PlaneIntersect(out x_01, va, vb) == 1)
            {
                seg01_crossed = true;
                IsOnCutPath = true;
            }  
        }
        void Check_intersect12()
        {
            Vector3 va = TVarra[1].VV;
            Vector3 vb = TVarra[2].VV;
            if (Generic_VaVb_PlaneIntersect(out x_12, va, vb) == 1)
            {
                seg12_crossed = true;
                IsOnCutPath = true;
            }
    
        }
        void Check_intersect20()
        {
            Vector3 va = TVarra[2].VV;
            Vector3 vb = TVarra[0].VV;
            if (Generic_VaVb_PlaneIntersect(out x_20, va, vb) == 1)
            {
                seg20_crossed = true;
                IsOnCutPath = true;
            }
        
        }

        public void CheckAllSegmentsForIntersects() {
            seg01_crossed = false;
            seg12_crossed = false;
            seg20_crossed = false;
            IsOnCutPath = false;
            Check_intersect01();
            Check_intersect12();
            Check_intersect20();
        }

        void FindNewHEad() {
 
            if (seg20_crossed && seg01_crossed && !seg12_crossed) { newHEAD = TVarra[0]; }
            else
            if (seg01_crossed && seg12_crossed && !seg20_crossed) { newHEAD = TVarra[1]; }
            else
            if (seg20_crossed && seg12_crossed && !seg01_crossed) { newHEAD = TVarra[2]; }
        }

    }
}
