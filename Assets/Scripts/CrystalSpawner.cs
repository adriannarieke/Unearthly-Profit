using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] Crystal[] crystalPrefabs = new Crystal[0];
    [SerializeField] int numCrystals = 0;
    [SerializeField] int seed = 0;
    [SerializeField] float spawnSize = 5f;

    void Awake()
    {
        for (int i = 0; i < numCrystals; i++)
        {
            Crystal targetPrefab = crystalPrefabs[Random.Range(0, crystalPrefabs.Length)];
            Crystal newCrystal = Instantiate(targetPrefab, transform);
            System.Random rnd = new System.Random(i + seed);
            newCrystal.transform.position = transform.position + new Vector3(Mathf.Sin(rnd.Next(0, 80)), Mathf.Cos(rnd.Next(0, 80))) * spawnSize;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < numCrystals; i++)
        {
            System.Random rnd = new System.Random(i + seed);
            Vector3 position = transform.position + new Vector3(Mathf.Sin(rnd.Next(0, 80)), Mathf.Cos(rnd.Next(0, 80))) * spawnSize;
            Gizmos.DrawWireSphere(position, 0.05f);
        }
    }
}
