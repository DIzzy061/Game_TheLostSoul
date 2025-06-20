using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CopySpriteSlicing : EditorWindow
{
    private Texture2D sourceTexture;
    private Texture2D targetTexture;

    [MenuItem("Tools/Copy Sprite Slicing")]
    public static void ShowWindow()
    {
        GetWindow<CopySpriteSlicing>("Copy Sprite Slicing");
    }

    void OnGUI()
    {
        GUILayout.Label("Copy Sprite Slicing from one texture to another", EditorStyles.boldLabel);

        sourceTexture = (Texture2D)EditorGUILayout.ObjectField("Source Texture", sourceTexture, typeof(Texture2D), false);
        targetTexture = (Texture2D)EditorGUILayout.ObjectField("Target Texture", targetTexture, typeof(Texture2D), false);

        if (GUILayout.Button("Copy Slicing"))
        {
            if (sourceTexture == null || targetTexture == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign both source and target textures.", "OK");
                return;
            }
            CopySlicing(sourceTexture, targetTexture);
        }
    }

    static void CopySlicing(Texture2D source, Texture2D target)
    {
        string sourcePath = AssetDatabase.GetAssetPath(source);
        string targetPath = AssetDatabase.GetAssetPath(target);

        var sourceImporter = AssetImporter.GetAtPath(sourcePath) as TextureImporter;
        var targetImporter = AssetImporter.GetAtPath(targetPath) as TextureImporter;

        if (sourceImporter == null || targetImporter == null)
        {
            Debug.LogError("Could not get TextureImporter for one of the textures.");
            return;
        }

        string prefix = "NEW_";

        // Read slicing from source
        var sourceSprites = new List<SpriteMetaData>(sourceImporter.spritesheet);

        for (int i = 0; i < sourceSprites.Count; i++)
        {
            var meta = sourceSprites[i];
            meta.name = prefix + meta.name;
            sourceSprites[i] = meta;
        }

        // Apply slicing to target
        targetImporter.spriteImportMode = SpriteImportMode.Multiple;
        targetImporter.spritesheet = sourceSprites.ToArray();

        // Save and reimport
        EditorUtility.SetDirty(targetImporter);
        targetImporter.SaveAndReimport();

        EditorUtility.DisplayDialog("Done", "Slicing copied with prefix! Unity will now generate new sprite IDs for the target texture.", "OK");
    }
}