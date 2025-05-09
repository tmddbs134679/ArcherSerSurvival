using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            // 인스턴스가 없다면, 씬에서 찾아서 설정
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    // 인스턴스가 없으면 새로운 오브젝트 생성
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }

                // 생성되었거나 찾아낸 인스턴스를 DDOL에 추가
                // 이 부분을 Instance에서 다시 호출하도록 변경
                DontDestroyOnLoad(((MonoBehaviour)_instance).gameObject);
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            // 인스턴스를 이 오브젝트로 설정
            _instance = this as T;
            DontDestroyOnLoad(gameObject);  // 이 인스턴스는 DDOL에 올리기
        }
        else if (_instance != this)
        {
            // 중복된 인스턴스는 파괴
            Destroy(gameObject);
        }
    }
}
