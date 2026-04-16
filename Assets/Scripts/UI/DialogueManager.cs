using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    [SerializeField] private Dialogue currentDialogue;
    [SerializeField] private Image characterIcon;
    [SerializeField] private TMP_Text characterNameTxt;
    [SerializeField] private TMP_Text dialogueLineTxt;

    private int currentLine = 0;
    private Canvas canvas;
    private bool inDialogue = false;

    public UnityAction<Dialogue> onDialogueStart;
    public UnityAction<Dialogue> onDialogueEnd;


    private void Start()
    {
        canvas = GetComponent<Canvas>();

        //Desactivar canvas al inicio por si las moscas
        canvas.enabled = false;
    }

    public void BeginDialogue(Dialogue dialogue)
    {
        //Asignar el nuevo diálogo a reproducir
        currentDialogue = dialogue;
        //Reiniciar la línea actual antes de empezar un nuevo diálogo
        currentLine = 0;
        //Activar el canvas
        canvas.enabled = true;

        inDialogue = true;

        //Mostrar la primera linea de diálogo
        ShowDialogueLine();

        //Llamar al callback de diálogo iniciado
        onDialogueStart?.Invoke(dialogue);
    }

    void ShowDialogueLine()
    {
        //Actualizar el texto de la línea de diálogo
        dialogueLineTxt.text = currentDialogue.GetLineText(currentLine);
        //Actualiza el icono con el personaje que diga esta línea y con su nombre
        characterIcon.sprite = currentDialogue.GetCharacter(currentLine).icon;
        //Lo mismo con el título del nombre del personaje
        characterNameTxt.text = currentDialogue.GetCharacter(currentLine).name;
    }

    public void NextLine()
    {
        //Si ha llegado a la última línea de diálogo, se cierra
        if (currentLine >= currentDialogue.lines.Count -1)
        {
            EndDialogue();
            return;
        }

        currentLine++;
        ShowDialogueLine();
    }

    void EndDialogue()
    {
        currentDialogue = null;
        canvas.enabled = false;

        onDialogueEnd?.Invoke(currentDialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentDialogue != null)
        {
            NextLine();
        }
    }

    //Callback que se llama al cambiar de escena
    private void OnLevelWasLoaded(int level)
    {
        //Eliminar todo lo que haya guardado en los callbacks cada vez que cambie de escena
        onDialogueStart = null;
        onDialogueEnd = null;
    }
}
