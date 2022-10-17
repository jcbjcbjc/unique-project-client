using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public static class Global
    {
        public static readonly LayerMask slotLayer = LayerMask.GetMask("Slot");
        public static Slot selectedSlot = null;
    }
}
