using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.Utils;
using Services;

namespace GameLogic
{
    public class CharacterManager :Service
    {
        List<Character> characterList_ = new List<Character>();

        public void CreateCharacter() {
           
            AddCharacter(new Character());
        }
        public void AddCharacter(Character character) {
            characterList_.Add(character);
        }
        public List<Character> GetCharacterList() { 
            return characterList_;  
        }
    }
}
