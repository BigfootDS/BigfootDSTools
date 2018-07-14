using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using BigfootDS;

namespace BigfootDS
{
    public class FolderStructureGenerator : MonoBehaviour
    {

        // There is no perfect Project folder structure and this is not an immutable structure.
        // Each game project will end up being laid out differently - this is just a starting point.
        static string[] foldersToGenerate = new string[] { "__Sandbox", "_Demo", "_ImportedAssets", "Animations", "Audio", "Fonts", "Materials", "Models", "Prefabs", "Plugins", "ScriptableObjects", "Scripts", "Shaders", "Sprites", "Textures", "Audio/SFX", "Audio/Music", "Scripts/Editor", "Plugins/iOS", "Plugins/Android" };

        [MenuItem("BigfootDS/Quick Generate Folders in Project")]
        public static void GenerateBasicFolderStructure()
        {
            Debug.Log("Starting the basic project folder structure generator.");
            for (int i = 0; i < foldersToGenerate.Length; i++)
            {
                // The "CreateDirectory" function automatically does nothing if the filepath is invalid or already used.
                // So no if statements or crazy validation checking needed - we can just run the function!
                Directory.CreateDirectory("Assets/" + foldersToGenerate[i]);
            }
            // Forcing an asset database refresh means we will see these new folders in our Project tab instantly.
            AssetDatabase.Refresh();
            Debug.Log("Finished generating a basic project folder structure.");
        }



    }


    public class FolderStructureWizard : ScriptableWizard
    {
        // Using a ScriptableWizard creates a pop-up Unity tab, allowing us to customize the functions we call before we run them.
        // Read more about it here: https://docs.unity3d.com/ScriptReference/ScriptableWizard.html
        [Tooltip("Make all folders listed below inside this folder. Basically setting a master or parent folder for the generator. Leave blank to let it generate directly into the root of your Assets folder.")]
        public string foldersPrefix = "";
        [Tooltip("One folder per entry in the list. You can make quick paths by using slashes - such as folder1/folder2 to make a folder containing a folder.")]
        public string[] customFoldersToGenerate = new string[] { "__Sandbox", "_Demo", "_ImportedAssets", "Animations", "Audio", "Fonts", "Materials", "Models", "Prefabs", "Plugins", "ScriptableObjects", "Scripts", "Shaders", "Sprites", "Textures", "Audio/SFX", "Audio/Music", "Scripts/Editor", "Plugins/iOS", "Plugins/Android" };
        // Bigfoot's favourite folder structure:
        // "__Sandbox", "_Demo", "_ImportedAssets", "Animations", "Audio", "Fonts", "Materials", "Models", "Prefabs", "Plugins", "ScriptableObjects", "Scripts", "Shaders", "Sprites", "Textures", "Audio/SFX", "Audio/Music", "Scripts/Editor", "Plugins/iOS", "Plugins/Android"

        // Privately handle backing up the default list. Devs can change the default list of folders coded up above, and their changes are safely respected by the folder generation wizard's "Reset" button.
        [HideInInspector] public string[] backupFoldersToGenerate = new string[0];
        bool hasBackedUpList = false;

        [MenuItem("BigfootDS/Custom Generate Folders in Project")]
        static void CreateFolderStructureWizard()
        {

            ScriptableWizard.DisplayWizard<FolderStructureWizard>("Folder Structure Wizard", "Create Folders", "Reset Options");

        }



        void OnWizardUpdate()
        {
            if (!hasBackedUpList)
            {
                // Back up the default list of folders to generate.
                // This allows other devs to change the hardcoded list, and let the rest of the script automatically integrate it into the backups system.
                hasBackedUpList = true;
                backupFoldersToGenerate = customFoldersToGenerate;
            }
            helpString = "Please set the folders that you would like to generate!";
            // helpString is automatically picked up by the ScriptableWizard type.
        }

        // When the user presses the "Create" button OnWizardCreate is called.
        // Think of it as the primary button of the wizard.
        void OnWizardCreate()
        {
            Debug.Log("Starting the custom project folder structure generator via the wizard.");

            string folderPrefix = "Assets/";
            if (foldersPrefix.Length > 0)
            {
                folderPrefix += "/" + foldersPrefix + "/";
            }
            for (int i = 0; i < customFoldersToGenerate.Length; i++)
            {
                // The "CreateDirectory" function automatically does nothing if the filepath is invalid or already used.
                // So no if statements or crazy validation checking needed - we can just run the function!
                Directory.CreateDirectory(folderPrefix + customFoldersToGenerate[i]);
            }

            // Forcing an asset database refresh means we will see these new folders in our Project tab instantly.
            AssetDatabase.Refresh();
            Debug.Log("Finished generating a custom project folder structure via the wizard.");
        }

        // When the user presses the "Apply" button OnWizardOtherButton is called.
        // Think of it as the secondary button of the wizard.
        void OnWizardOtherButton()
        {
            customFoldersToGenerate = backupFoldersToGenerate;
            //customFoldersToGenerate = new string[] { "_Demo", "_ImportedAssets", "Animations", "Audio", "Fonts", "Materials", "Models", "Prefabs", "ScriptableObjects", "Scripts", "Shaders", "Sprites", "Textures", "Audio/SFX", "Audio/Music", "Scripts/Editor" };
        }
    }
}