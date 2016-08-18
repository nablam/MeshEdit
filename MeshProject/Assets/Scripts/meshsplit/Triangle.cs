using System.Linq;
using UnityEngine;
using System.Collections;
using System.Text;

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
        //
        //
        //  a type_0 tri has 0 for head
        //  a type_1 tri has 1 for head
        //  a type_2 tri has 2 for head
        //
        //###########################
        //

        public TriVert[] TVarra;
        public int ID;
        public char obj_L_R_OP_X; //Left right, onpath, X defalt 
        public Vector3 x_01; public bool seg01_crossed;
        public Vector3 x_12; public bool seg12_crossed;
        public Vector3 x_20; public bool seg20_crossed;
        public bool IsOnCutPathBOOL;

        public Triangle T0=null;
        public Triangle T1=null;
        public Triangle T2=null;

        Vector3 _myCenter;
        GameObject __P1;
        public Vector3 _myNormal;

        public TriVert newHEAD;
        public bool thisTriangleis_RIGHT;
        Vector3 __p1Normal;
        bool HAS_all_3_subTRI;

        public Triangle(int id, TriVert[] passedTriVertarra, GameObject p1) {
            TVarra = new TriVert[3];
            TVarra[0] = passedTriVertarra[0];
            TVarra[1] = passedTriVertarra[1];
            TVarra[2] = passedTriVertarra[2];
            ID = id;
            obj_L_R_OP_X = 'n';
            x_01 = Vector3.zero;
            x_12 = Vector3.zero;
            x_20 = Vector3.zero;
            Calculate_myCenter();
            __P1 = p1;
            //__p1Normal = __P1.GetComponent<PLANE>().PlaneNormal;
            seg01_crossed = false;
            seg12_crossed = false;
            seg20_crossed = false;
            IsOnCutPathBOOL = false;
            thisTriangleis_RIGHT = false;
            HAS_all_3_subTRI = false;
        }


        public Triangle( TriVert[] passedTriVertarra, GameObject p1, char _l_r_O)
        {
            TVarra = new TriVert[3];
            TVarra[0] = passedTriVertarra[0];
            TVarra[1] = passedTriVertarra[1];
            TVarra[2] = passedTriVertarra[2];
            ID = 0;
            obj_L_R_OP_X = _l_r_O;
            x_01 = Vector3.zero;
            x_12 = Vector3.zero;
            x_20 = Vector3.zero;
            Calculate_myCenter();
            __P1 = p1;
           // __p1Normal = __P1.GetComponent<PLANE>().PlaneNormal;
            seg01_crossed = false;
            seg12_crossed = false;
            seg20_crossed = false;
            IsOnCutPathBOOL = false;
            thisTriangleis_RIGHT = false;
            HAS_all_3_subTRI = false;

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
                IsOnCutPathBOOL = true;
                obj_L_R_OP_X = 'O';
            }  
        }
        void Check_intersect12()
        {
            Vector3 va = TVarra[1].VV;
            Vector3 vb = TVarra[2].VV;
            if (Generic_VaVb_PlaneIntersect(out x_12, va, vb) == 1)
            {
                seg12_crossed = true;
                IsOnCutPathBOOL = true;
                obj_L_R_OP_X = 'O';
            }
    
        }
        void Check_intersect20()
        {
            Vector3 va = TVarra[2].VV;
            Vector3 vb = TVarra[0].VV;
            if (Generic_VaVb_PlaneIntersect(out x_20, va, vb) == 1)
            {
                seg20_crossed = true;
                IsOnCutPathBOOL = true;
                obj_L_R_OP_X = 'O';
            }
        
        }
        public void SetIntersects() {
            //First I want to set all v LR
            SetOWN_TV_LR();
            Debug.Log("TRI :" + TVarra[0].LRO + " "+ TVarra[1].LRO + " "+TVarra[2].LRO + " ");
            NEW_SET_MY_OBJlro();
            //setOWN_OBJ_LR();
           // Debug.Log("OBJset :" + obj_L_R_OP_X+ " ");
            seg01_crossed = false;
            seg12_crossed = false;
            seg20_crossed = false;
            IsOnCutPathBOOL = false;
            Check_intersect01();
            Check_intersect12();
            Check_intersect20();
        }

        void SetOWN_TV_LR() {
            __p1Normal = __P1.GetComponent<PLANE>().PlaneNormal;
            foreach (TriVert tv in TVarra)
            {
               
                if (Vector3.Dot(tv.VV - __P1.transform.position, __p1Normal) > 0.0f) { tv.LRO = 'R';  }
                else
                 if (Vector3.Dot(tv.VV - __P1.transform.position, __p1Normal) < 0.0f) { tv.LRO = 'L'; }
                else
                { tv.LRO = 'O'; } //Odoes getset ... YAY
              //  Debug.Log("TVLRO-------"+tv.LRO+ " " + __P1.transform.position + " " + __p1Normal);
            }
        }

        void setOWN_OBJ_LR() {
            if (ALLmyVertsAreOntheRigh()) { obj_L_R_OP_X = 'R'; }
            else
            if(ALLmyVertsAreOntheLeft()) { obj_L_R_OP_X = 'L'; }
            else
                obj_L_R_OP_X = 'O';
        }

        int GetTriType() {
            if (seg20_crossed && seg01_crossed && !seg12_crossed) { HAS_all_3_subTRI = true; return 0; }  //HEad 0
            else
            if (seg01_crossed && seg12_crossed && !seg20_crossed) { HAS_all_3_subTRI = true; return 1; } //HEad 1
            else
            if (seg12_crossed && seg20_crossed && !seg01_crossed) { HAS_all_3_subTRI = true; return 2; } //HEad 2
            else
             if (seg12_crossed && !seg01_crossed && !seg20_crossed) return 3; //HEad 0
            else
             if (seg20_crossed && !seg12_crossed && !seg01_crossed) return 4; //HEad 1
            else
             if (seg01_crossed && !seg20_crossed && !seg12_crossed) return 5; //HEad 2 
            else
                return -1;  //nohead
            

        }
        public bool ALLmyVertsAreOntheRigh() {
            if (   (TVarra[0].LRO == TVarra[1].LRO) && (TVarra[0].LRO == TVarra[2].LRO) && (TVarra[2].LRO == 'R') ) return true;
            else
                return false;
        }
        public bool ALLmyVertsAreOntheLeft()
        {
            if ( TVarra[0].LRO == 'L' && TVarra[1].LRO == 'L' && TVarra[2].LRO == 'L') return true;
            else
                return false;
        }

        public void NEW_SET_MY_OBJlro()
        {
            int cnt_tvR = TVarra.Where(e => e.LRO == 'R').Count();
            int cnt_tvL = TVarra.Where(e => e.LRO == 'L').Count();
            int cnt_tvO = TVarra.Where(e => e.LRO == 'O').Count();
            if (cnt_tvR > cnt_tvL) obj_L_R_OP_X = 'R';
            else
                if (cnt_tvL > cnt_tvR) obj_L_R_OP_X = 'L';

           // Debug.Log("cnt_tvR:" + cnt_tvR + "cnt_tvL:" + cnt_tvL + "cnt_tvO" + cnt_tvO);
        }


        public bool ALLmyVertsAreOntheCutLine()
        {
            if (!ALLmyVertsAreOntheRigh() && !ALLmyVertsAreOntheLeft()) return true;
            else
                return false;
        }

    

        //SUB TRIANGLES T0 T1 T2
        //###########################
        //
        //    Sub Triangles
        //       
        //    T0= Single TRi  is   0(aka newHEad)  x_01, x_20  
        //    T1= TWINtri_1   is   1(aka next cloxkwise head) , 2, x_20
        //    T2= TWINtri_2   is   2(aka next cloxkwise head) , x_20, x_01
        //            0
        //          /    \
        //  ----------------------------   
        //    x_20         x_01
        //      /      .      \
        //     /  .             \ 
        //    2--------x_12-------1
        //
        //################################

        void FindNewHEad()
        {
            int headIndex = GetTriType() % 3;
            if (headIndex > -1)
                newHEAD = TVarra[headIndex];
            else newHEAD = null;

        }

        public void BreakUp_Tri_Set_T0_T1_T2_or_T1_T2() {
            FindNewHEad();
            if (HAS_all_3_subTRI) Breakup_3Tris();
            else
                Breakup2Tris();

        }
        void Breakup_3Tris()
        { 
            //the lr of this HEAD tri will be :          

            char _lr = newHEAD.LRO;

            char _Not_lr;
            if (_lr == 'R') _Not_lr = 'L';
            else
                _Not_lr = 'R';

            TriVert[] singleTriTriVErt = new TriVert[3];
            TriVert[] TWINTriTriVErt_1 = new TriVert[3];
            TriVert[] TWINTriTriVErt_2 = new TriVert[3];

            if (GetTriType() == 0)
            {
                //Debug.Log("TYPE 0" + "lrChosen=" + _lr);
                //Making T0
                singleTriTriVErt[0] = TVarra[0];
                singleTriTriVErt[1] = new TriVert(x_01, _lr);
                singleTriTriVErt[2] = new TriVert(x_20, _lr);
                //Making T1
                TWINTriTriVErt_1[0] = TVarra[1];
                TWINTriTriVErt_1[1] = TVarra[2];
                TWINTriTriVErt_1[2] = new TriVert(x_20, _Not_lr);
                //Making T2
                TWINTriTriVErt_2[0] = TVarra[1];
                TWINTriTriVErt_2[1] = new TriVert(x_20, _Not_lr);
                TWINTriTriVErt_2[2] = new TriVert(x_01, _Not_lr);
            }
            else
            if (GetTriType() == 1)
            {
               // Debug.Log("TYPE 1" + "lrChosen=" + _lr);
                //Making T0
                singleTriTriVErt[0] = TVarra[1];
                singleTriTriVErt[1] = new TriVert(x_12, _lr);
                singleTriTriVErt[2] = new TriVert(x_01, _lr);
                //Making T1
                TWINTriTriVErt_1[0] = TVarra[2];
                TWINTriTriVErt_1[1] = TVarra[0];
                TWINTriTriVErt_1[2] = new TriVert(x_01, _Not_lr);
                T1 = new Triangle(-10, TWINTriTriVErt_1, __P1);
                //Making T2
                TWINTriTriVErt_2[0] = TVarra[2];
                TWINTriTriVErt_2[1] = new TriVert(x_01, _Not_lr);
                TWINTriTriVErt_2[2] = new TriVert(x_12, _Not_lr);
            }
            else
            {
              //  Debug.Log("TYPE 2" + "lrChosen=" + _lr);
                //Making T0
                singleTriTriVErt[0] = TVarra[2];
                singleTriTriVErt[1] = new TriVert(x_20, _lr);
                singleTriTriVErt[2] = new TriVert(x_12, _lr);
                //Making T1
                TWINTriTriVErt_1[0] = TVarra[0];
                TWINTriTriVErt_1[1] = TVarra[1];
                TWINTriTriVErt_1[2] = new TriVert(x_20, _Not_lr);

                //Making T2
                TWINTriTriVErt_2[0] = TVarra[1];
                TWINTriTriVErt_2[1] = new TriVert(x_12, _Not_lr);
                TWINTriTriVErt_2[2] = new TriVert(x_20, _Not_lr);

            }

            T0 = new Triangle(singleTriTriVErt, __P1, _lr);
            T1 = new Triangle(TWINTriTriVErt_1, __P1, _Not_lr);
            T2 = new Triangle(TWINTriTriVErt_2, __P1, _Not_lr);
        }




        void Breakup2Tris() {
            char ControlingLR;
            char NOTcontrollingLR='X';



            TriVert[] TWINTriTriVErt_1 = new TriVert[3];
            TriVert[] TWINTriTriVErt_2 = new TriVert[3];

            if (GetTriType() == 3)    //crossing 0 vertex 
            {
                ControlingLR = TVarra[1].LRO;  //NOT THE HEAD< CUZ THE HEAD IS onpath O

                if (ControlingLR=='L')
                    NOTcontrollingLR ='R';
                else
                 if (ControlingLR == 'R')
                    NOTcontrollingLR = 'L';

                //Making T1
                TWINTriTriVErt_1[0] = TVarra[0];
                TWINTriTriVErt_1[1] = TVarra[1];
                TWINTriTriVErt_1[2] = new TriVert(x_12, ControlingLR);
                //Making T2
                TWINTriTriVErt_2[0] = TVarra[0];
                TWINTriTriVErt_2[1] = new TriVert(x_12, NOTcontrollingLR);
                TWINTriTriVErt_2[2] = TVarra[2];
            }
            else
            if (GetTriType() == 4)
            {
                ControlingLR = TVarra[2].LRO;//NOT THE HEAD< CUZ THE HEAD IS onpath O

                if (ControlingLR == 'L')
                    NOTcontrollingLR = 'R';
                else
                 if (ControlingLR == 'R')
                    NOTcontrollingLR = 'L';

                //Making T1
                TWINTriTriVErt_1[0] = TVarra[1];
                TWINTriTriVErt_1[1] = TVarra[2];
                TWINTriTriVErt_1[2] = new TriVert(x_20, ControlingLR);
                //Making T2
                TWINTriTriVErt_2[0] = TVarra[1];
                TWINTriTriVErt_2[1] = new TriVert(x_20, NOTcontrollingLR);
                TWINTriTriVErt_2[2] = TVarra[0];
            }
            else
            {
                ControlingLR = TVarra[0].LRO;//NOT THE HEAD< CUZ THE HEAD IS onpath O

                if (ControlingLR == 'L')
                    NOTcontrollingLR = 'R';
                else
                 if (ControlingLR == 'R')
                    NOTcontrollingLR = 'L';

                //Making T1
                TWINTriTriVErt_1[0] = TVarra[2];
                TWINTriTriVErt_1[1] = TVarra[0];
                TWINTriTriVErt_1[2] = new TriVert(x_01, ControlingLR);
                //Making T2
                TWINTriTriVErt_2[0] = TVarra[2];
                TWINTriTriVErt_2[1] = new TriVert(x_01, NOTcontrollingLR);
                TWINTriTriVErt_2[2] = TVarra[1];

            }

            
            T1 = new Triangle(TWINTriTriVErt_1, __P1, ControlingLR);
            T2 = new Triangle(TWINTriTriVErt_2, __P1, ControlingLR);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < 3; x++) {

                sb.Append("__"+TVarra[x].ToString()+"\n"  ) ;
            }
            // string output = "id= " + ID + "  lrOBJ=" + objLR + T1 == null ? " t1n/a" : " " + T1;

            string allverts = sb.ToString();
            string output = "id= " + ID + "  lrOBJ=" + obj_L_R_OP_X + "  isonpath:"+ IsOnCutPathBOOL.ToString() + " verts "+ allverts;

            return output.ToString();
        }
    }
}
