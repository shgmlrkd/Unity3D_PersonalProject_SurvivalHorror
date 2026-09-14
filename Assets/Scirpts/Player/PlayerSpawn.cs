using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform resetTransform;

    [SerializeField]
    private GameObject playerObject;

    private void Awake()
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        if (playerObject == null) return;

        Transform playerTransform = playerObject.transform;
        playerTransform.position = resetTransform.position;
        playerTransform.localRotation = resetTransform.localRotation;
    }
}