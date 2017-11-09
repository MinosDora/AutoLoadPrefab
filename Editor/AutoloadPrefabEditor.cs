using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoLoadPrefab))]
public class AutoLoadPrefabEditor : Editor
{
    private AutoLoadPrefab autoloadPrefabObj;

    private void Awake()
    {
        autoloadPrefabObj = (AutoLoadPrefab)target;
    }

    void OnPrefabInstanceUpdated(GameObject instance)
    {
        AutoLoadPrefab[] autoLoadPrefabArray = instance.GetComponentsInChildren<AutoLoadPrefab>(true);
        if (!(autoLoadPrefabArray.Length > 0))
        {
            return;
        }
        for (int i = 0; i < autoLoadPrefabArray.Length; i++)
        {
            if (autoLoadPrefabArray[i].InstancePrefab != null)
            {
                GameObject.DestroyImmediate(autoLoadPrefabArray[i].InstancePrefab);
                autoLoadPrefabArray[i].InstancePrefab = null;
            }
            Debug.LogError("Has Delete Autoload Prefab Instances.");
        }

        PrefabUtility.prefabInstanceUpdated -= OnPrefabInstanceUpdated;
        PrefabUtility.ReplacePrefab(instance, PrefabUtility.GetPrefabParent(instance), ReplacePrefabOptions.ConnectToPrefab);
        PrefabUtility.ReconnectToLastPrefab(instance);
        Selection.activeGameObject = (GameObject)PrefabUtility.GetPrefabParent(instance);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Instance", GUILayout.Height(25), GUILayout.MinWidth(120), GUILayout.ExpandWidth(false)))
            {
                if (autoloadPrefabObj != null && autoloadPrefabObj.SourcePrefab != null && autoloadPrefabObj.InstancePrefab == null)
                {
                    autoloadPrefabObj.InstancePrefab = (GameObject)PrefabUtility.InstantiatePrefab(autoloadPrefabObj.SourcePrefab);
                    autoloadPrefabObj.InstancePrefab.transform.SetParent(autoloadPrefabObj.transform, false);
                    PrefabUtility.prefabInstanceUpdated += OnPrefabInstanceUpdated;
                }
            }
            if (GUILayout.Button("Destroy", GUILayout.Height(25), GUILayout.MinWidth(120), GUILayout.ExpandWidth(false)))
            {
                if (autoloadPrefabObj != null && autoloadPrefabObj.InstancePrefab != null)
                {
                    GameObject.DestroyImmediate(autoloadPrefabObj.InstancePrefab);
                    autoloadPrefabObj.InstancePrefab = null;
                    PrefabUtility.prefabInstanceUpdated -= OnPrefabInstanceUpdated;
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}