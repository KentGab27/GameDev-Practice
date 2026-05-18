using System.Threading.Tasks;
using UnityEngine;

public class FallController : MonoBehaviour
{
    [Header("Fall Settings")]
    [SerializeField] private float FallSpeed;
    [SerializeField] private GameObject[] FallingObjects;

    void Start()
    {
        InvokeRepeating("Fall", FallSpeed, FallSpeed);
    }

    void Fall()
    {
        if (FallingObjects == null || FallingObjects.Length == 0) return;

        GameObject prefab = FallingObjects[Random.Range(0, FallingObjects.Length)];
        Instantiate(prefab, new Vector3(Random.Range(-10, 10), 10, 0), Quaternion.identity); 
    }
}
