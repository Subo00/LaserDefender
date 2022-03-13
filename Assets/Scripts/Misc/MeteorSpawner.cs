using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    ////////////Private//////////////////////////////
    [SerializeField] private ObjectPool[] _meteorPools;
    [SerializeField] private float _spawnerXLeft;
    [SerializeField] private float _spawnerXRight;
    [SerializeField] private float _spawnerY;
    [SerializeField] private Vector2 _minForce;
    [SerializeField] private Vector2 _maxForce;
    [SerializeField] private Vector2 _delayTime; //Vector is used here to store a range between two floats

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAsteroids(_meteorPools));
        }while(true);
    }


    private IEnumerator SpawnAsteroids(ObjectPool[] pools) 
    {        
        
        int tmpIndex = Random.Range(0,pools.Length-1);
        GameObject meteor = pools[tmpIndex].GetPooledObject(); 
        
        if(meteor != null)
        {
            meteor.transform.position = new Vector3(Random.Range(_spawnerXLeft, _spawnerXRight),
                                                    _spawnerY,
                                                    0); //set it's position
            meteor.transform.rotation = Quaternion.identity;   //and rotation
            meteor.SetActive(true);
            
            var tmpRb = meteor.GetComponent<Meteor>().GetRigidbody();
            Vector2 newForce;
            newForce = new Vector2(Random.Range(_minForce.x, _maxForce.x),
                                    Random.Range(_minForce.y, _maxForce.y)); 
            tmpRb.AddForce(newForce, ForceMode2D.Impulse);

        }
        yield return new WaitForSeconds(Random.Range(_delayTime.x,_delayTime.y));
        
    }
}
