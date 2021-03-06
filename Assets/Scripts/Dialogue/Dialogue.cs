using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
    [Tooltip("대사 치는 케릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] context;

}

[System.Serializable]
public class DialogueEvent{

    public string name;
    
    public Vector2 line;
    public Dialogue[] dialogues;

}
