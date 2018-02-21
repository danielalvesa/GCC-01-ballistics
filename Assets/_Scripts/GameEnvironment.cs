using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironment : MonoBehaviour {

    //force applied on every bullet in the scene
    //is randomize every new game
    public static Vector3 windZone;

    //New Gravity created just to have control over the value
    public static Vector3 GRAVITY = new Vector3(0, -9.807f, 0);

    void Start()
    {
        windZone = Random.insideUnitSphere;
    }

}
