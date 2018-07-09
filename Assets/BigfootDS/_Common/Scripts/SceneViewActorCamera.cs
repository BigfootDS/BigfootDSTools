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

    [MenuItem("BigfootDS/Zero Main Camera's Position")]
    public static void ResetMainCamToWorldZero ()
    {
        Camera.main.transform.position = Vector3.zero;
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
        Camera.main.transform.localScale = Vector3.one;
        Debug.Log("Moved the scene's Main Camera to the center of the universe, facing forward!");
    }

    [MenuItem("BigfootDS/Reset Main Camera Transform To Unity Default")]
    public static void ResetMainCamToUnityDefault ()
    {
        Camera.main.transform.position = new Vector3(0, 1, -10);
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
        Camera.main.transform.localScale = Vector3.one;
        Debug.Log("Moved the scene's Main Camera X0, Y1, Z-10 and it's facing forward!");
    }

    // Toggle-able actor camera with checkmark in editor menu enabled by this:http://answers.unity.com/answers/1525395/view.html 
    private const string MenuName = "BigfootDS/Toggle Actor Camera";
    private const string SettingName = "ActorCameraEnabled";

    public static bool IsActorCamEnabled
    {
        // Correct way to set up a variable for things like EditorPrefs & PlayerPrefs. 
        // Get/set handled by variable and context of variable usage.

        // The "false" in this line is the default value. If you introduce this script into your project for the first time, we don't want the actor camera functionality to instantly run and wreck the scene's stuff.
        get { return EditorPrefs.GetBool(SettingName, false); }

        // Straightforward part. Assigns a value to the variable.
        set { EditorPrefs.SetBool(SettingName, value); }
    }

    [MenuItem(MenuName)]
    private static void ToggleAction()
    {
        // Inverts the bool. If it's true, make it false. Or if it's false, make it true.
        IsActorCamEnabled = !IsActorCamEnabled;
        
        // Adding or removing the Update function from the Editor's Update event list for clarity & performance.
        // Might not actually be needed. Further research required.
        if (IsActorCamEnabled)
        {
            EditorApplication.update += Update;
        } else
        {
            EditorApplication.update -= Update;
        }
    }

    [MenuItem(MenuName, true)]
    private static bool ToggleActionValidate()
    {
        // Validation function for the "ToggleAction" function. 
        // Important to prevent errors & crashes in Editor scripting as it affects the whole Editor.
        Menu.SetChecked(MenuName, IsActorCamEnabled);
        return true;
    }

    static void Update()
    {
        
        if (!EditorApplication.isPlaying && IsActorCamEnabled)
        {
            //Debug.Log("Attempting to match Main Camera & SceneView camera in the editor update frames.");
            Camera.main.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
            Camera.main.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
            Camera.main.transform.localScale = SceneView.lastActiveSceneView.camera.transform.localScale;
        }
    }

}


