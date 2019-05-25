using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class TransformationGrid : MonoBehaviour
{
    public Transform prefab;

    public int gridResolution = 10;    //10 x 10 grid. Sort of. increasing the #, increases number of cubes but also pulls them out of a grid and onto sa flat plane. 

    Transform[] grid;
    
    List<Transformation> transformations;
    
    //8.
    Matrix4x4 transformation;


    
    void Awake () {

        //1. CREATE a 3D grid of points in order to visualize our transformations. 
        grid = new Transform[gridResolution * gridResolution * gridResolution];
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
        
        //4. MAKE a list for refs to the components. "The advantage (over an array) is that it will put the components in the list instead of creating a new array"
        transformations = new List<Transformation>();
    }
    
    void Update () {
        
        UpdateTransformation();
        //6. TRANSFORM points according to changes in the gridResolution
        //GetComponents<Transformation>(transformations);
        for (int i = 0, z = 0; z < gridResolution; z++) {    
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }
        
        //9.
        void UpdateTransformation () {
            GetComponents<Transformation>(transformations);
            if (transformations.Count > 0) {
                transformation = transformations[0].Matrix;
                for (int i = 1; i < transformations.Count; i++) {
                    transformation = transformations[i].Matrix * transformation;
                }
            }
        }
    
    //2. INSTANTIATE a prefab at the points on the grid. 
    Transform CreateGridPoint (int x, int y, int z) {
        Transform point = Instantiate<Transform>(prefab);    //instantiates a prefab (cube or w/e)...
        point.localPosition = GetCoordinates(x, y, z);    //...at the point we are assigning it
        point.GetComponent<MeshRenderer>().material.color = new Color(    //changes color of prefab based on grid position. A rainbow :D 
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );
        return point;
    }
    
    //3. CENTERS the grid @ origin so any transformations are relative to its centre. 
    // I dont see how this is doing that. Is it just because we set the prefab origin to 0,0,0 in the inspector? 
    // Just tried setting it to something else. Did not work. Still no fucking clue. 
    // Also did you know that if you mess with a prefab in play mode, those transformations persist? Not cool, Unity. 
    Vector3 GetCoordinates (int x, int y, int z) {
        return new Vector3(
            x - (gridResolution - 1) * 0.5f,    
            //ok so I set this to 10F and it shifted on the x axis, so i get that now, but why are we setting it to .5f? Is that arbitrary?
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }
    
    //7. APPLIES transformations to the original coords instead of the current coords so you don't fuck up the intended shape by stacking transforms.
    Vector3 TransformPoint (int x, int y, int z) {
        Vector3 coordinates = GetCoordinates(x, y, z);
        for (int i = 0; i < transformations.Count; i++) {    
            coordinates = transformations[i].Apply(coordinates);
        }
        return coordinates;
    }
    
    
    
}
