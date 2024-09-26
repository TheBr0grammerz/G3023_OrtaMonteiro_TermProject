using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _distanceToCenter;


    void Update()
    {
       
    }

    public float GetDistanceToCenter()
    {
        return Vector3.Distance(transform.position, Vector3.zero);
    }
}
