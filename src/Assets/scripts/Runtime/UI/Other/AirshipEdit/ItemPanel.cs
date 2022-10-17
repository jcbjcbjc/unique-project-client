using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.UI;
using UI;

namespace UI
{
    
    public class ItemPanel : ItemPanelBase
    {
    
        public override void InitPanelContent(ItemBean bean)
        {
            base.InitPanelContent(bean);
            this.transform.Find("ContentPanel/Text").GetComponent<Text>().text = bean.ToString();
        }
    }
}