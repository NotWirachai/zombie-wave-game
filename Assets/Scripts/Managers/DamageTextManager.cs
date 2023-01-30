using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : Singleton<DamageTextManager>
{
    public ObjectPooler Pooler { get; set; }

    public static DamageTextManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Pooler = GetComponent<ObjectPooler>();
    }

}
