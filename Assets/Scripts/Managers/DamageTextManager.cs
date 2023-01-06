using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : Singleton<DamageTextManager>
{
    public ObjectPooler Pooler { get; set; }

    private void Start()
    {
        Pooler = GetComponent<ObjectPooler>();
    }
}
