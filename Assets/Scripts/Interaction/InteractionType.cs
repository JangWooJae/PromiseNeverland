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
    } //

    public string GetObjectType(){
        string ObjectType = "";

        if(isDoor){ObjectType = "Door";} 
        else if(isItem){ObjectType = "Item";}
        else if(isChar){ObjectType = "Char";}
        else if(isFurniture){ObjectType = "Furniture";}

        return ObjectType;
    } //
}
