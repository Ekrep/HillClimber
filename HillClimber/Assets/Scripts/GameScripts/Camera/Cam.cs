using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Vector3 distance;
    [SerializeField] private GameObject _followedbyCamObject;
    void Start()
    {
        distance = _followedbyCamObject.transform.position - gameObject.transform.position;
    }

    
    void Update()
    {
        gameObject.transform.position = _followedbyCamObject.transform.position - distance;
    }
}
