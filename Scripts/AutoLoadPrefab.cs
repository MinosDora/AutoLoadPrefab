using UnityEngine;

public class AutoLoadPrefab : MonoBehaviour
{
    [Tooltip("prefab source")]
    public GameObject SourcePrefab;
    [Tooltip("prefab instance, in Editor Mode , it's connected to the prefab source.")]
    public GameObject InstancePrefab;

    void Start()
    {
        LoadSourcePrefab();
    }

    public void LoadSourcePrefab()
    {
        if (SourcePrefab != null && InstancePrefab == null)
        {
            InstancePrefab = GameObject.Instantiate<GameObject>(SourcePrefab);
            InstancePrefab.transform.SetParent(this.transform, false);
        }
    }

    public void DestroyInstancePrefab()
    {
        if (InstancePrefab != null)
        {
            GameObject.DestroyImmediate(InstancePrefab);
            InstancePrefab = null;
        }
    }
}