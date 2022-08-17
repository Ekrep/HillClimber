using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    [HideInInspector]public GameObject selectedGrabbableObj;

    [HideInInspector]public Hand selectedHand;


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        GrabbableObjectSelection();
    }

    private void GrabbableObjectSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Character.Instance.CharacterCurrentState!=Enums.CharacterStates.MidAir)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject.CompareTag("Grabbable"))
                    {
                        if (Vector3.Distance(Character.Instance.gameObject.transform.position, hit.transform.position) <
                            Character.Instance.FlyAbleDistance && Character.Instance.CharacterCurrentState == Enums.CharacterStates.Alive&&
                            hit.transform.GetComponent<GrabbableObj>().IsGrabbable&&selectedGrabbableObj!=hit.transform.gameObject)
                        {
                            selectedGrabbableObj = hit.transform.gameObject;
                            if (HandManager.Instance.hands[0].IsGrabbed == false)
                            {
                                hit.transform.GetComponent<GrabbableObj>().GrabbableObjectSpringJoint.connectedBody = HandManager.Instance.hands[0].GetComponent<Rigidbody>();
                                hit.transform.GetComponent<GrabbableObj>().PullCharacter();
                                selectedHand = HandManager.Instance.hands[0];
                                GameManager.Instance.GrabbableObjectSelected();

                            }
                            else
                            {
                                hit.transform.GetComponent<GrabbableObj>().GrabbableObjectSpringJoint.connectedBody = HandManager.Instance.hands[1].GetComponent<Rigidbody>();
                                hit.transform.GetComponent<GrabbableObj>().PullCharacter();
                                selectedHand=HandManager.Instance.hands[1];
                                GameManager.Instance.GrabbableObjectSelected();
                            }



                        }

                    }
                }
            
                
            }
        }
    }



}
