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

                // ????뽰젎?癒?퐣 ?紐꾨뮞??곷뮞??筌띾슢諭??곸１???嚥?DDOL ?源낆쨯
                DontDestroyOnLoad(_instance.gameObject);
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
        else if (_instance != this)
        {
            // 餓λ쵎?????삵닏??븍뱜揶쎛 DDOL ?袁⑸퓠 ???댘??
            Destroy(gameObject);
        }
    }


}
