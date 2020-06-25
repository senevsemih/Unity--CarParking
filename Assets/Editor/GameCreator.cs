using UnityEngine;
using UnityEditor;

public class GameCreator : EditorWindow
{
    string objectBaseName = "";
    string findName = "";

    int objectID = 1;
    float objectScale;
    float spawnRadius = 5f;
    GameObject carToSpawn;
    GameObject startPoints;
    
    GameObject platform;
    Vector3 Psize;
    Vector3 Ppos = Vector3.zero;

    [MenuItem("Tool/Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GameCreator));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn new Object", EditorStyles.boldLabel);

        objectBaseName = EditorGUILayout.TextField("Base Name", objectBaseName);
        findName = EditorGUILayout.TextField("Find Name", findName);
        GameObject foundObject = GameObject.Find(findName);
        if (foundObject)
        {
            foundObject.transform.position = EditorGUILayout.Vector3Field("Location", foundObject.transform.position);
            foundObject.transform.localScale = EditorGUILayout.Vector3Field("Scale", foundObject.transform.localScale);
        }
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectScale = EditorGUILayout.Slider("Object Scale", objectScale, 0.5f, 3f);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius); 
        EditorGUILayout.Space();

        carToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", carToSpawn, typeof(GameObject), false) as GameObject;
        if (GUILayout.Button("Spawn to Object"))
        {
            SpawnObject();
        }
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        startPoints = EditorGUILayout.ObjectField("Start Points", startPoints, typeof(GameObject), true) as GameObject;
        if (GUILayout.Button("Create Start Points"))
        {
            SpawnObject();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        
        EditorGUILayout.BeginHorizontal();
        platform = EditorGUILayout.ObjectField("Platform", platform, typeof(GameObject), false) as GameObject;
        if (GUILayout.Button("Create Platform"))
        {
            platformCreate();
        }
        EditorGUILayout.EndHorizontal();
        

    }
    void SpawnObject()
    {
        if (carToSpawn == null)
        {
            Debug.LogError("assing an object to be spawn");
            return;
        }
        if(objectBaseName == string.Empty)
        {
            Debug.LogError("enter a base name for the object");
            return;
        }

        Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);

        GameObject newObject = Instantiate(carToSpawn, spawnPos, carToSpawn.transform.rotation);

        newObject.name = objectBaseName + objectID;
        newObject.transform.localScale = Vector3.one * objectScale;

        objectID++;
    }
    void platformCreate()
    {
        if (platform == null)
        {
            Debug.LogError("assing an object to be spawn");
            return;
        }
        if (objectBaseName == string.Empty)
        {
            Debug.LogError("enter a base name for the object");
            return;
        }

        GameObject platformObject = Instantiate(platform, Ppos, platform.transform.rotation);

        platformObject.name = objectBaseName;
        platformObject.transform.localScale = Vector3.one * 1;
    }
}
