using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ReplaceSpritesInPrefabs : EditorWindow
{
    private Texture2D oldAtlas;
    private Texture2D newAtlas;
    private string newPrefix = "NEW_";
    private string prefabsFolder = "Assets/Prefabs";

    [MenuItem("Tools/Replace Sprites In Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceSpritesInPrefabs>("Replace Sprites In Prefabs");
    }

    void OnGUI()
    {
        GUILayout.Label("Replace Sprites In Prefabs", EditorStyles.boldLabel);
        oldAtlas = (Texture2D)EditorGUILayout.ObjectField("Old Atlas", oldAtlas, typeof(Texture2D), false);
        newAtlas = (Texture2D)EditorGUILayout.ObjectField("New Atlas", newAtlas, typeof(Texture2D), false);
        newPrefix = EditorGUILayout.TextField("New Prefix", newPrefix);
        prefabsFolder = EditorGUILayout.TextField("Prefabs Folder", prefabsFolder);

        if (GUILayout.Button("Replace Sprites In All Prefabs"))
        {
            ReplaceSpritesInAllPrefabs();
        }
    }

    void ReplaceSpritesInAllPrefabs()
    {
        if (oldAtlas == null || newAtlas == null)
        {
            Debug.LogError("Please assign both old and new atlases.");
            return;
        }

        // Получаем все спрайты из старого и нового атласа
        string oldAtlasPath = AssetDatabase.GetAssetPath(oldAtlas);
        string newAtlasPath = AssetDatabase.GetAssetPath(newAtlas);

        var oldSprites = AssetDatabase.LoadAllAssetsAtPath(oldAtlasPath);
        var newSprites = AssetDatabase.LoadAllAssetsAtPath(newAtlasPath);

        // Создаём словарь: "хвост имени" -> новый спрайт
        var newSpriteDict = new Dictionary<string, Sprite>();
        foreach (var obj in newSprites)
        {
            if (obj is Sprite sprite)
            {
                // Обрезаем префикс
                string tail = sprite.name.StartsWith(newPrefix) ? sprite.name.Substring(newPrefix.Length) : sprite.name;
                newSpriteDict[tail] = sprite;
            }
        }

        // Получаем все префабы в папке
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabsFolder });
        int replacedCount = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            bool changed = false;

            // Ищем все SpriteRenderer в префабе
            var renderers = prefab.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var renderer in renderers)
            {
                if (renderer.sprite != null && renderer.sprite.texture == oldAtlas)
                {
                    // Обрезаем префикс у старого имени (если есть)
                    string tail = renderer.sprite.name;
                    if (tail.StartsWith(newPrefix))
                        tail = tail.Substring(newPrefix.Length);

                    // Ищем новый спрайт с таким же хвостом
                    if (newSpriteDict.TryGetValue(tail, out Sprite newSprite))
                    {
                        renderer.sprite = newSprite;
                        changed = true;
                    }
                }
            }

            if (changed)
            {
                EditorUtility.SetDirty(prefab);
                replacedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Done", $"Заменено спрайтов в {replacedCount} префабах!", "OK");
    }
}