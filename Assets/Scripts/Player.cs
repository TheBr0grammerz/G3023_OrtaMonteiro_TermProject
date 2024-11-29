using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    

    [SerializeField]
    private float _distanceToCenter;

    [SerializeField]
    private GameObject _projectilePrefab;




    void Update()
    {
        GetDistanceToCenter();
    }

    public float GetDistanceToCenter()
    {
        return Vector3.Distance(transform.position, Vector3.zero);
    }

    public void Shoot()
    {
        Instantiate(_projectilePrefab, transform.position, transform.rotation);
    }

}