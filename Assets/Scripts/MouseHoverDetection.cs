using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverDetection : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            // 检测到鼠标悬停在物体上
            GameObject hoveredObject = hitInfo.collider.gameObject;

            if (hoveredObject.GetComponent<BuildingScript>()){
              //  hoveredObject.transform.localScale *= 1.1f;

            }// 在这里可以进行一些处理，比如变换颜色、显示信息等
            // 例如：hoveredObject.GetComponent<Renderer>().material.color = Color.red;

            Debug.Log("Mouse is hovering over: " + hoveredObject.name);
        }
        else
        {
            // 鼠标没有悬停在任何物体上
        }
    }
}
