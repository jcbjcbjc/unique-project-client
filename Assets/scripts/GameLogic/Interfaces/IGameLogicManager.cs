using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.scripts.Utils.enums.HandlerFrameResultEnum;

namespace Assets.scripts.GameLogic
{
    public interface IGameLogicManager
    {
        void updateLogic();
        void init();
        void OnFrameHandle(object obj);
        void OnRepairFrame(object obj);
        HandlerFrameResult HandleFrame();
        void RepairFrameRequest(HandlerFrameResult handlerFrameResult);
    }
}
