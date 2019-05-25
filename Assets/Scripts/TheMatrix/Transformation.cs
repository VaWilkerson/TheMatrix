using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transformation : MonoBehaviour
{
    //"base component for all transformations, that they can inherit from"
    //I get what that means, but I don't know how this code is making it work.
    //it's a template sort of? 
    //public abstract Vector3 Apply (Vector3 point);
    public Vector3 Apply (Vector3 point) {
        return Matrix.MultiplyPoint(point);
    }
    
    //ok now I see. 
    public abstract Matrix4x4 Matrix { get; }    //has a Vect3 parameter, assumes that the missing fourth coordinate is 1.
    // "If you want to multiply a direction instead of a point, you can use Matrix4x4.MultiplyVector."
}
