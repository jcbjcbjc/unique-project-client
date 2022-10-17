using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ItemPanelBase : MonoBehaviour
    {
        private List<ItemPanelBase> childList;//�����弯��
        [HideInInspector]
        public Button downArrow;//�¼�ͷ��ť
        public Sprite down, right,dot;
        public bool isOpen { get; set; }//�����忪��״̬
        private Vector2 startSize;//��ʼ��С
    
        private void Awake()
        {
            childList = new List<ItemPanelBase>();
            downArrow = this.transform.Find("ContentPanel/ArrowButton").GetComponent<Button>();
            downArrow.onClick.AddListener(() =>
            {
                if (isOpen)
                {
                    CloseChild();
                    isOpen = false;
                }
                else
                {
                    OpenChild();
                    isOpen = true;
                }
            });
            startSize = this.GetComponent<RectTransform>().sizeDelta;
            isOpen = false;
        }
    
        //��������嵽����
        private void AddChild(ItemPanelBase parentItemPanelBase)
        {
            childList.Add(parentItemPanelBase);
            if (childList.Count >= 1)
            {
                downArrow.GetComponent<Image>().sprite = right;
            }
        }
    
        /// <summary>
        /// ���ø����壬�����岻Ϊһ���˵�
        /// </summary>
        /// <param name="parentItemPanelBase"></param>
        public void SetItemParent(ItemPanelBase parentItemPanelBase)
        {
            this.transform.parent = parentItemPanelBase.transform;
            parentItemPanelBase.AddChild(this);
            this.GetComponent<VerticalLayoutGroup>().padding = new RectOffset((int)parentItemPanelBase.downArrow.GetComponent<RectTransform>().sizeDelta.x, 0, 0, 0);
            if (parentItemPanelBase.isOpen)
            {
                
                this.GetComponent<ItemPanelBase>().AddParentSize((int)this.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
            else
            {
                this.transform.gameObject.SetActive(false);        
            }
        }
    
        /// <summary>
        /// ���ø����壬������Ϊһ���˵�
        /// </summary>
        /// <param name="tran"></param>
        public void SetBaseParent(Transform tran)
        {
            this.transform.parent = tran;
        }
    
        /// <summary>
        /// ����һ������������Panel��С
        /// </summary>
        /// <param name="change"></param>
        public void UpdateRectTranSize(int change)
        {
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(startSize.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + change);
        }
        /// <summary>
        /// ���Ӹ�����߶�
        /// </summary>
        /// <param name="parentItem"></param>
        /// <param name="change"></param>
        public void AddParentSize(int change)
        {
            if (this.transform.parent.GetComponent<ItemPanelBase>() != null)
            {
                this.transform.parent.GetComponent<ItemPanelBase>().UpdateRectTranSize(change);
                this.transform.parent.GetComponent<ItemPanelBase>().AddParentSize(change);
            }
        }
    
        /// <summary>
        /// �ر��������б�
        /// </summary>
        public void CloseChild()
        {
            if (childList.Count == 0) return;
            foreach (ItemPanelBase child in childList)
            {
                child.gameObject.SetActive(false);
                child.GetComponent<ItemPanelBase>().AddParentSize(-(int)child.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
            downArrow.GetComponent<Image>().sprite = right;
        }
    
        /// <summary>
        /// ���������б�
        /// </summary>
        public void OpenChild()
        {
            if (childList.Count == 0) return;
            foreach (ItemPanelBase child in childList)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<ItemPanelBase>().AddParentSize((int)child.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
            downArrow.GetComponent<Image>().sprite = down;
        }
    
        //���Item����
        public virtual void InitPanelContent(ItemBean bean) { }
    }
}
