using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TracerBlink : MonoBehaviour, ICastable {
    public float distanceForward = 5;
    public void Cast()
    {
        //gameObject.transform.parent.parent.transform.Translate(gameObject.transform.parent.parent.forward * 3);
        //gameObject.transform.parent.parent.position += gameObject.transform.parent.parent.forward * 3;
        gameObject.GetComponentInParent<CharacterController>().Move(gameObject.transform.parent.parent.forward * distanceForward);
    }
}
