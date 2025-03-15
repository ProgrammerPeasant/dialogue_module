# Модуль диалогов для Unity

## Описание
Этот модуль предназначен для создания диалоговой системы в Unity с возможностью отображения текста, портретов персонажей и вариантов выбора ответа.

## Функции
- 📜 Плавное отображение текста (эффект печатной машинки).
- 🎭 Отображение имени персонажа и его портрета.
- 🎭 Поддержка вариантов выбора с динамическими кнопками.
- 🔊 Воспроизведение звука печати при наборе текста.
- ✅ Гибкость в настройке диалогов через массив `DialogueStep`.

## Установка
1. Добавьте скрипт `DialogManager.cs` в ваш проект Unity.
2. Создайте пустой объект `DialogManager` в сцене и прикрепите к нему этот скрипт.
3. Настройте UI-элементы в инспекторе, добавив ссылки на:
   - `dialogueText` (TextMeshProUGUI) — поле для текста.
   - `characterName` (TextMeshProUGUI) — имя персонажа.
   - `characterImage` (Image) — портрет персонажа.
   - `choicePanel` (GameObject) — панель с вариантами выбора.
   - `choiceButtonPrefab` (GameObject) — префаб кнопки выбора.
   - `typingAudioSource` (AudioSource) — источник звука.
   - `typingSound` (AudioClip) — звук печатной машинки.

## Использование

### Запуск диалога
Чтобы запустить диалог, вызовите метод `StartDialogue()`:
```csharp
FindObjectOfType<DialogManager>().StartDialogue();
```

### Настройка диалогов
Диалоги настраиваются через массив `DialogueStep` в `DialogManager`. Каждый шаг (`DialogueStep`) может содержать:
- `name` — имя персонажа.
- `text` — текст, отображаемый на экране.
- `portrait` — спрайт персонажа.
- `hasChoices` — есть ли у шага варианты ответа.
- `choices` — список вариантов выбора (`DialogueChoice`).

Пример добавления диалога в `Inspector`:
```csharp
public DialogueStep[] dialogueSteps = new DialogueStep[]
{
    new DialogueStep { name = "Герой", text = "Привет!", hasChoices = false },
    new DialogueStep { name = "Друг", text = "Как дела?", hasChoices = true, choices = new List<DialogueChoice>
    {
        new DialogueChoice { choiceText = "Хорошо", nextStepIndex = 2 },
        new DialogueChoice { choiceText = "Плохо", nextStepIndex = 3 }
    }}
};
```

### Переход к следующему шагу
Для перехода к следующему шагу вызывайте метод `OnNextLine()` (например, при клике):
```csharp
FindObjectOfType<DialogManager>().OnNextLine();
```

## Требования
- Unity 2020+.
- TextMeshPro (включен в стандартный пакет Unity UI).

## Дополнительно
Если у вас есть вопросы или предложения, создайте issue или pull request в репозитории проекта! 🎮

