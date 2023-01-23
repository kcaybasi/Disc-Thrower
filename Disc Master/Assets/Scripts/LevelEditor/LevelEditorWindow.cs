using System;
using System.Collections;
using System.Collections.Generic;
using LevelEditor;
using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    Texture2D _headerSectionTexture;
    Texture2D _levelSectionTexture;
    Texture2D _levelIcon;
    private const float IconSize = 80;

    readonly Color _headerSectionColor = new Color(13f/255f,32f/255f, 61f/255f, 1);
    
    Rect _headerSection;
    Rect _levelSection;
    Rect _levelIconSection;
    
    GUISkin _skin;
    public static LevelData EditorLevelData { get; set; }
    
    //create an open window function
    [MenuItem("Window/Level Editor")]
    public static void OpenWindow()
    {
        LevelEditorWindow window = (LevelEditorWindow)GetWindow(typeof(LevelEditorWindow));
        window.minSize= new Vector2(600, 300);
        window.Show();
    }
    public static void CloseWindow()
    {
        LevelEditorWindow window = (LevelEditorWindow)GetWindow(typeof(LevelEditorWindow));
        window.Close();
    }
    //Similar to start or awake function
    private void OnEnable()
    {
        InitTextures();
        InitData();
        _skin= Resources.Load<GUISkin>("GUIStyles/EnemySkin");
    }
    
    //Initialize data
    void InitData()
    {
        EditorLevelData= (LevelData)CreateInstance(typeof(LevelData));
    }
    
    //Initialize Texture2D values
    void InitTextures()
    {
        _headerSectionTexture= new Texture2D(1,1);
        _headerSectionTexture.SetPixel(0,0, _headerSectionColor);
        _headerSectionTexture.Apply();
        
        _levelSectionTexture=Resources.Load<Texture2D>("Textures/MageBackground");
        _levelIcon=Resources.Load<Texture2D>("Textures/MageIcon");
    }

    //Similar to update function
    //Not called every frame but it is called more than once per interaction
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawLevelSettings();
    }

    //Defines rect values and paints Textures based on Rects
    void DrawLayouts()
    {
        _headerSection.x= 0;
        _headerSection.y= 0;
        _headerSection.width= Screen.width;
        _headerSection.height = 50;      
        
        _levelSection.x= 0;
        _levelSection.y= 50;
        _levelSection.width = position.width;
        _levelSection.height = position.width-50;     
        
        _levelIconSection.x=_levelSection.x+_levelSection.width/2-IconSize/2;
        _levelIconSection.y = _levelSection.y + 8;
        _levelIconSection.width = IconSize;
        _levelIconSection.height = IconSize;
        
        GUI.DrawTexture(_headerSection, _headerSectionTexture);
        GUI.DrawTexture(_levelSection, _levelSectionTexture);
        GUI.DrawTexture(_levelIconSection, _levelIcon);
        
    }
    
    //Draw contents of header
    void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);
        
        GUILayout.Label("Level Editor", _skin.GetStyle("Header1"));
        
        GUILayout.EndArea();
    }
    //Draw contents of mage region
    void DrawLevelSettings()
    {
        GUILayout.BeginArea(_levelSection);
        GUILayout.Space(IconSize+8);
        GUILayout.Label("SpawnableSettings", _skin.GetStyle("Header2"));
        
        //Assigning prefab to Editor Level Data
        EditorLevelData.spawnablePrefab= (GameObject)EditorGUILayout.ObjectField("Spawnable Prefab", EditorLevelData.spawnablePrefab, typeof(GameObject), false);
        
        //Spawnable create button
        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            SpawnPrefab();
        }
        GUILayout.Space(100);
        
        //Save button
        if (GUILayout.Button("Save!", GUILayout.Height(40)))
        {
            SaveLevelData();
        }
        
        GUILayout.EndArea();
    }

    private void SaveLevelData()
    {
        LevelData levelDataScriptableObj=CreateInstance<LevelData>(); //Create a new instance of LevelData Scriptable Object
        var spawnableObjects = SpawnableObjects(); //Get all spawnable objects in the scene
        List<ObjectData> objectDataList = new List<ObjectData>(); //Create a new list of ObjectData
        CreateObjectDataList(spawnableObjects, objectDataList);
        levelDataScriptableObj.LevelObjects = objectDataList; //Assign the objectDataList to the LevelObjects list in the LevelData Scriptable Object
        var path = GetSavePath(); //Get the path to save the file
        SaveScriptableObj(levelDataScriptableObj, path); //Save the level data to the path
        ClearScene(spawnableObjects); //Clear the scene
    }

    private static void ClearScene(List<GameObject> spawnableObjects)
    {
        foreach (GameObject spawnableObject in spawnableObjects) //Destroy all spawnable objects to prevent duplicates
        {
            DestroyImmediate(spawnableObject);
        }
    }

    private static void SaveScriptableObj(LevelData levelDataScriptableObj, string path)
    {
        AssetDatabase.CreateAsset(levelDataScriptableObj, path); //Create the asset
        AssetDatabase.SaveAssets(); //Save the asset
        AssetDatabase.Refresh(); //Refresh the asset database
    }
    private static string GetSavePath()
    {
        string path =
            EditorUtility.SaveFilePanel("Save Level Data", Application.dataPath, "New Level Data",
                "asset"); //Open a save file panel
        if (path == "")
            return path;
        path = FileUtil.GetProjectRelativePath(path); //Get the relative path of the file
        return path;
    }
    private static List<GameObject> SpawnableObjects()
    {
        List<GameObject> spawnableObjects = new List<GameObject>(); //Create a new list of spawnable objects
        spawnableObjects.AddRange(GameObject.FindGameObjectsWithTag("Spawnable")); //Add all spawnable objects to the list
        return spawnableObjects;
    }

    private void CreateObjectDataList(List<GameObject> spawnableObjects, List<ObjectData> objectDataList)
    {
        //Loop through all spawnable objects and add their object data to the objectDataList
        foreach (GameObject spawnableObject in spawnableObjects)
        {
            ObjectData objectData = new ObjectData();
            objectData.Position = spawnableObject.transform.position;
            objectData.Rotation = spawnableObject.transform.rotation;
            objectData.Scale = spawnableObject.transform.localScale;
            objectData.Prefab = spawnableObject.GetComponent<Spawnable>().Prefab;
            objectDataList.Add(objectData);
        }
    }
    private void SpawnPrefab()
    {
        if (EditorLevelData.spawnablePrefab != null)
        {
            var gameObject = Instantiate(EditorLevelData.spawnablePrefab, EditorLevelData.spawnablePrefab.transform.position, EditorLevelData.spawnablePrefab.transform.rotation);
            gameObject.AddComponent<Spawnable>();
            var spawnable = gameObject.GetComponent<Spawnable>();
            gameObject.tag = "Spawnable";
            gameObject.name = EditorLevelData.spawnablePrefab.name;
            spawnable.Prefab = EditorLevelData.spawnablePrefab;
        }
    }
}


