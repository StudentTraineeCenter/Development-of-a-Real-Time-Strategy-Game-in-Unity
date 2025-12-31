using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionQueue : MonoBehaviour
{
    public Transform spawnPoint;
    Queue<UnitData> queue = new Queue<UnitData>();
    bool producing = false;

    public void Enqueue(UnitData unit)
    {
        queue.Enqueue(unit);

        if (!producing)
            StartCoroutine(Produce());
    }

    IEnumerator Produce()
    {
        producing = true;

        while (queue.Count > 0)
        {
            UnitData unit = queue.Dequeue();
            yield return new WaitForSeconds(2f); // èas výstavby

            Instantiate(unit.prefab, spawnPoint.position, Quaternion.identity);
        }

        producing = false;
    }
}
