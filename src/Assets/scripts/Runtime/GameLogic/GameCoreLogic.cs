using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managers;
using C2BNet;
using UnityEngine;
using static Assets.scripts.Utils.enums.BattleModeEnum;
using Services;
using NetWork;

namespace GameLogic
{
    public class GameCoreLogic
    {
        public void update(IList<FrameHandle> frameHandles)
        {
            updateLogic(frameHandles);
        }
        private void updateLogic(IList<FrameHandle> frameHandles)
        {
            recordLastPos();

            var characterList = ServiceLocator.Get<CharacterManager>().GetCharacterList();


            //开启预测回滚  rocker

            //foreach (FrameHandle fh in frameHandles)
            //{
            //    if (fh.UserId == ServiceLocator.Get<User>().user.Id)
            //    {
            //        foreach (FrameHandle frameHandle in GameData.PredictedInput)
            //        {
            //            if (frameHandle.OpretionId == fh.OpretionId)
            //            {
            //                GameData.PredictedInput.Remove(frameHandle);
            //            }
            //        }
            //    }
            //}


            foreach (var character in characterList)
            {
                foreach (FrameHandle fh in frameHandles)
                {
                    if (fh.UserId == character.Userid)
                    {
                        character.update(fh);
                    }
                }
            }

            //以下是其他生物的逻辑



        }

        private void recordLastPos()
        {

        }
    }
}
