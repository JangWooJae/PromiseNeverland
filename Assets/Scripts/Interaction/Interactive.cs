using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactive : MonoBehaviour
{

    [SerializeField] Camera cam;

    RaycastHit hitInfo;

    [SerializeField] GameObject go_NormalAim;
    [SerializeField] GameObject go_InteractiveAim;
    [SerializeField] Text txt_TargetName;

    bool isContact = false; // 상호작용 가능 오브젝트에 에임이 걸렸는 지 여부 확인
    public static bool isInteract = false; // 상호작용 가능 오브젝트와 상호작용 했는지 여부 (상호작용 시 투사체가 날아가는 이펙트 구현시 필요)

    DialogueManager theDM;
    
    // 대화창 나올 때 에임과 다른 UI 감추기
    public void HideUI(){
        go_InteractiveAim.SetActive(false);
        go_NormalAim.SetActive(false);
    }

    // 대화 종료 시 에임이 다시 나오는 함수
    public void ShowUI(){
        go_NormalAim.SetActive(true);
        isInteract = false;
        isContact = false;
    }

    void Start(){
        theDM = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        CheckObject();
        ClickLeftBtn();
    }

    // 상호작용 가능 여부 판단하는 항목
    void CheckObject(){
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        if(Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 2)){
            Contact();
        } else {
            if(!isInteract){ // 대화창이 열려 있을 때 커서를 움직여도 에임이 안나오게 조건문 사용
                NotContact();
            }
        }
    }

    // 상호작용 가능한 오브젝트에 에임이 닿았을 때 관련 항목 활성화
    void Contact(){
        if (hitInfo.transform.CompareTag("Interaction")){
            if (!isContact){
                isContact = true;
                go_InteractiveAim.SetActive(true);
                go_NormalAim.SetActive(false);
                txt_TargetName.text = hitInfo.transform.GetComponent<InteractionType>().GetName();
            }
        } else{
            NotContact();
        }
    }

    // 상호작용 가능한 오브젝트에서 에임이 벗어났을 때 관련 항목 비활성화
    void NotContact(){
        if (isContact){
            isContact=false;
            go_InteractiveAim.SetActive(false);
            go_NormalAim.SetActive(true);
        }
    }

    // 좌클릭 상호작용
    void ClickLeftBtn(){
        if(Input.GetMouseButtonDown(0)){
            if(!isInteract){
                if(isContact){
                    Interact();
                }
            } else if(isInteract){
                if(isContact){
                    theDM.NextText();
                }
            }   
        } 
    }

    // 상호작용
    void Interact(){
        isInteract = true;

        theDM.ShowDialogue();

    }
}