using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TracerRecall : MonoBehaviour, ICastable
{
    public float recallLength = 3;
    public int posCount = 10;
    private int trimmingCount = 0;
    private float curTime;

    private LinkedList<Vector3> positionList;
    private LinkedList<Quaternion> rotationList;
    private LinkedList<Quaternion> camRotationList;
    //private Vector3 recallForward;

    private bool recalling;

    public float recallingLength = 0.5f;
    private float curingTime;
    private float accCuringTime;

    private Vector3 turnToPos;
    private Quaternion turnToRotation;
    private Quaternion turnToCamRotation;

    void Awake()
    {
        positionList = new LinkedList<Vector3>();
        rotationList = new LinkedList<Quaternion>();
        camRotationList = new LinkedList<Quaternion>();
    }

    void OnEnable()
    {
        recalling = false;
        curTime = 0;

        positionList.Clear();
        positionList.AddLast(gameObject.GetComponentInParent<ParentMarker>().transform.position);

        rotationList.Clear();
        rotationList.AddLast(gameObject.GetComponentInParent<ParentMarker>().transform.rotation);

        camRotationList.Clear();
        camRotationList.AddLast(gameObject.GetComponentInParent<ParentMarker>().gameObject.GetComponentInChildren<CameraMarker>().transform.rotation);

        trimmingCount = 1;

        turnToPos = positionList.Last.Value;
        turnToRotation = rotationList.Last.Value;
        turnToCamRotation = camRotationList.Last.Value;

        curingTime = 0;
        accCuringTime = 0;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if(recalling)
        {
            gameObject.GetComponentInParent<FirstPersonController>().enabled = false;
            if (positionList.Count > 0)
            {
                if (curingTime >= recallingLength / (float)trimmingCount)
                {
                    curingTime = 0;
                }

                if(curingTime <= 0)
                {
                    turnToPos = positionList.Last.Value;
                    turnToRotation = rotationList.First.Value;//Last.Value for curingTime
                    turnToCamRotation = camRotationList.First.Value;//Last.Value for curingTime
                    positionList.RemoveLast();
                    rotationList.RemoveLast();
                    camRotationList.RemoveLast();
                }

                gameObject.GetComponentInParent<ParentMarker>().transform.position = Vector3.MoveTowards(gameObject.GetComponentInParent<ParentMarker>().transform.position, turnToPos, Vector3.Distance(gameObject.GetComponentInParent<ParentMarker>().transform.position, turnToPos) / (((recallingLength / (float)trimmingCount) - curingTime) / Time.deltaTime));

                gameObject.GetComponentInParent<ParentMarker>().transform.rotation = Quaternion.Slerp(gameObject.GetComponentInParent<ParentMarker>().transform.rotation, turnToRotation, (Time.deltaTime / ((recallingLength / (float)trimmingCount) - accCuringTime/*curingTime*/)));

                gameObject.GetComponentInParent<ParentMarker>().gameObject.GetComponentInChildren<CameraMarker>().transform.rotation = Quaternion.Slerp(gameObject.GetComponentInParent<ParentMarker>().gameObject.GetComponentInChildren<CameraMarker>().transform.rotation, turnToCamRotation, (Time.deltaTime / ((recallingLength / (float)trimmingCount) - accCuringTime/*curingTime*/)));
                
                curingTime += Time.deltaTime;
                accCuringTime += Time.deltaTime;

                return;
            }
            gameObject.GetComponentInParent<ParentMarker>().GetComponent<FirstPersonController>().m_MouseLook.Init(gameObject.GetComponentInParent<ParentMarker>().transform, gameObject.GetComponentInParent<ParentMarker>().gameObject.GetComponentInChildren<CameraMarker>().transform);
            gameObject.GetComponentInParent<ParentMarker>().GetComponent<FirstPersonController>().enabled = true;

            positionList.Clear();
            positionList.AddLast(gameObject.GetComponentInParent<ParentMarker>().transform.position);

            rotationList.Clear();
            rotationList.AddLast(gameObject.GetComponentInParent<ParentMarker>().transform.rotation);

            camRotationList.Clear();
            camRotationList.AddLast(gameObject.GetComponentInParent<ParentMarker>().gameObject.GetComponentInChildren<CameraMarker>().transform.rotation);

            curTime = 0;
            recalling = false;
        }
        else
        {
            if (curTime >= recallLength / (float)posCount)
            {
                if(positionList.Count >= posCount)
                {
                    positionList.RemoveFirst();
                    rotationList.RemoveFirst();
                    camRotationList.RemoveFirst();
                }
                positionList.AddLast(gameObject.GetComponentInParent<ParentMarker>().transform.position);
                rotationList.AddLast(gameObject.GetComponentInParent<ParentMarker>().transform.rotation);
                camRotationList.AddLast(gameObject.GetComponentInParent<ParentMarker>().gameObject.GetComponentInChildren<CameraMarker>().transform.rotation);

                curTime = 0;
            }
            curTime += Time.deltaTime;
        }
    }

    public void Cast()
    {
        recalling = true;
        trimmingCount = positionList.Count;
    }
}