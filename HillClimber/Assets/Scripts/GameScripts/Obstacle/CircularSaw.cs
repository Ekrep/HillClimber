using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CircularSaw : MonoBehaviour
{
    [SerializeField]private SawMovementAxis _sawMovementType;


    private float _sawFirstAngle;

    [Range(0,100)]
    [SerializeField]private float _sawRotationalSpeed;

    [Range(0,100)]
    [SerializeField]private float _sawMovementSpeed;

    [Header("Saw Min Max Movement Positions")]
    [SerializeField]private float _minPosition;
    [SerializeField]private float _maxPosition;

    private bool reached;

    private delegate void MoveSaw();
    private MoveSaw SawMove;

    
    void Start()
    {
        switch (_sawMovementType)
        {
            case SawMovementAxis.X:
                SawMove = MoveSawOnXAxis;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case SawMovementAxis.Y:
                SawMove = MoveSawOnYAxis;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            default:
                break;
        }
        
        _sawFirstAngle = gameObject.transform.rotation.y;
    }

    private void Update()
    {
        SawMove();
    }

    private void MoveSawOnXAxis()
    {
       
        _sawFirstAngle = _sawFirstAngle + _sawRotationalSpeed * 10f * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, _sawFirstAngle, 0);
        if (!reached)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(_minPosition, gameObject.transform.position.y, gameObject.transform.position.z), _sawMovementSpeed * Time.deltaTime);
        }
        
        if (gameObject.transform.position.x == _minPosition && reached == false)
        {
            reached = true;
        }
        if (reached==true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(_maxPosition, gameObject.transform.position.y, gameObject.transform.position.z), _sawMovementSpeed * Time.deltaTime);
            if (gameObject.transform.position.x == _maxPosition && reached == true)
            {
                reached = false;
            }
        }
     
        
  

    }
    private void MoveSawOnYAxis()
    {
        _sawFirstAngle = _sawFirstAngle + _sawRotationalSpeed * 10f * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(_sawFirstAngle, 0, 90);
        if (!reached)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3( gameObject.transform.position.x, _minPosition, gameObject.transform.position.z), _sawMovementSpeed * Time.deltaTime);
        }

        if (gameObject.transform.position.y == _minPosition && reached == false)
        {
            reached = true;
        }
        if (reached == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, _maxPosition, gameObject.transform.position.z), _sawMovementSpeed * Time.deltaTime);
            if (gameObject.transform.position.y == _maxPosition && reached == true)
            {
                reached = false;
            }
        }
    }
   

    private enum SawMovementAxis
    {
        X,
        Y
        
    }

   /* #region Custom Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(CircularSaw))]
    public class CircularSawEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CircularSaw circularSaw = (CircularSaw)target;
            switch (circularSaw._sawMovementType)
            {
                case SawMovementAxis.X:

                    circularSaw._sawMovementSpeed = (float)EditorGUILayout.Slider("Saw Movement Speed",circularSaw._sawMovementSpeed, 0, 100);
                    circularSaw._sawRotationalSpeed = (float)EditorGUILayout.Slider("Saw Rotational Movement Speed", circularSaw._sawRotationalSpeed, 0, 100);
                    break;
                case SawMovementAxis.Y:
                    circularSaw._sawMovementSpeed = (float)EditorGUILayout.Slider("Saw Movement Speed", circularSaw._sawMovementSpeed, 0, 100);
                    circularSaw._sawRotationalSpeed = (float)EditorGUILayout.Slider("Saw Rotational Movement Speed", circularSaw._sawRotationalSpeed, 0, 100);
                    break;
                default:
                    break;
            }
        }
    }
#endif
    #endregion*/
}
