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

                // ????筌믨퀣???????嶺뚮ㅎ?ц짆???⑤８痢??癲ル슢???????⑤챿??????DDOL ?濚밸Ŧ援욃ㅇ?

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
                // 濚욌꼬?댄꺇???????щ빘???됰씭肄??좊읈? DDOL ??ш끽維????????
                Destroy(gameObject);
            }
        }
        
    }


}
