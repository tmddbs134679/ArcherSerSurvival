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
            // �ν��Ͻ��� ���ٸ�, ������ ã�Ƽ� ����
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    // �ν��Ͻ��� ������ ���ο� ������Ʈ ����
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }

                // �����Ǿ��ų� ã�Ƴ� �ν��Ͻ��� DDOL�� �߰�
                // �� �κ��� Instance���� �ٽ� ȣ���ϵ��� ����
                DontDestroyOnLoad(((MonoBehaviour)_instance).gameObject);
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            // �ν��Ͻ��� �� ������Ʈ�� ����
            _instance = this as T;
            DontDestroyOnLoad(gameObject);  // �� �ν��Ͻ��� DDOL�� �ø���
        }
        else if (_instance != this)
        {
            // �ߺ��� �ν��Ͻ��� �ı�
            Destroy(gameObject);
        }
    }
}
