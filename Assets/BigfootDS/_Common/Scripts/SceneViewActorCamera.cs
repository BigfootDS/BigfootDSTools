using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneViewActorCamera : MonoBehaviour {


    [MenuItem("BigfootDS/Move Main Camera To SceneView Camera")]
    public static void QuickMoveMainCamToSceneCam()
    {
        // This will grab the first camera tagged as "Main Camera" in the Hierarchy and make its transform values match the Scene View camera's transform values. It'll be a mess if you have more than one camera tagged as "Main Camera"!
        Camera.main.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
        Camera.main.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
        Camera.main.transform.localScale = SceneView.lastActiveSceneView.camera.transform.localScale;
        Debug.Log("Moved the scene's Main Camera to match the position, rotation and scale of the Scene View camera!");
        
    }


}
