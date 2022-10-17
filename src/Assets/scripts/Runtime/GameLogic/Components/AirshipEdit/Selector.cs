using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class Selector : MonoBehaviour
    {
        public GameObject indicator;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)){
                Vector3 mousePos = Input.mousePosition;
                Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit,
                        Camera.main.farClipPlane, Global.slotLayer)){
                    indicator.SetActive(true);
                    indicator.transform.position = hit.transform.position;
                    indicator.transform.rotation = hit.transform.rotation;
                    Global.selectedSlot = hit.transform.GetComponent<Slot>();
                    Debug.Log("select: " + hit.transform.position + " " + Global.selectedSlot);
                } else {
                    indicator.SetActive(false);
                }
            }
        }
    }
}
