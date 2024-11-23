using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _meteorPrefab;

    [SerializeField] 
    private GameObject _player;

    [SerializeField]
    private int _maxMeteors;

    private List<GameObject> _meteors;


    void Start()
    {
        //for (int i = 0; i < 20; i++)
        //{
        //    Vector3 spawnPoint = new Vector3(_player.transform.position.x + Random.Range(-50f, 50f),
        //        _player.transform.position.y + Random.Range(-50f, 50f), 0.0f);

        //    _meteors.Add(Instantiate(_meteorPrefab, spawnPoint, Quaternion.identity));
        //}
    }

    void Update()
    {
        if (_meteors.Count < _maxMeteors)
        {
            Vector3 spawnPoint = new Vector3(_player.transform.position.x + Random.Range(-50f, 50f),
                _player.transform.position.y + Random.Range(-50f, 50f), 0f);

            GameObject newMeteor = Instantiate(_meteorPrefab, spawnPoint, Quaternion.identity);
            
            _meteors.Add(newMeteor);
        }

        foreach (GameObject meteor in _meteors)
        {
            float distance = Vector3.Distance(_player.transform.position, meteor.transform.position);

            if (distance > 100f)
            {
                Destroy(meteor);
            }
        }
    }

}
