using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

namespace Inventory.Model
{

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{

    [field: SerializeField]
    public bool IsStackable {get; set;}
    
    public int ID => GetInstanceID ();

    [field: SerializeField]
    public int MaxStackSize  {get; set;} = 1;

    [field:SerializeField]
    public string Name {get; set;}

    [field: SerializeField]
    [field: TextArea]
    public string Description {get; set;}

    [field: SerializeField]
    public Sprite ItemImage {get; set;}

    [field: SerializeField]
    public Animator ItemAnimator{get;set;}

}

}
