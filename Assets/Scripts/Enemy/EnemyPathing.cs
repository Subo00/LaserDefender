using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] private WaveConfig _waveConfig;

    /////////Private variables////////
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;
    void OnEnable()
    {
        _waypoints = _waveConfig.GetWaypoints();
        _waypointIndex = 0;
        transform.position = _waypoints[_waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        _waveConfig = waveConfig;
    }

    private void Move()
    {
        if(_waypointIndex <= _waypoints.Count - 1)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;
            var movementThisFrame = _waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, 
                                                    targetPosition, 
                                                    movementThisFrame);
            
            if(transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
