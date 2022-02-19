using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    // DatabaseManager에 저장된 스크립트를 꺼내야함
    // 몇번째 줄까지 호출할 지 여기서 컨트롤함
    
    public Dialogue[] GetDialogue(){
        dialogue.dialogues = DatabaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        return dialogue.dialogues;
    }

}
