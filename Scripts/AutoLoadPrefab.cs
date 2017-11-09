using UnityEngine;

public class AutoLoadPrefab : MonoBehaviour
{
    public enum AutoLoadMoment
    {
        None,
        Awake,
        Start,
        OnEnable,
    }

    public AutoLoadMoment LoadMoment = AutoLoadMoment.Start;
    public GameObject SourcePrefab;
    public GameObject InstancePrefab;

    void Awake()
    {
        if (LoadMoment == AutoLoadMoment.Awake)
        {
            LoadSourcePrefab();
        }
    }

    void Start()
    {
        if (LoadMoment == AutoLoadMoment.Start)
        {
            LoadSourcePrefab();
        }
    }

    void OnEnable()
    {
        if (LoadMoment == AutoLoadMoment.OnEnable)
        {
            LoadSourcePrefab();
        }
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