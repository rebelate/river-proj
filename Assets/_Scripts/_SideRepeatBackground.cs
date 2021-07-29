using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SideRepeatBackground : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;
    Vector3 startCamera;
    Vector3[] startBg;
    public Vector3 cameraSpeed;

    void Awake(){
        cam = Camera.main.transform;
        startCamera = cam.transform.position;
        
        startBg = new Vector3[backgrounds.Length];
    }

    void Start(){
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++){
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }

        for(int i = 0; i < backgrounds.Length; i++){
            startBg[i] = backgrounds[i].position;
        }
    }

    void Update(){
        for(int i = 0; i < backgrounds.Length; i++){
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            
            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        if(cam.position.x > 4000){
            cam.position = startCamera;
            for(int i = 0; i < backgrounds.Length; i++){
                backgrounds[i].position = startBg[i];
            }
        }

        previousCamPos = cam.position;

        cam.position += cameraSpeed;
    }

    // [SerializeField] private Vector2 parallaxEFfectMultiplier;
    // public bool parentMove;
    // private Transform cameraTransform;
    // private Vector3 lastCameraPosition;
    // private float textureUnitSizeX;
    // private void Start(){
    //     cameraTransform = Camera.main.transform;
    //     lastCameraPosition = cameraTransform.position;
    //     Sprite sprite = GetComponent<SpriteRenderer> ().sprite;
    //     Texture2D texture = sprite.texture;
    //     textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    // }
    // private void FixedUpdate(){
    //     if(parentMove){
    //         Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
    //         transform.parent.transform.position += new Vector3(deltaMovement.x * parallaxEFfectMultiplier.x, deltaMovement.y * parallaxEFfectMultiplier.y);
    //         lastCameraPosition = cameraTransform.position;

    //         if(Mathf.Abs(cameraTransform.position.x - transform.parent.transform.position.x / 2) >= textureUnitSizeX) {
    //             float offsetPositionX = (cameraTransform.transform.position.x - transform.parent.transform.position.x) % textureUnitSizeX;
    //             transform.parent.transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, cameraTransform.position.y);
    //         }
    //     } else {
    //         Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
    //         transform.position += new Vector3(deltaMovement.x * parallaxEFfectMultiplier.x, deltaMovement.y * parallaxEFfectMultiplier.y);
    //         lastCameraPosition = cameraTransform.position;

    //         if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX) {
    //             float offsetPositionX = (cameraTransform.transform.position.x - transform.position.x) % textureUnitSizeX;
    //             transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, cameraTransform.position.y);
    //         }
    //     }
    // }
}
