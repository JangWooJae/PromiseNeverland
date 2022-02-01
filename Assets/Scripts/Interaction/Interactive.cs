using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactive : MonoBehaviour
{

    [SerializeField] Camera cam;

    RaycastHit hitInfo;

    [SerializeField] GameObject NormalAim;
    [SerializeField] GameObject InteractiveAim;
    [SerializeField] Text txt_TargetName;

    bool isContact = false; // 상호작용 가능 오브젝트에 에임이 걸렸는 지 여부 확인
    public static bool isInteract = false; // 상호작용 가능 오브젝트와 상호작용 했는지 여부 (상호작용 시 투사체가 날아가는 이펙트 구현시 필요)

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
            NotContact();
        }
    }

    // 상호작용 가능한 오브젝트에 에임이 닿았을 때 관련 항목 활성화
    void Contact(){
        if (hitInfo.transform.CompareTag("Interaction")){
            if (!isContact){
                isContact = true;
                InteractiveAim.SetActive(true);
                NormalAim.SetActive(false);
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
            InteractiveAim.SetActive(false);
            NormalAim.SetActive(true);
        }
    }

    // 좌클릭 상호작용
    void ClickLeftBtn(){
        if(!isInteract){
            if(Input.GetMouseButtonDown(0)){
                if(isContact){
                    Interact();
                }
            }
        }
    }

    // 상호작용
    void Interact(){
        isInteract = true;

        StopCoroutine("Interaction");
        
    }
}