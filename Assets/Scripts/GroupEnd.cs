using UnityEngine;

public class GroupEnd : MonoBehaviour
{
    public Spawner spawner;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        spawner.NextGeneration();
    }
}
