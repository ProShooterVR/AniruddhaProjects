using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace Anirudha.Core.Singletons
{
    public class Singleton<T> : NetworkBehaviour where T : NetworkBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        T[] foundInstances = FindObjectsOfType(typeof(T)) as T[];
                        if (foundInstances.Length > 0)
                        {
                            _instance = foundInstances[0];
                            if (foundInstances.Length > 1)
                            {
                                Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                            }
                        }
                        else
                        {
                            if (NetworkManager.Singleton && NetworkManager.Singleton.IsServer)
                            {
                                GameObject obj = new GameObject();
                                obj.name = typeof(T).Name;
                                _instance = obj.AddComponent<T>();
                                DontDestroyOnLoad(obj);
                            }
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
