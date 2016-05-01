using UnityEngine;

namespace Mod
{
    public class Main : MonoBehaviour
    {
        public void Start()
        {
            var modObject = new GameObject();
            modObject.AddComponent<modComponent>();
            DontDestroyOnLoad(modObject);
        }
    }


    public class modComponent : MonoBehaviour
    {
        //This is where you do the code and all that jazz.
    }
}
