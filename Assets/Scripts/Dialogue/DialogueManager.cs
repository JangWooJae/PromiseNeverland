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

    bool isDialogue = false;

    Interactive theIC;

    void Start(){
        theIC = FindObjectOfType<Interactive>();
    }

    void Update(){
        // Dialogue 활성화 되어 있을 때 대화창을 다음으로 넘기는 기능 구현 예정
        // if(isDialogue){
        //     if(Input.GetMouseButtonDown(0)){
        //         EndDialogue();
        //     }
        // }
    }

    // 대화창 활성화
    public void ShowDialogue(){
        txt_Dialogue.text = "";
        txt_DialogueName.text = "";

        theIC.HideUI();
        SettingUI(true);        
    }

    // 대화창 비활성화
    void EndDialogue(){
        txt_Dialogue.text = "";
        txt_DialogueName.text = "";

        theIC.ShowUI();
        SettingUI(false);
    }

    // 다음 텍스트 - 지금은 다음 텍스트로 넘길 줄 몰라서 종료만 넣어둠
    public void NextText(){
        EndDialogue();
    }
    
    // 대화창 상태 변경 함수
    void SettingUI(bool p_flag){
        
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
        isDialogue = p_flag;

    }

    // 대화창 오른쪽 아래 화살표 깜빡거리게 하는 Coroutine
    // IEnumerator DialogueArrow()


}
