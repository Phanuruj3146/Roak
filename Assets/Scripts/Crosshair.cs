using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Crosshair : MonoBehaviour
{
    public bool canPlace = false;
    public Camera xrCamera;
    public ARRaycastManager arRaycastMng;
    public GameObject monsterTarget;
    public GameObject playerTarget;
    public GameObject monster;
    public GameObject player;
    public Button spawnBtnUI;
    public TextMeshProUGUI spawnBtnUIText;

    private Pose placementPose;
    private bool monsterExist = false;
    private bool playerExist = false;
    // Start is called before the first frame update
    void Start()
    {
        Button spawnBtn = spawnBtnUI.GetComponent<Button>();
        spawnBtn.onClick.AddListener(InstantiateObject);

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdateTargetIndicator();

        //if (canPlace && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    if (monsterAlive == false)
        //    {
        //        InstantiateObject(monster);
        //    } else if (playerAlive == false)
        //    {
        //        InstantiateObject(player);
        //    }
        //}

        if (canPlace && Input.GetKeyUp(KeyCode.Space))
        {
            InstantiateObject();
        }
    }

    void InstantiateObject()
    {
        string type = "";
        if (monsterExist == false)
        {

            Instantiate(monster, placementPose.position, Quaternion.Euler(-90,0,0));
            type = "Monster";
            spawnBtnUIText.text = "Spawn Player";

        }
        else if (playerExist == false)
        {
            Instantiate(player, placementPose.position, placementPose.rotation);
            type = "Player";
            spawnBtnUI.gameObject.SetActive(false);
            Debug.Log("time to move!");
        }
        if (type == "Monster")
        {
            monsterExist = true;
            monsterTarget.SetActive(false);
        }
        else if (type == "Player")
        {
            playerExist = true;
            playerTarget.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        Vector3 screenCenter = xrCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        arRaycastMng.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        canPlace = hits.Count > 0;

        if (canPlace)
        {
            placementPose = hits[0].pose;

            var cameraForward = xrCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    void UpdateTargetIndicator()
    {
        if (canPlace)
        {
            if (monsterExist == false)
            {
                monsterTarget.SetActive(true);
                monsterTarget.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else if (playerExist == false)
            {
                playerTarget.SetActive(true);
                playerTarget.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
        }
        else
        {
            if (monsterExist == false)
            {
                monsterTarget.SetActive(false);
                monsterTarget.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else if (playerExist == false)
            {
                playerTarget.SetActive(false);
                playerTarget.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
        }
    }
}
