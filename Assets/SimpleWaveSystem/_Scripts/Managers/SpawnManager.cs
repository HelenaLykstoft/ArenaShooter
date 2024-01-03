using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RehtseStudio.SimpleWaveSystem.Managers
{

    public class SpawnManager : MonoBehaviour
    {

        public bool isBetweenRounds = false;

        [Header("Waves properties")]
        private int _waveNumber;
        private int _actualWaveNumber;
        private int _currentObjectToSpawnOnScene;
        private int _objectDestroyed = 0;
        private bool _isNewWave = true;
        private float baseHealth = 0;
        private float enemyHealthCurrentWave = 0;

        [Header("Wait seconds between object spawn")]
        private WaitForSeconds _objectSpawnWaitForSeconds;
        [SerializeField] private float _waitForSecondsBetweenObjectSpawn = 3f;

        [Header("Wait second between waves")]
        private WaitForSeconds _nextWaveRoutineWaitForSeconds;
        [Range(0f, 60f)]
        [SerializeField] private float _waitForSecondBetweenWaves = 3f;

        [Header("Wave Text reference")]
        [SerializeField] private Text _waveSystemText;

        [Header("Position / destination where the objects are going to spawn")]
        [SerializeField] private Vector3 _objectPosition1;
        [SerializeField] private Vector3 _objectPosition2;
        [SerializeField] private Vector3 _objectPosition3;
        [SerializeField] private Vector3 _objectPosition4;
        //Getting Reference to the WaveSystemManager Script and PoolManager Script
        private WaveSystemManager _waveSystemManager;
        private PoolManager _poolManager;


        private void Start()
        {

            _waveSystemManager = GetComponent<WaveSystemManager>();
            _poolManager = GetComponent<PoolManager>();

            _objectSpawnWaitForSeconds = new WaitForSeconds(_waitForSecondsBetweenObjectSpawn);
            _nextWaveRoutineWaitForSeconds = new WaitForSeconds(_waitForSecondBetweenWaves);
            StartWave();
        }

        public void StartWave()
        {
            StartObjectWave();
        }

        private void StartObjectWave()
        {
            _waveSystemText.text = "" + (_actualWaveNumber + 1);
            StartCoroutine(NextWaveRoutine());
        }

        private IEnumerator NextWaveRoutine()
        {

            isBetweenRounds = true;
            if (_waveNumber == 0)
                yield return new WaitForSeconds(3f);
            else
                yield return _nextWaveRoutineWaitForSeconds;
            isBetweenRounds = false;
            _isNewWave = true;
            StartCoroutine(SpawnObjectRoutine());
        }

        private IEnumerator SpawnObjectRoutine()
        {
            _currentObjectToSpawnOnScene = _waveSystemManager.AmountOfObjectToSpawnInThisWave(_waveNumber);

            for (int i = 0; i < _currentObjectToSpawnOnScene; i++)
            {
                //Get the object to pass to the PoolManager
                var selectedObject = _waveSystemManager.ReturnObjectTypeId(_waveNumber, i);
                GameObject newObject = _poolManager.RequestObjectToSpawn(selectedObject);
                //You can change the direcction of the object you want to spawn
                //You can change this part of the code
                //You can use the method ObjectPositionToSpawn(Vector3 _objPos) to set the position of the object or you can use the method ObjectPositionToSpawn() to set a random position
                newObject.transform.position = ObjectPositionToSpawn();
                newObject = SetHealthForWave(newObject);
                newObject.SetActive(true);
                yield return _objectSpawnWaitForSeconds;
            }

        }

        public void ObjectWaveCheck()
        {
            _objectDestroyed++;
            if (_objectDestroyed == _currentObjectToSpawnOnScene)
            {
                _objectDestroyed = 0;
                _waveNumber++;
                _actualWaveNumber++;

                if ((_waveNumber + 1) > _waveSystemManager.WavesReferenceLenght())
                {
                    _waveSystemText.text = "No more Object to spawn";
                    _waveNumber = 0;
                    _objectDestroyed = 0;

                }

                _waveSystemText.text = "" + (_actualWaveNumber + 1);
                StartCoroutine(NextWaveRoutine());

            }
        }

        //You can access this Public Method if you like to change the positon of the object Fixed spwan position
        /*public void ObjectPositionToSpawn(Vector3 _objPos) 
        {
            _objectPosition = _objPos;
        }*/

        //You can access this Public Method if you like to change the positon of the object Random spwan position
        public Vector3 ObjectPositionToSpawn()
        {
            int randomPosition = Random.Range(1, 5);
            switch (randomPosition)
            {
                case 1:
                    return _objectPosition1;

                case 2:
                    return _objectPosition2;

                case 3:
                    return _objectPosition3;

                case 4:
                    return _objectPosition4;

            }
            return _objectPosition1;
        }

        private GameObject SetHealthForWave(GameObject obj)
        {

            if (_actualWaveNumber == 0)
            {
                baseHealth = obj.GetComponent<Health>().maxHealth;
            }
            if (_isNewWave == true)
            {
                _isNewWave = false;
                enemyHealthCurrentWave = baseHealth + (baseHealth * 0.2f * _actualWaveNumber);
            }
            if (obj.name.Contains("Boss") || obj.name.Contains("boss"))
            {
                enemyHealthCurrentWave = enemyHealthCurrentWave * 10;
            }

            obj.GetComponent<Health>().maxHealth = enemyHealthCurrentWave;
            return obj;
        }




    }

}

