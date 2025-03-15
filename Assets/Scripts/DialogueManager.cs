using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
using TMPro;

public class DialogManager : MonoBehaviour
{
    [System.Serializable] 
    public class DialogueStep { 
        public string name; // имя персонажа
        public string text; // текст на экране
        public Sprite portrait; // спрайт персонажа
        public bool hasChoices; // есть ли у этого шага варианты выбора
        public List<DialogueChoice> choices; // список вариантов выбора (если есть)
        
    }

    [System.Serializable] 
    public class DialogueChoice { 
        public string choiceText; // надпись на кнопке выбора
        public int nextStepIndex;
    }
    
    [Header("UI Components")] public TextMeshProUGUI dialogueText;
    public Image characterImage;

    public TextMeshProUGUI characterName;

    public GameObject choicePanel;

    public GameObject choiceButtonPrefab;

    [Header("Dialog Settings")] 
    public DialogueStep[] dialogueSteps; // массив шагов диалога
    public float typingSpeed = 0.05f; // скорость печати

    [Header("Audio")] public AudioSource typingAudioSource;
    public AudioClip typingSound;

    private int currentStepIndex;
    private bool isTyping = false;

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        currentStepIndex = 0;
        ShowStep(currentStepIndex);
    }

    private void ShowStep(int stepIndex)
    {
        if (stepIndex < 0 || stepIndex >= dialogueSteps.Length)
        {
            Debug.Log("Выход за пределы массива шагов или диалог завершён.");
            return;
        }

        choicePanel.SetActive(false);
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        if (dialogueSteps[stepIndex].portrait != null)
        {
            characterName.text = dialogueSteps[stepIndex].name;
            characterImage.sprite = dialogueSteps[stepIndex].portrait;
        }

        StartCoroutine(TypeLine(dialogueSteps[stepIndex].text));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            if (typingSound != null && typingAudioSource != null)
            {
                typingAudioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (dialogueSteps[currentStepIndex].hasChoices &&
            dialogueSteps[currentStepIndex].choices.Count > 0)
        {
            ShowChoices(dialogueSteps[currentStepIndex].choices);
        }
    }
    
    public void OnNextLine()
    {
        if (isTyping)
        {
            // eсли печатается доспрочно показываю весь текст
            StopAllCoroutines();
            dialogueText.text = dialogueSteps[currentStepIndex].text;
            isTyping = false;
            if (dialogueSteps[currentStepIndex].hasChoices &&
                dialogueSteps[currentStepIndex].choices.Count > 0)
            {
                ShowChoices(dialogueSteps[currentStepIndex].choices);
            }
        }
        else
        {
            if (!dialogueSteps[currentStepIndex].hasChoices)
            {
                currentStepIndex++;
                if (currentStepIndex < dialogueSteps.Length)
                {
                    ShowStep(currentStepIndex);
                }
                else
                {
                    Debug.Log("Диалог завершён!");
                }
            }
        }
    }

    private void ShowChoices(List<DialogueChoice> choices)
    {
        choicePanel.SetActive(true);

        foreach (var choice in choices)
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choicePanel.transform);
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.choiceText;

            Button button = buttonObj.GetComponent<Button>();
            int nextIndex = choice.nextStepIndex;
            button.onClick.AddListener(() => OnChoiceSelected(nextIndex));
        }
    }

    private void OnChoiceSelected(int nextStepIndex)
    {
        choicePanel.SetActive(false);
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        currentStepIndex = nextStepIndex;
        ShowStep(currentStepIndex);
    }
    
    
}