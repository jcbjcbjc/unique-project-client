using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.Utils;
using C2GNet;
using NetWork;
using Services;
using UnityEngine;

namespace GameLogic
{
    public class CharacterManager :Service
    {
        List<Character> characterList_ = new List<Character>();

        EventSystem eventSystem;
        protected internal override void AfterInitailize()
        {
            base.AfterInitailize();
            eventSystem = ServiceLocator.Get<EventSystem>();
        }

        public void InitCharacters() {

            NRoom room=  ServiceLocator.Get<User>().NRoom;

            foreach (AllTeam team in room.AllTeam) {
                foreach (RoomUser roomUser in team.Team) {
                    int index = team.Team.IndexOf(roomUser);
                    CreateCharacter(roomUser,index);
                }
            }
        }
        public void CreateCharacter(RoomUser roomUser,int index) {

            //这里创造角色的方法是错误的应该加载物体再获取脚本
            GameObject player = (GameObject)Resources.Load("Prefabs/Player");
            player = Instantiate(player);
            Character character = player.GetComponent<Character>();
            //这里是正确的获取了脚本之后加上
            character.Userid = roomUser.UserId;
            character.Nickname = roomUser.NickName;
            character.Teamid = roomUser.TeamId;
            character.CCharacterId = roomUser.CCharacterId;
            character.Positionid = index;




            LogUtil.log("生成角色的队伍号是", roomUser.TeamId, "角色的userid是", roomUser.UserId);


            GameObject capture = (GameObject)Resources.Load("Prefabs/capture");
            capture = Instantiate(capture);

            //正确的
            AddCharacter(character);
        }


        public void AddCharacter(Character character) {
            characterList_.Add(character);
        }
        public List<Character> GetCharacterList() { 
            return characterList_;  
        }
        public void RemoveCharacter(Character character) { 
            if (characterList_.Contains(character))
            {
                characterList_.Remove(character);
            }
        }
    }
}
