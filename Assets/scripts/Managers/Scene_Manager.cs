using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class Scene_Manager
    {
        public static void Load(int index) { 
            SceneManager.LoadScene(index);
        }
    }
}
