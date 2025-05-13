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

                // ???쒖젏?먯꽌 ?몄뒪?댁뒪??留뚮뱾?댁죱?쇰?濡?DDOL ?깅줉
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
            // 以묐났???ㅻ툕?앺듃媛 DDOL ?꾩뿉 ?뚭눼??
            Destroy(gameObject);
        }
    }


}
