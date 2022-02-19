using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;
    [SerializeField] Image img_DialogueArrow;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_DialogueName;
    [SerializeField] float textDelay;

    Dialogue[] dialogues;

    bool isDialogue = false;
    bool isNext = false;

    int lineCount = 0;
    int contextCount = 0;

    Interactive theIC;

    void Start(){
        theIC = FindObjectOfType<Interactive>();
    }

    void update(){
        NextDialogue();
    }

    // 대화창 활성화
    public void ShowDialogue(Dialogue[] p_dialogues){
        txt_Dialogue.text = "";
        txt_DialogueName.text = "";

        dialogues = p_dialogues;

        theIC.HideUI();
        SettingUI(true);
        StartCoroutine(TypeWriter());
    }

    void NextDialogue(){
        if (isDialogue){
            if(isNext){
                if(Input.GetMouseButtonDown(0)){
                    isNext = false;
                    txt_Dialogue.text = "";

                    if(++contextCount < dialogues[lineCount].context.Length){
                        StartCoroutine(TypeWriter());
                    } else{
                        contextCount = 0;
                        if(++lineCount < dialogues.Length){
                            StartCoroutine(TypeWriter());
                        } else {
                            EndDialogue();
                        }
                    }
                }
            }
        }
    }

    // 대화창 비활성화
    void EndDialogue(){
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;

        theIC.ShowUI();
        SettingUI(false);
    }
    
    // 대화창 상태 변경 함수
    void SettingUI(bool p_flag){
        isDialogue = p_flag;
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);

    }

    IEnumerator TypeWriter(){
        string t_ReplaceText = dialogues[lineCount].context[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");

        // txt_Dialogue.text = t_ReplaceText;
        txt_DialogueName.text = dialogues[lineCount].name;
        for (int i = 0; i < t_ReplaceText.Length; i++){
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
        yield return null;

    }

    // 대화창 오른쪽 아래 화살표 깜빡거리게 하는 Coroutine
    // IEnumerator DialogueArrow()

}
