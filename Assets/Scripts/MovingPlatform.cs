using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;
    private Transform _point;
    private float _speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == _pointB.position) {
            _point = _pointA;
        }
        else if (transform.position == _pointA.position) {
            _point = _pointB;
        }

        transform.position = Vector3.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player") {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = null;
        }
    }

}
