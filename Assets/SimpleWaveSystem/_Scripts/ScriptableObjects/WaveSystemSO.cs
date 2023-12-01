using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RehtseStudio.SimpleWaveSystem.SO
{

    [System.Serializable]
    public class WaveSetUp
    {

        public int objectID; 
        public GameObject gameObjectToSpawn;

    }

    [CreateAssetMenu(menuName = "Simple Wave System/Create New Wave")]
    public class WaveSystemSO : ScriptableObject
    {

        [Header("Amount of objects to spawn on this wave")]
        public WaveSetUp[] objectToSpawnOnThisWave;


        public int ReturnObjectType(int enemynumber)
        {
            
            
            int nextObjectType = objectToSpawnOnThisWave[enemynumber].objectID;
           
            return nextObjectType;

        }

        public int GetWaveLenght()
        {
            return objectToSpawnOnThisWave.Length;
        }

    }

}

