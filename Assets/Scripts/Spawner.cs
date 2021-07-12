using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;
    public int renderAmount = 3;
    public float spacing = 5f;
    public GameManager gameManager;
    public Transform player;
    public GameObject groupEnd;
    public Score score;

    private Dictionary<GameObject, float> objectLengths = new Dictionary<GameObject, float>();
    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    private List<GameObject> activeObjects = new List<GameObject>();
    
    private static float GetObjectLength(GameObject obj)
    {
        var children = obj.GetComponentsInChildren<Transform>();
        return children.Select(child => child.localPosition.z).Max() + 1;
    }

    private void OnValidate()
    {
        renderAmount = Math.Max(2, renderAmount);
    }

    private void Awake()
    {
        FillObjectLengths();
    }

    private void Start()
    {
        GenerateObjects();
    }

    private void FillObjectLengths()
    {
        foreach (var obj in objects)
        {
            objectLengths[obj] = GetObjectLength(obj);
        }
    }

    private void RemoveObjects()
    {
        foreach (var obj in activeObjects)
        {
            Destroy(obj);
        }
        activeObjects.Clear();
    }

    private void GenerateObjects()
    {
        while (objectQueue.Count < renderAmount)
        {
            objectQueue.Enqueue(GetRandomObject());
        }

        var offset = new Vector3(0, 0, 0);
        var objectQueueArray = objectQueue.ToArray();
        for (var i = 0; i < objectQueueArray.Length; i++)
        {
            var selectedObject = objectQueueArray[i];
            
            var position = transform.position + offset;
            var obj = Instantiate(selectedObject, position, Quaternion.identity);
            activeObjects.Add(obj);

            offset.z += objectLengths[selectedObject];

            if (i == 1)
                CreateGroupEnd(transform.position + offset);

            offset.z += spacing;
        }
    }

    private void CreateGroupEnd(Vector3 position)
    {
        var obj = Instantiate(groupEnd, position, Quaternion.identity);
        var component = obj.AddComponent<GroupEnd>();
        component.spawner = this;
        activeObjects.Add(obj);
    }

    public void NextGeneration()
    {
        if (!gameManager.isRunning)
            return;
        FindObjectOfType<AudioManager>().Play("Ding");
        objectQueue.Dequeue();
        RemoveObjects();
        GenerateObjects();
        ResetPlayerPosition();
        score.AddPoints(1);
    }

    private void ResetPlayerPosition()
    {
        var newPosition = player.position;
        var firstObject = objectQueue.Peek();
        newPosition.z = transform.position.z + objectLengths[firstObject];
        player.position = newPosition;
    }
    
    private GameObject GetRandomObject()
    {
        return objects[Random.Range(0, objects.Length)];
    }
}
