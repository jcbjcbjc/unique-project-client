using Assets.scripts.GameLogic.Models;
using Assets.scripts.Models;
using C2GNet;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.scripts.GameLogic.Managers
{
    public class CharacterManager
    {
        private static CharacterManager _instance;
        public static CharacterManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterManager();

                }
                return _instance;
            }
        }

        public List<Character> characterList;
       
        /**
         * ��ӽ�ɫ
         * @param Creature
         */
        public void AddCharacter(Character character)
        {
            this.characterList.Add(character);
        }

        /**
         * ���
         */
        public void Clear()
        {
            characterList=null;
        }

        /**
        * ������ɫ
        */
        public void CreateCharacter()
        {
            NRoom myRoom = User.Instance.room;
            if (myRoom == null)
            {
                // wssd
                return;
            }
            int index = 1;
            for (int i = 0; i < myRoom.AllTeam.Count; i++)
            {
                AllTeam team = myRoom.AllTeam[i];
                for (int j = 0; j < team.Team.Count; j++)
                {
                    RoomUser roomUser = team.Team[j];
                    CreateSingleCharacter(roomUser,index);
                    index++;
                }
                // //������ɫ
            }
        }
        private void CreateSingleCharacter(RoomUser roomUser,int index)
        {
            var obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + "Player"));
            obj.name = roomUser.NickName;
            Character character = obj.GetComponent<Character>();
            CharacterManager.Instance.AddCharacter(character);

            character.init(roomUser.UserId, roomUser.NickName, roomUser.CCharacterId, roomUser.TeamId);
        }
    }
}
   