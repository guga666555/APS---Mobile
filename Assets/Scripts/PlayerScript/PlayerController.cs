using PlayerConfig;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variáveis de instância;
    public EventCameraFade eventCameraFade;
    public GameObject collectableGUI;
    public RawImage fadeGUI;
    private Camera playerCamera;
    private _ObjectInteractable currentInteractable;

    [SerializeField] private AudioSource footstepsAudioSource;
    [HideInInspector] public AudioSource playerAudioSource;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public PlayerInventory playerInventory;

    // Armazena os vetores de movimentação do jogadores;
    private Vector2 currentInput;
    private Vector3 moveDirection;

    // Armazena os vetores de rotação da camera;
    private float xRotation;
    private float yRotation;

    // Define o ponto na tela de onde o raycast utilizado pelo sistema de interação irá partir;
    private Vector3 interactionPoint = new Vector3(0.5f, 0.5f, 0f);

    // Valor da gravidade (Coloquei como uma constante por segurança);
    private const float gravity = 9.81f;

    // Recebe as configurações do personagem;
    [SerializeField] private _PlayerControllerDefines defines;

    // Ponto usado de referência ao solo (Achei melhor fazer por raycast, o metodo isGrounded do character controller é uma autêntica merda);
    [SerializeField] private Transform groundChecker;

    // Joystick para controlar a camera;
    public Joystick cameraJoystick;

    // Joystick para controlar o personagem;
    public Joystick movementJoystick;

    // Intervalo entre os sons dos passos;
    private float footstepTimer = 0f;

    // Sistema de Inventario;
    [HideInInspector] public GeigerDevice geigerDevice;
    [HideInInspector] public TabletDevice tabletDevice;
    [HideInInspector] public bool GeigerEquiped, TabletEquiped;
    [SerializeField] private GameObject currentSlot;
    public Transform tabletContainer, geigerContainer;
    public List<GameObject> inventorySlots;
    private int currentSlotIndex;

    // Elementos da UI;
    [SerializeField] private GameObject interactionButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject crossHair;

    private void Awake()
    {
        collectableGUI.SetActive(false);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInventory = GetComponent<PlayerInventory>();
        playerCamera = GetComponentInChildren<Camera>();
        playerAudioSource = GetComponent<AudioSource>();
        this.CheckStartingInventory();

        PostProcessLayer postProcessing = GetComponentInChildren<PostProcessLayer>();
        if (!EventStart.enablePostProcessing) postProcessing.enabled = false;
    }

    void Update()
    {
        this.MovePlayerInput();
        this.PlayerGravity();
        this.CameraInput();
        this.InteractionCheck();
        this.FootStepsSounds();
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void MovePlayerInput()
    {
        currentInput = new Vector2(defines._moveSpeed * movementJoystick.Vertical, defines._moveSpeed * movementJoystick.Horizontal);
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
    }

    private void CameraInput()
    {
        yRotation += cameraJoystick.Horizontal * Time.deltaTime * defines._sensX;
        xRotation -= cameraJoystick.Vertical * Time.deltaTime * defines._sensY;
        xRotation = Mathf.Clamp(xRotation, -defines._maxUpperRotation, defines._maxLowerRotation);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void PlayerGravity()
    {
        if (!characterController.isGrounded)
            moveDirection.y = -gravity * 0.8f;
        else
            moveDirection.y = 0;
    }

    private void FootStepsSounds()
    {
        if (currentInput.x == 0 || currentInput.y == 0) return;
        if (!characterController.isGrounded) return;

        footstepTimer -= Time.deltaTime * characterController.velocity.magnitude;
        if (footstepTimer <= 0 && Physics.Raycast(groundChecker.position, Vector3.down, out RaycastHit hit, 0.5f, defines._groundLayer))
        {
            switch (hit.collider.tag)
            {
                case "TerrainWood": footstepsAudioSource.PlayOneShot(defines._fstepAudioWood[Random.Range(0, defines._fstepAudioWood.Length - 1)]); break;
                case "TerrainGrass": footstepsAudioSource.PlayOneShot(defines._fstepAudioGrass[Random.Range(0, defines._fstepAudioGrass.Length - 1)]); break;
                case "TerrainMetal": footstepsAudioSource.PlayOneShot(defines._fstepAudioMetal[Random.Range(0, defines._fstepAudioMetal.Length - 1)]); break;
                case "TerrainStairs": footstepsAudioSource.PlayOneShot(defines._fstepAudioStairs[Random.Range(0, defines._fstepAudioStairs.Length - 1)]); break;
                case "TerrainAsphalt": footstepsAudioSource.PlayOneShot(defines._fstepAudioAsphalt[Random.Range(0, defines._fstepAudioAsphalt.Length - 1)]); break;
                default: footstepsAudioSource.PlayOneShot(defines._fstepAudioRubber[Random.Range(0, defines._fstepAudioRubber.Length - 1)]); break;
            }
            footstepsAudioSource.pitch = Random.Range(0.9f, 1.1f);
            footstepTimer = 1.3f;
        }
    }

    public void InteractionInput()
    {
        if (currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionPoint), out RaycastHit hit, defines._interactionDistance, defines._interactionLayer))
        {
            this.CheckInventory(currentInteractable);
            currentInteractable.OnInteraction();
        }
    }

    private void InteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionPoint), out RaycastHit hit, defines._interactionDistance, defines._interactionLayer))
        {
            if (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID())
            {
                hit.collider.TryGetComponent(out currentInteractable);
                if (currentInteractable) currentInteractable.OnLookAt();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLookAway();
            currentInteractable = null;
        }
    }

    public void CheckCurrentItem()
    {
        if (currentSlot != null) 
        {
            GeigerEquiped = currentSlot.GetComponent<GeigerDevice>() ? true : false;
            TabletEquiped = currentSlot.GetComponent<TabletDevice>() ? true : false;
            geigerDevice = GeigerEquiped ? geigerDevice = currentSlot.GetComponent<GeigerDevice>() : null;
            TabletEquiped = TabletEquiped ? tabletDevice = currentSlot.GetComponent<TabletDevice>() : null;
        }
    }

    public void CheckStartingInventory()
    {
        _ObjectInteractable[] array = GetComponentsInChildren<_ObjectInteractable>();
        foreach(var i in array)
        {
            inventorySlots.Add(i.gameObject);
        }
    }

    public void CheckInventory(_ObjectInteractable objectInteractable) 
    {
        if (objectInteractable.GetComponent<_Inventory>())
        {
            _Inventory _inventory = objectInteractable.GetComponent<_Inventory>();
            _inventory.ObtainInventory(this);
            inventorySlots.Add(_inventory.gameObject);
        }
    }

    private void EquipInventory(GameObject index)
    {
        _Inventory e = index.GetComponent<_Inventory>();
        e.EquipInventory();

        foreach(var i in inventorySlots)
        {
            if(i.gameObject != index)
            {
                _Inventory f = i.GetComponent<_Inventory>();
                f.UnequipInventory();
            }
        }
        CheckCurrentItem();
    }

    public void ChangeInventorySlot()
    {
        if (inventorySlots.Count() > 1)
        {
            if (currentSlotIndex >= inventorySlots.Count())
            {
                currentSlotIndex = 1;
                currentSlot = inventorySlots[currentSlotIndex - 1];
                EquipInventory(currentSlot);
            }
            else
            {
                currentSlotIndex++;
                currentSlot = inventorySlots[currentSlotIndex - 1];
                EquipInventory(currentSlot);
            }
        }
        else if (inventorySlots.Count() == 1)
        {
            currentSlotIndex = 1;
            currentSlot = inventorySlots[currentSlotIndex - 1];
            EquipInventory(currentSlot);
        }
    }

    public void DisableGUI()
    {
        cameraJoystick.gameObject.SetActive(false);
        movementJoystick.gameObject.SetActive(false);
        interactionButton.SetActive(false);
        inventoryButton.SetActive(false);
        crossHair.SetActive(false);
        collectableGUI.SetActive(false);
    }

    public void EnableGUI()
    {
        cameraJoystick.gameObject.SetActive(true);
        movementJoystick.gameObject.SetActive(true);
        interactionButton.SetActive(true);
        inventoryButton.SetActive(true);
        crossHair.SetActive(true);
    }
}
