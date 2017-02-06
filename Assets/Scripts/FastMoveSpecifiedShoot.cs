using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMoveSpecifiedShoot : SpecifiedShoot {
    public override void cast()
    {
        gameObject.transform.parent.parent.position = new Vector3(0f, 0f, 0f);
    }
}
