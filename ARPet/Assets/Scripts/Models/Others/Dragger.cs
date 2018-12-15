﻿using UnityEngine;
using TMPro;
public class Dragger : MonoBehaviour, IDragger {
    public LayerMask HitLayerMask;

    private readonly float maxHitDist = 100f;
    private GameObject boxPrefab;
    IDraggable currentDrag;
    float dragDistance;
    float textTimer;
    float resetText = Mathf.Infinity;
    bool mctiShown;
    bool ttiShown;


    public float distToGrab = 0.5f;
    public TextMeshProUGUI UIText;
    public void BreakDrag() {
        currentDrag = null;
    }

    private void Awake()
    {
        boxPrefab = ResourceManager.Instance.BlockPrefab;
    }

    void Update () {

        textTimer -= Time.deltaTime;
        resetText -= Time.deltaTime;
        if(resetText < 0){
            mctiShown = false;
            ttiShown = false;
        }

        if(Input.GetKeyDown(KeyCode.B)) {
            SpawnBox();
        }

        if(currentDrag != null) {
            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.B)) {
                currentDrag.OnDragEnd();
                currentDrag = null;
            } else {
                currentDrag.OnDragContinue(transform.position + transform.forward * dragDistance, transform.rotation);
            }
        } else {
            var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            // !!!
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, maxHitDist, HitLayerMask)) {
                //Debug.Log(hitInfo.collider.name);
                var draggable = hitInfo.collider.GetComponent<IDraggable>();
                if(draggable != null) {
                    if(hitInfo.distance < distToGrab) {
                        if(!ttiShown) {
                            UIText.text = "Tap and hold interact!";
                            ttiShown = true;
                            textTimer = 5f;
                            resetText = 60f;
                        }
                    } else {
                        if(!mctiShown) {
                            UIText.text = "Move closer to interact.";
                            mctiShown = true;
                            textTimer = 5f;
                        }
                    }

                    if(Input.GetMouseButtonDown(0)) {

                        dragDistance = hitInfo.distance;
                        currentDrag = draggable;
                        currentDrag.OnDragStart(this, transform.rotation);
                    } 
                }
            } else {
                if(textTimer < 0) {
                    UIText.text = "";
                }
            }
        }
    }

    public void SpawnBox() {
        var newBox = Instantiate(boxPrefab);
        newBox.transform.position = transform.forward * 0.3f; 
        if(currentDrag != null) {
            currentDrag.OnDragStart(this, transform.rotation);
        }
    }
}