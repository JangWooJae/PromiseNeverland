using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionType : MonoBehaviour
{
    public bool isDoor;
    public bool isItem;
    public bool isChar;
    public bool isFurniture;

    [SerializeField] string ObjectName;

    public string GetName(){
        return ObjectName;
    }
}
