using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 1.0f;
    public float peekDistance = 2.0f;

    private Vector3 offset;
    private Vector3 targetPos;
    private Vector3 peekOffset = Vector3.zero;

    private void Awake()
    {
        peekOffset = Vector3.zero; // Сбрасываем peek при создании
        Debug.Log("CameraFollow: Awake вызван, peekOffset сброшен");
        SceneManager.sceneLoaded += OnSceneLoaded;
        Invoke(nameof(DelayedFindTarget), 0.1f);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        peekOffset = Vector3.zero;
        Debug.Log("CameraFollow: Объект уничтожен, peekOffset сброшен");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        peekOffset = Vector3.zero;
        Debug.Log("CameraFollow: Сцена загружена, peekOffset сброшен");
        Invoke(nameof(DelayedFindTarget), 0.1f);
    }

    private void DelayedFindTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            SetTarget(player.transform);
            SnapToTargetImmediately();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = (mouseWorld - target.position);
            dir.z = 0;
            if (dir.magnitude > 1f) dir = dir.normalized;
            peekOffset = dir * peekDistance;
        }
        else
        {
            peekOffset = Vector3.Lerp(peekOffset, Vector3.zero, Time.deltaTime * 7f); // плавно возвращаем
        }

        targetPos = target.position + offset + peekOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
            offset = transform.position - target.position;
    }

    public void SnapToTargetImmediately()
    {
        if (target == null) return;
        offset = transform.position - target.position;
        transform.position = target.position + offset;
    }

    public void ResetCameraState()
    {
        peekOffset = Vector3.zero;
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
