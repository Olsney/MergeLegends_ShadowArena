using UnityEditor;
using UnityEngine;

public class ConvertMesh
{
    [MenuItem("Tools/Asset to Mesh")]
    static void Convert()
    {
        Mesh mesh = Selection.activeObject as Mesh;
        if (mesh == null) return;
        
        string path = AssetDatabase.GetAssetPath(mesh);
        string newPath = path.Replace(".asset", ".mesh");
        
        AssetDatabase.CreateAsset(Object.Instantiate(mesh), newPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log("Saved: " + newPath);
    }
}