using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneViewActorCamera : MonoBehaviour {

    [MenuItem("BigfootDS/Move Main Camera To SceneView Camera")]
    public static void QuickMoveMainCamToSceneCam()
    {
        if (Camera.main != null)
        {
            // This will grab the first camera tagged as "Main Camera" in the Hierarchy and make its transform values match the Scene View camera's transform values. It'll be a mess if you have more than one camera tagged as "Main Camera"!
            MatchMainCameraToValues();
            Debug.Log("Moved the scene's Main Camera to match the position, rotation and scale of the Scene View camera!");
        } else
        {
            Debug.Log("Camera.main could not be found. Do you have a Camera component on an object tagged as MainCamera?");
        }
    }

    [MenuItem("BigfootDS/Zero Main Camera's Position")]
    public static void ResetMainCamToWorldZero ()
    {
        if (Camera.main != null)
        {
            MatchMainCameraToValues(true, false);
        }
        else
        {
            Debug.Log("Camera.main could not be found. Do you have a Camera component on an object tagged as MainCamera?");
        }
        
    }


    [MenuItem("BigfootDS/Reset Main Camera Transform To Unity Default")]
    public static void ResetMainCamToUnityDefault ()
    {
        if (Camera.main != null)
        {
            MatchMainCameraToValues(false, true);
        }
        else
        {
            Debug.Log("Camera.main could not be found. Do you have a Camera component on an object tagged as MainCamera?");
        }
        
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
            if (Camera.main != null)
            {
                MatchMainCameraToValues();
            }
            else
            {
                Debug.Log("Camera.main could not be found. Do you have a Camera component on an object tagged as MainCamera?");
            }
            
        }
    }

    /// <summary>
    /// This overload will force the Main Camera to match the Scene View camera.
    /// </summary>
    static public void MatchMainCameraToValues ()
    {
        //Debug.Log("Attempting to match Main Camera & SceneView camera in the editor update frames.");
        Camera.main.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
        Camera.main.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
        Camera.main.transform.localScale = SceneView.lastActiveSceneView.camera.transform.localScale;
    }

    /// <summary>
    /// Make the scene's Main Camera move to specified values. This overload lets you specify every value, and takes in the rotation values as a Quaternion instead of a Vector3.
    /// </summary>
    /// <param name="newPosition">New position coordinates.</param>
    /// <param name="newRotation">New rotation values</param>
    /// <param name="newScale">New scale multiplier values.</param>
    /// <param name="useGlobalSpace"> If this is true, all values above will be applied in world/global space.</param>
    static public void MatchMainCameraToValues(Vector3 newPosition, Quaternion newRotation, Vector3 newScale, bool useGlobalSpace)
    {
        if (useGlobalSpace)
        {
            //Debug.Log("Attempting to match Main Camera & SceneView camera in the editor update frames.");
            Camera.main.transform.position = newPosition;
            Camera.main.transform.rotation = newRotation;
            Camera.main.transform.localScale = new Vector3(newScale.x / Camera.main.transform.lossyScale.x, newScale.y / Camera.main.transform.lossyScale.y, newScale.z / Camera.main.transform.lossyScale.z);
        } else
        {
            //Debug.Log("Attempting to match Main Camera & SceneView camera in the editor update frames.");
            Camera.main.transform.localPosition = newPosition;
            Camera.main.transform.localRotation = newRotation;
            Camera.main.transform.localScale = newScale;
        }

    }

    /// <summary>
    /// Make the scene's Main Camera move to specified values. This overload lets you specify every value, and takes in the rotation values as a Vector3 instead of a Quaternion. Defaults to modifying position & rotation in worldspace.
    /// </summary>
    /// <param name="newPosition">New position coordinates.</param>
    /// <param name="newRotation">New rotation values</param>
    /// <param name="newScale">New scale multiplier values.</param>
    static public void MatchMainCameraToValues (Vector3 newPosition, Quaternion newRotation, Vector3 newScale)
    {
        //Debug.Log("Attempting to match Main Camera & SceneView camera in the editor update frames.");
        Camera.main.transform.position = newPosition;
        Camera.main.transform.rotation = newRotation;
        Camera.main.transform.localScale = newScale;
    }

    /// <summary>
    /// Make the scene's Main Camera move to specified values. This overload lets you specify every value, and takes in the rotation values as a Vector3 instead of a Quaternion.
    /// </summary>
    /// <param name="newPosition">New position coordinates.</param>
    /// <param name="newRotation">New rotation values</param>
    /// <param name="newScale">New scale multiplier values.</param>
    /// <param name="useGlobalSpace"> If this is true, all values above will be applied in world/global space.</param>
    static public void MatchMainCameraToValues(Vector3 newPosition, Vector3 newRotation, Vector3 newScale, bool useGlobalSpace)
    {
        if (useGlobalSpace)
        {
            Camera.main.transform.position = newPosition;
            Camera.main.transform.rotation = Quaternion.Euler(newRotation);
            Camera.main.transform.localScale = Camera.main.transform.TransformVector(newScale);
            Camera.main.transform.localScale = new Vector3(newScale.x / Camera.main.transform.lossyScale.x, newScale.y / Camera.main.transform.lossyScale.y, newScale.z / Camera.main.transform.lossyScale.z);
        } else
        {
            Camera.main.transform.localPosition = newPosition;
            Camera.main.transform.localRotation = Quaternion.Euler(newRotation);
            Camera.main.transform.localScale = newScale;
        }

    }

    /// <summary>
    /// Make the scene's Main Camera move to specified values. This overload lets you specify every value, and takes in the rotation values as a Vector3 instead of a Quaternion. Defaults to modifying position & rotation in worldspace.
    /// </summary>
    /// <param name="newPosition">New position coordinates.</param>
    /// <param name="newRotation">New rotation values</param>
    /// <param name="newScale">New scale multiplier values.</param>
    /// <param name="useGlobalSpace"> If this is true, all values above will be applied in world/global space.</param>
    static public void MatchMainCameraToValues(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        Camera.main.transform.position = newPosition;
        Camera.main.transform.rotation = Quaternion.Euler(newRotation);
        Camera.main.transform.localScale = newScale;
    }

    /// <summary>
    /// Make the scene's Main Camera move to specified values. This overload will let you choose between one of two options: either reset the Main Camera to the world-zero, or reset it to Unity's default values. This overload will fail if both are true or if both are false.
    /// </summary>
    /// <param name="zeroTheCamera">Reset the Main Camera's position & rotation to Vector3.zero, and scale of Vector3.one.</param>
    /// <param name="resetToUnityDefault">Reset the Main Camera's position to X0, Y1, Z-10, rotation to Vector3.zero, and scale to Vector3.one.</param>
    static public void MatchMainCameraToValues (bool zeroTheCamera, bool resetToUnityDefault)
    {
        if ((zeroTheCamera && resetToUnityDefault) || (!zeroTheCamera && !resetToUnityDefault))
        {
            Debug.Log("Attempted to match the Main Camera to some values. Was expecting either zeroTheCamera to be true OR resetToUnityDefault to be true. Both can't be true and both can't be false, otherwise behaviour won't be as expected. Please review your own code where you've called the MatchMainCameraToValues function.");
            return;
        }
        if (zeroTheCamera)
        {
            Camera.main.transform.position = Vector3.zero;
            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
            Camera.main.transform.localScale = Vector3.one;
            Debug.Log("Moved the scene's Main Camera to the center of the universe, facing forward!");
        } else if (resetToUnityDefault)
        {
            Camera.main.transform.position = new Vector3(0, 1, -10);
            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
            Camera.main.transform.localScale = Vector3.one;
            Debug.Log("Moved the scene's Main Camera X0, Y1, Z-10 and it's facing forward!");
        } 
    }





}


