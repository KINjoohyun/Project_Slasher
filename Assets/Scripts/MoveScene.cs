using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void Move(string name)
    {
        SceneManager.LoadScene(name);
    }
}