using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace mymesh {
    public class TRImanager 
    {
        List<TRI> _TRILIST;
        List<int> _trianglesList;
        List<Vector3> _verteciesList;

        public TRImanager(Mesh aMEsh) {
            _TRILIST = new List<TRI>();
            _trianglesList = aMEsh.triangles.ToList();
            _verteciesList = aMEsh.vertices.ToList();
            Init_TRILIST();

        }

        void Init_TRILIST() {
            int TMax=_trianglesList.Count;           
            for (int idCnt = 0; idCnt < TMax / 3; idCnt++)
            {
                VERT[] VERTarra = new VERT[3];
                for (int _localOffset = 0; _localOffset < 3; _localOffset++)
                {
                    int i = idCnt*3 + _localOffset;
                    int valAti = _trianglesList[i];
                    Vector3 v3 = _verteciesList[valAti];
                    VERT aVERT = new VERT(i, valAti, valAti, v3);
                    VERTarra[_localOffset] = aVERT;
                }
                TRI aTRI = new TRI(idCnt, VERTarra);
                _TRILIST.Add(aTRI);
            }
        }
        //this should only makelists and return ytthem ,, the eshecxtenssion will draw mesh


        public int CNTTRI = 0;
       public void Remove_TRI(int TID) {
            int TID2 = 0 ;
            TRI tritoremove = _TRILIST.Find(e => e.id == CNTTRI);           
            _TRILIST.Remove(tritoremove);
            Update_triangleList();

            CNTTRI++;
        }

       public int[] GetTriARRA() {       
                //for (int i = 0; i < _trianglesList.Count; i++) Debug.Log("{{{{" + i + "," + _trianglesList[i] + " }}}}}");              
                return _trianglesList.ToArray();
        }
       public Vector3[] GetVertARRA() { return _verteciesList.ToArray(); }
        public List<Vector3> GetVertList2() { return _verteciesList; }

        void Update_triangleList()
        {
            _trianglesList.Clear();
            _trianglesList = _TRILIST.SelectMany(e => e.my3Verts).Select(e => e._Tv).ToList();
        }

        void Print_TriINTS_From_TRRIILIST() {
            Debug.Log("tri from TRILIST");
            foreach (TRI tri in _TRILIST)
            {

                Debug.Log("["+tri._V1._Ti+","+ tri._V1._Tv+"]");
                Debug.Log("[" + tri._V2._Ti + "," + tri._V2._Tv + "]");
                Debug.Log("[" + tri._V3._Ti + "," + tri._V3._Tv + "]");

            }
        }
    }
}
