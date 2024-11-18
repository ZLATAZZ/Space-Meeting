using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("UFOs for Decoration")]
    [SerializeField] private GameObject[] ufos;
    [SerializeField] private GameObject[] positionsForUFO;

    [Header("Obstacles")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] crystalsAndWalls;

    [Header("Sounds For Obstacles")]
    [SerializeField] private AudioSource[] obstacleSounds;
    
    [Header("Positions")]
    [SerializeField] private GameObject positionCenter;
    [SerializeField] private GameObject floorPosition;
    [SerializeField] private GameObject[] floorPositions;
    [SerializeField] private GameObject[] sidePositions;
    

    [Header("Aliens")]
    [SerializeField] private GameObject[] aliens;

    public static SpawnManager Instance { get; private set; }


    private GameObject centerObject;
    [HideInInspector] public GameObject alienMain;

    private float spawnIntervalAlien;
    private float spawnIntervalForSide;
    private float spawnIntervalForFloor;

    int floorIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameFollow.Instance.isGameRunning)
        {
            if (SceneFlow.Instance.isHat)
            {
                spawnIntervalForSide = 50.0f;
            }
            else
            {
                spawnIntervalForSide = 20.0f;
            }
            if (SceneFlow.Instance.isShip)
            {
                spawnIntervalForFloor = 10.0f;
            }
            else
            {
                spawnIntervalForFloor = 2.0f;
            }
            if (SceneFlow.Instance.isAlien)
            {
                spawnIntervalAlien = 40.0f;
            }
            else
            {
                spawnIntervalAlien = 60.0f;
            }


            Debug.Log($"Alien: {SceneFlow.Instance.isAlien} ({spawnIntervalAlien} ), Helmet: {SceneFlow.Instance.isHat} ({spawnIntervalForSide}), Ship: {SceneFlow.Instance.isShip}");
            StartCoroutine(SpawnCenter());

            

            if (GameFollow.Instance.hasSettingForVoiceMode == false)
            {
                StartCoroutine(SpawnFloorObjects());
                StartCoroutine(SpawnSideObjects());
                StartCoroutine(SpawnBoost());
            }
            
            

            StartCoroutine(SpawnMainAliens());

        }
    }

    private void Update()
    {
        CheckSpawnedObjects();
        
    }
    private void CreatePooledObjects(GameObject obj, GameObject objectPos)
    {
        if (obj != null)
        {
            obj.transform.position = objectPos.transform.position;

            obj.SetActive(true);
        }
    }

    private void DeactivatePooledObjects(GameObject obj, GameObject objectPos)
    {
        if (obj != null)
        {
            obj.transform.position = objectPos.transform.position;

            obj.SetActive(false);
        }
    }
    private void SpawnUFO(GameObject obj)
    {
        int positionIndex = Random.Range(0, positionsForUFO.Length);
        CreatePooledObjects(obj, positionsForUFO[positionIndex]);
    }
 
  
    private void SpawnCenterObject()
    {
        int crystalIndex = Random.Range(0, crystalsAndWalls.Length);
        centerObject = Instantiate(crystalsAndWalls[crystalIndex], positionCenter.transform.position, Quaternion.identity);
        obstacleSounds[crystalIndex].Play();
    }

    
    private void SpawnAliens()
    {
        int aliensIndex = Random.Range(0, aliens.Length);
        alienMain = Instantiate(aliens[aliensIndex], positionCenter.transform.position, Quaternion.identity);
    }

    

    #region Activate and Deactivate Objects which are created with Pools

    //manage coins
    private void SpawnCoins(GameObject obj)
    {
        CreatePooledObjects(obj, positionCenter);
       
    }

    //manage all floor objects
    private void CreateFloorObjects(GameObject obj)
    {
        CreatePooledObjects(obj, floorPosition);
        
    }

    private void CreateFloorEffect(GameObject obj)
    {
        CreatePooledObjects(obj, floorPositions[ObjectPool.Instance.index]);
    }

    private void DeactivateFloorObject(GameObject obj)
    {
        DeactivatePooledObjects(obj, floorPosition);
    }

    private void DeactivateFloorEffect(GameObject obj)
    {
        DeactivatePooledObjects(obj, floorPositions[ObjectPool.Instance.index]);
    }

    //manage all side objects
    private void CreateSideObjects(GameObject obj)
    {
        CreatePooledObjects(obj, obj);
    }

    private void CreateSideEffects(GameObject obj)
    {
        CreatePooledObjects(obj, sidePositions[ObjectPool.Instance.indexForSide]);
    }

    private void DeactivateSideEffects(GameObject obj)
    {
        DeactivatePooledObjects(obj, sidePositions[ObjectPool.Instance.indexForSide]);
    }


    private void DeactivateSideObjects(GameObject obj)
    {
        DeactivatePooledObjects(obj, obj);
    }
        
    #endregion

    #region all coroutines

    IEnumerator SpawnSideObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnIntervalForSide);
            GameObject sideEffect = ObjectPool.Instance.PooledObject("EffectSide");
            GameObject sideObject = ObjectPool.Instance.PooledObject("Side");
            CreateSideEffects(sideEffect);


            yield return new WaitForSeconds(2.0f);


            CreateSideObjects(sideObject);

            yield return new WaitForSeconds(1.0f);
            DeactivateSideObjects(sideObject);
            DeactivateSideEffects(sideEffect);
        }
    }

    IEnumerator SpawnFloorObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnIntervalForFloor);
            GameObject floorEffect = ObjectPool.Instance.PooledObject("Effect");
            GameObject floorObject = ObjectPool.Instance.PooledObject("Floor");
            CreateFloorEffect(floorEffect);
            

            yield return new WaitForSeconds(2.0f);


            CreateFloorObjects(floorObject);

            yield return new WaitForSeconds(1.0f);
            DeactivateFloorObject(floorObject);
            DeactivateFloorEffect(floorEffect);
        }
    }

    IEnumerator SpawnBoost()
    {

        while (true)
        {
            GameObject coin = ObjectPool.Instance.PooledObject("Coin");

            SpawnCoins(coin);

            yield return new WaitForSeconds(2.0f);
            SpawnCoins(coin);
        }
    }

    IEnumerator SpawnCenter()
    {
        while (true) 
        {
            GameObject ufo = ObjectPool.Instance.PooledObject("UFO");
            SpawnUFO(ufo);
            SpawnCenterObject();

            yield return new WaitForSeconds(6.0f);
        }
    }

    IEnumerator SpawnMainAliens()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnIntervalAlien);
            SpawnAliens();
        }
    }
    #endregion

    private void CheckSpawnedObjects()
    {
        if(alienMain != null)
        {
            centerObject.SetActive(!alienMain.activeSelf);
        }
    }
}
