using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMove : MonoBehaviour, ICastable {
    public void Cast()
    {
        gameObject.transform.parent.parent.position += gameObject.transform.parent.parent.forward * 3;
    }
}
