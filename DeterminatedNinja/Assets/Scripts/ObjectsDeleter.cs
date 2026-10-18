using UnityEngine;

public class ObjectsDeleter: MonoBehaviour
{
    public void DeleteByTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var item in gameObjects)
        {
            Destroy(item);
        }
    }
}
