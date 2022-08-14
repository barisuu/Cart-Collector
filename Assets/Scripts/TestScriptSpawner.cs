using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptSpawner : MonoBehaviour
{
    [Header("Prefab Creator")] 
    [SerializeField] private Transform spawnTarget;
    [SerializeField] private GameObject collectable;

    [Header("Collectable Spawn Settings")] 
    [SerializeField] private float spawnTimer;
    [SerializeField] private List<GameObject> collectableList;
    private List<GameObject> _closedCollectableList;

    private void Start()
    {
        _closedCollectableList = collectableList;
        StartCoroutine(SpawnCollectable());
    }

    private IEnumerator SpawnCollectable()
    {
        do
        {
            Spawn();
            yield return new WaitForSeconds(spawnTimer);
        } while (true);
    }
    
    private void Spawn()
    {
        var randIndex = Random.Range(0, _closedCollectableList.Count);
        _closedCollectableList[randIndex].SetActive(true);
        _closedCollectableList.RemoveAt(randIndex);
    }

    [ContextMenu("Creator")]
    private IEnumerator Creator()
    {
        for (var i = 0; i < 20; i++)
        {
            var newCollectable = Instantiate(collectable, spawnTarget);
            newCollectable.transform.localPosition = GetRandomPos();
            newCollectable.name = "Collectable_" + (i + 1);
            collectableList.Add(newCollectable);
            yield return new WaitForEndOfFrame();
        }
    }

    private static Vector3 GetRandomPos()
    {
        var randX = Random.Range(-10, 10);
        var randZ = Random.Range(-10, 10);
        return new Vector3(randX, 0.5f, randZ);
    }
}