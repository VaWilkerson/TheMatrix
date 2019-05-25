using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransformation : Transformation {

    //INTENT: rotate 
    //USE: put on grid obj
    //RESULT: cool
    
    //1. sets the axis of rotation for the x y and z axis
    public Vector3 rotation;
    public  Vector3 Apply (Vector3 point) {
        float radX = rotation.x * Mathf.Deg2Rad;
        float radY = rotation.y * Mathf.Deg2Rad;
        float radZ = rotation.z * Mathf.Deg2Rad;
        float sinX = Mathf.Sin(radX);    //not gonna lie, I'm a little confused with this part. 
        float cosX = Mathf.Cos(radX);
        float sinY = Mathf.Sin(radY);
        float cosY = Mathf.Cos(radY);
        float sinZ = Mathf.Sin(radZ);
        float cosZ = Mathf.Cos(radZ);

        Vector3 xAxis = new Vector3(    //ok, a lot confused. 
            cosY * cosZ,
            cosX * sinZ + sinX * sinY * cosZ,
            sinX * sinZ - cosX * sinY * cosZ
        );
        Vector3 yAxis = new Vector3(
            -cosY * sinZ,
            cosX * cosZ - sinX * sinY * sinZ,
            sinX * cosZ + cosX * sinY * sinZ
        );
        Vector3 zAxis = new Vector3(
            sinY,
            -sinX * cosY,
            cosX * cosY
        );

        return xAxis * point.x + yAxis * point.y + zAxis * point.z;
    }
    
    //2.
    public override Matrix4x4 Matrix {
        get {
            float radX = rotation.x * Mathf.Deg2Rad;
            float radY = rotation.y * Mathf.Deg2Rad;
            float radZ = rotation.z * Mathf.Deg2Rad;
            float sinX = Mathf.Sin(radX);
            float cosX = Mathf.Cos(radX);
            float sinY = Mathf.Sin(radY);
            float cosY = Mathf.Cos(radY);
            float sinZ = Mathf.Sin(radZ);
            float cosZ = Mathf.Cos(radZ);
			
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetColumn(0, new Vector4(
                cosY * cosZ,
                cosX * sinZ + sinX * sinY * cosZ,
                sinX * sinZ - cosX * sinY * cosZ,
                0f
            ));
            matrix.SetColumn(1, new Vector4(
                -cosY * sinZ,
                cosX * cosZ - sinX * sinY * sinZ,
                sinX * cosZ + cosX * sinY * sinZ,
                0f
            ));
            matrix.SetColumn(2, new Vector4(
                sinY,
                -sinX * cosY,
                cosX * cosY,
                0f
            ));
            matrix.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }

     void Update()
     {
         rotation += new Vector3(.33f, .33f, .33f);
         //rotation.y += 1;
     }

    //ONLY ROTATES ON Z AXIS. for more complicated shit see above. 
    //something something sine and cosine 
//    public override Vector3 Apply (Vector3 point) {
//        float radZ = rotation.z * Mathf.Deg2Rad;    //omg radians  
//        float sinZ = Mathf.Sin(radZ);
//        float cosZ = Mathf.Cos(radZ);
//
//        return new Vector3(
//            point.x * cosZ - point.y * sinZ,
//            point.x * sinZ + point.y * cosZ,
//            point.z
//        );
//    }
}
