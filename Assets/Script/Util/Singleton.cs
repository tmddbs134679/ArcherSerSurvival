using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }

                // ????嶺뚮?????????癲ル슢???吏????ㅿ폍筌???꿔꺂?????????ㅼ굻??????DDOL ?嚥싲갭큔?댁쉩??

            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            if(_instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                // 嚥싳쉶瑗??꾧틚????????鍮????곗뵯???醫딆쓧? DDOL ????썹땟????????
                Destroy(gameObject);
            }
        }
        
    }


}
