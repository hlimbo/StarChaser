using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
    //http://wiki.unity3d.com/index.php/Toolbox
    private static object lock_ = new object();
    private static SoundManager instance_ = null;
    private static GameObject soundGO;
    private static bool applicationIsQuitting = false;

    void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
            soundGO = gameObject;
        }
        else if (instance_ != this) //if instance already exists
            Destroy(gameObject);

        DontDestroyOnLoad(soundGO);
    }

    public static SoundManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[SoundManager] Instance '" +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (lock_)
            {
                if (instance_ == null)//create singleton object when being accessed
                {
                    //try to find in the scene a SoundManager game object if it exists
                    instance_ = FindObjectOfType<SoundManager>();
                    if ((FindObjectsOfType<SoundManager>()).Length > 1)
                    {
                        Debug.LogError("[SoundManager] Something went really wrong " +
                            " - there should never be more than 1 SoundManager!" +
                            " Reopening the scene might fix it.");
                        return instance_;
                    }
                    if (instance_ == null) //SoundManager is accessed from other script but not placed in Unity Editor
                    {
                        soundGO = new GameObject("Sound Manager");
                        instance_ = soundGO.AddComponent<SoundManager>();
                        DontDestroyOnLoad(soundGO);
                        Debug.Log("SoundManager instance created in the scene");
                    }
                    else
                        Debug.Log("SoundManager already exists in the current scene");
                }
            }

            return instance_;
        }
    }

    //apparently gameobjects in unity get destroyed in random order..
    //so we have to ensure here that any subsequent calls to this instance
    //don't instantiate another singleton object
    void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
