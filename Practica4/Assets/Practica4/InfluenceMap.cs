using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace es.ucm.fdi.iav.rts
{
    public struct Vector2Nodo
    {
        public int x, y;
        public float d;
        public Vector2Nodo(int X, int Y)
        {
            x = X;
            y = Y;
            d = 1;
        }

        public Vector2Nodo(int X, int Y, float n)
        {
            x = X;
            y = Y;
            d = n;
        }
    }


    public class InfluenceMap : Grid
    {

        // Mis listas completas de instalaciones y unidades
        private List<Unit> mUnits;
        private List<Facility> mFacilities;

        // Las listas completas de instalaciones y unidades del enemigo
        private List<Unit> nUnits;
        private List<Facility> nFacilities;


        // Las listas completas de accesos limitados y torretas 
        private List<LimitedAccess> resourcesList;
        private List<Tower> towersList;

        float[,] influences;

        float[,] influenceBuffer;

        public float Decay { get; set; }
        public float Momentum { get; set; }

        public int Width { get { return influences.GetLength(0); } }
        public int Height { get { return influences.GetLength(1); } }
        public float GetValue(int x, int y)
        {
            return influences[x, y];
        }


        public InfluenceMap(int size, float decay, float momentum)
        {
            influences = new float[size, size];
            influenceBuffer = new float[size, size];
            Decay = decay;
            Momentum = momentum;
        }

        public InfluenceMap(int w, int h, float decay, float momentum)
        {
            influences = new float[w, h];
            influenceBuffer = new float[w, h];
            Decay = decay;
            Momentum = momentum;
        }

        public void SetInfluence(int x, int y, float value)
        {
            if (x < Width && y < Height)
            {

                influences[x, y] = value;
                influenceBuffer[x, y] = value;
            }

        }

        public void SetInfluence(Vector2 pos, float value)
        {
            if (pos.x < Width && pos.y < Height)
            {
                influences[(int)pos.x, (int)pos.y] = value;
                influenceBuffer[(int)pos.x, (int)pos.y] = value;

            }
        }


        void UpdateInfluenceBuffer()
        {
            for (int i = 0; i < influences.GetLength(0); i++)
                for (int j = 0; j < influences.GetLength(1); j++)
                    influenceBuffer[i, j] = influences[i, j];
        }


        public void UpdateUnits(int player1, int player2)
        {
            // Alctualizacion y carga preliminal de unidades para evitar que cuando se hagan los calculos
            // las influencias se pisen en orden de procesamiento
            mFacilities = new List<Facility>();
            mUnits = new List<Unit>();

            nFacilities = new List<Facility>();
            nUnits = new List<Unit>();

            var Facilities = RTSGameManager.Instance.GetBaseFacilities(player1);
            var PFacilities = RTSGameManager.Instance.GetProcessingFacilities(player1);

            foreach (var m in Facilities)
            {
                mFacilities.Add(m);
            }
            foreach (var p in PFacilities)
            {
                mFacilities.Add(p);
            }

            var UnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(player1);
            var UnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(player1);
            var UnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(player1);

            foreach (var p in UnitsExtractList)
            {
                mUnits.Add(p);
            }
            foreach (var p in UnitsExploreList)
            {
                mUnits.Add(p);
            }
            foreach (var p in UnitsDestroyerList)
            {
                mUnits.Add(p);
            }

            var EnemyFacilities = RTSGameManager.Instance.GetBaseFacilities(player2);
            var EnemyPFacilities = RTSGameManager.Instance.GetProcessingFacilities(player2);

            foreach (var p in EnemyFacilities)
            {
                nFacilities.Add(p);
            }
            foreach (var p in EnemyPFacilities)
            {
                nFacilities.Add(p);
            }

            var EnemyUnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(player2);
            var EnemyUnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(player2);
            var EnemyUnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(player2);

            foreach (var p in EnemyUnitsExtractList)
            {
                nUnits.Add(p);
            }
            foreach (var p in EnemyUnitsExploreList)
            {
                nUnits.Add(p);
            }
            foreach (var p in EnemyUnitsDestroyerList)
            {
                nUnits.Add(p);
            }

            foreach (var p in mFacilities)
            {


            }

            foreach (var p in mUnits)
            {


            }
            foreach (var p in nFacilities)
            {


            }

            foreach (var p in nUnits)
            {


            }

        }

        public void UpdateMap()
        {
            for (int i = 0; i < influences.GetLength(0); i++)
            {
                for (int j = 0; j < influences.GetLength(0); j++)
                {
                    float minInf = 0.0f, maxInf = 0.0f;
                    Vector2Nodo[] next = GetNeighbors(i, j);

                    foreach (Vector2Nodo n in next)
                    {
                        float f = influenceBuffer[n.x, n.y] * Mathf.Exp(-Decay * n.d);
                        maxInf = Mathf.Max(f, maxInf);
                        minInf = Mathf.Min(f, minInf);

                    }
                    if (Mathf.Abs(minInf) > maxInf)
                    {
                        influences[i, j] = Mathf.Lerp(influenceBuffer[i, j], minInf, Momentum);
                    }
                    else
                    {
                        influences[i, j] = Mathf.Lerp(influenceBuffer[i, j], maxInf, Momentum);
                    }

                }
            }



        }

        Vector2Nodo[] GetNeighbors(int x, int y)
        {
            List<Vector2Nodo> retVal = new List<Vector2Nodo>();

            // as long as not in left edge
            if (x > 0)
            {
                retVal.Add(new Vector2Nodo(x - 1, y));
            }

            // as long as not in right edge
            if (x < influences.GetLength(0) - 1)
            {
                retVal.Add(new Vector2Nodo(x + 1, y));
            }

            // as long as not in bottom edge
            if (y > 0)
            {
                retVal.Add(new Vector2Nodo(x, y - 1));
            }

            // as long as not in upper edge
            if (y < influences.GetLength(1) - 1)
            {
                retVal.Add(new Vector2Nodo(x, y + 1));
            }


            // diagonals


            if (x > 0 && y > 0)
            {
                retVal.Add(new Vector2Nodo(x - 1, y - 1, 1.4142f));
            }


            if (x < influences.GetLength(0) - 1 && y < influences.GetLength(1) - 1)
            {
                retVal.Add(new Vector2Nodo(x + 1, y + 1, 1.4142f));
            }


            if (x > 0 && y < influences.GetLength(1) - 1)
            {
                retVal.Add(new Vector2Nodo(x - 1, y + 1, 1.4142f));
            }

            if (x < influences.GetLength(0) - 1 && y > 0)
            {
                retVal.Add(new Vector2Nodo(x + 1, y - 1, 1.4142f));
            }

            return retVal.ToArray();
        }
    }
}
