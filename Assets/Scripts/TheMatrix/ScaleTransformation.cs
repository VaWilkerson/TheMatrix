using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTransformation : Transformation {
    
    //INTENT: scale grid
    //USE: Put script on TransformGrid obj
    //RESULT: neat. 

    public Vector3 scale;

    public  Vector3 Apply (Vector3 point) {    //set to 1, 1, 1 in inspector for what it looked like before
        point.x *= scale.x;
        point.y *= scale.y;
        point.z *= scale.z;
        return point;
    }

    public override Matrix4x4 Matrix {
        get {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(scale.x, 0f, 0f, 0f));
            matrix.SetRow(1, new Vector4(0f, scale.y, 0f, 0f));
            matrix.SetRow(2, new Vector4(0f, 0f, scale.z, 0f));
            matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }
    
}
