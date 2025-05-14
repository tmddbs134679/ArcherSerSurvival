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

                // ????癲ル슢??????????꿔꺂????筌?????욱룏嶺???轅붽틓??????????쇨돐??????DDOL ??μ떜媛?걫??곸돥??

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
                // ?μ떝?띄몭??袁㏉떄?????????????怨쀫뎐????ル봿?? DDOL ?????밸븶????????
                Destroy(gameObject);
            }
        }
        
    }


}
