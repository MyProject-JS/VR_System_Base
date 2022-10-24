using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public GameObject[] goPoints;
    public float rotSpeed = 2.0f;
    public float moveSpeed = 0.8f;
    public int viewMode = 1; // forcemove = 0, lookat = 1
    private int curGoPointIdx = 0;
    private int numGoPoints = 0;
    private Transform curTransform;
    private CharacterController curCharacterController;
    private Camera curPlayerCamera;
    void Start()
    {
        curGoPointIdx = 0; // ���� GoPoint�� �ε���
        curTransform = GetComponent<Transform>(); // ���� Player�� Transform
        goPoints = GameObject.FindGameObjectsWithTag("Point"); // ������ GoPoints��
        numGoPoints = goPoints.Length; // GoPoints �� ����.
        curCharacterController = GetComponent<CharacterController>(); // ���� Player�� CharacterController ���
        curPlayerCamera = Camera.main; // ���� Camera ���

    }

    void Update()
    {
        if (viewMode == 0) MovePlayer();
        else if (viewMode == 1) LookAtMovePlayer();
    }
 
    void MovePlayer()
    {
        // Player�� ������ ���� ����
        Vector3 goDirection = goPoints[curGoPointIdx].transform.position - curTransform.position;
        // Player�� �ٶ� ������ Rot���� ���ʹϾ��� ���ؼ� ����.
        Quaternion goRoation = Quaternion.LookRotation(goDirection);
        // �ش� �������� ȸ��
        curTransform.rotation = Quaternion.Slerp(curTransform.rotation, goRoation, Time.deltaTime * rotSpeed);
        // �ش� �������� �̵�
        curTransform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    void LookAtMovePlayer()
    {
        // ���� �ٶ󺸰� �ִ� Camera�� ������ �÷��̾ �����̴� ����
        Vector3 ForwardDir = curPlayerCamera.transform.forward;
        // �ش� ������ ���� ���� 0���� �־ �����϶� ���̿� ��� ���� ���⼺ ����
        ForwardDir.y = 0;
        // ���� CharacterController�� SimpleMove�� ���ؼ� �̵�
        curCharacterController.SimpleMove(ForwardDir * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            curGoPointIdx++;
            if (curGoPointIdx == numGoPoints)
            {
                curGoPointIdx = 0;
                viewMode = 1;
            }
            Debug.Log("Trigger Enter : " + other.name);
        }
    }
}
