using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject clipboardObject;

    [SerializeField]
    GameObject cluesMergerObject;

    [SerializeField]
    GameObject telephoneObject;
    [SerializeField]
    GameObject descriptionFiledObject;
    [SerializeField]
    GameObject cluePrefab;
    [SerializeField]
    GameObject contactPrefab;
    [SerializeField]
    GameObject DialButton;

    [SerializeField]
    List<Sprite> spriteList;
    [SerializeField]
    List<string> spriteNamesList;
    [SerializeField]
    List<AudioClip> audioClips;


    [SerializeField]
    uint cluePadding = 8;
    [SerializeField]
    uint contactsPadding = 4;

    Caller currentCaller;
    Monologue currentMonologue;
    int currentMonologueStage;


    enum PhoneStates { Idle, Ringing, Listening, Asking, Contacts };
    PhoneStates currentPhoneState = PhoneStates.Idle;
    enum MergerStates { FirstSlotSelect, SecondSlotSelect, Unselected };
    MergerStates currentMergerState = MergerStates.Unselected;
    SortedSet<string> currentClues = new SortedSet<string>();
    SortedSet<string> currentContacts = new SortedSet<string>();
    GameObject cluesContainerObject;
    float cluesContainerObjectDefaultY;
    GameObject contactListObject;
    GameObject contactContainertObject;
    GameObject phoneSpeechTextBox;
    GameObject phoneObject;
    GameObject mergerSlot1;
    GameObject mergerSlot2;
    GameObject mergerMerge;
    GameObject mergerResult;
    float contactContainerObjectDefaultY;
    List<RingEvent> ringEventStageArea = new List<RingEvent>();

    void Start()
    {
        CluesRecepies.SetUpRecipes();

        foreach ((_, Caller c) in CallersDefinitions.callerDefs)
        {
            c.CheckCluesIntegrity();
        }

        if (spriteList.Count != spriteNamesList.Count)
            throw new Exception("Sprite list and Sprite names list doesnt match in count");

        cluesContainerObject = clipboardObject.transform.Find("CluesArea").Find("Clues").gameObject;
        cluesContainerObjectDefaultY = cluesContainerObject.transform.localPosition.y;

        contactListObject = telephoneObject.transform.Find("ContactList").gameObject;

        contactContainertObject = contactListObject.transform.Find("Contacts").gameObject;
        contactContainerObjectDefaultY = contactContainertObject.transform.localPosition.y;

        phoneSpeechTextBox = telephoneObject.transform.Find("TextBox").gameObject;

        phoneObject = telephoneObject.transform.Find("PhonePlace").gameObject;

        mergerSlot1 = cluesMergerObject.transform.GetChild(0).gameObject;
        mergerSlot2 = cluesMergerObject.transform.GetChild(1).gameObject;
        mergerMerge = cluesMergerObject.transform.GetChild(2).gameObject;
        mergerResult = cluesMergerObject.transform.GetChild(3).gameObject;



        RingEvent startEvent = new RingEvent(callsToWait: 0, secondsAfterCallsToWait: 1.5f, caller: "Supervisor", monologueIdentifier: "Initial Call");

        ringEventStageArea.Add(startEvent);

        OnCallEnd();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            AddNewClue(string.Format("Clue {0}", currentClues.Count));
    }

    void ReprintCurrentClues()
    {
        foreach (Transform t in cluesContainerObject.transform)
            Destroy(t.gameObject);

        int position = 0;
        foreach (string s in currentClues)
            AddClueToClipBoard(s, position++);
    }


    // Update is called once per frame
    void AddNewClue(string clue)
    {
        if (clue == "Game end")
            SceneManager.LoadScene("Victory");

        if (currentClues.Add(clue))
        {
            AddClueToClipBoard(clue, currentClues.Count - 1);
        }
    }
    void AddClueToClipBoard(string clue, int position)
    {
        GameObject newClueObject = Instantiate(cluePrefab);
        newClueObject.transform.Find("Text").GetComponent<TMP_Text>().SetText(clue);
        newClueObject.transform.SetParent(cluesContainerObject.transform);
        newClueObject.transform.localPosition = new Vector3(0,
                    -1 * (cluePrefab.GetComponent<RectTransform>().sizeDelta.y + cluePadding) * position
                    - cluePadding - cluePrefab.GetComponent<RectTransform>().sizeDelta.y / 2,
                    0);

        newClueObject.transform.localScale = Vector3.one;

        newClueObject.GetComponent<Button>().onClick.AddListener(() => OnClueClick(newClueObject));

    }

    public void ScrollClues(float value)
    {
        if (currentClues.Count == 0)
            return;

        float cluesAreaLength = clipboardObject.transform.Find("CluesArea").GetComponent<RectTransform>().sizeDelta.y;

        float clueListLength = cluePadding + cluePrefab.GetComponent<RectTransform>().sizeDelta.y / 2
                    + (cluePrefab.GetComponent<RectTransform>().sizeDelta.y + cluePadding) * currentClues.Count;

        ScrollInContainer(cluesContainerObject, value, cluesContainerObjectDefaultY, cluesAreaLength, clueListLength);

    }
    public void ScrollContacts(float value)
    {
        if (currentContacts.Count == 0)
            return;

        float contactsAreaLength = contactListObject.transform.GetComponent<RectTransform>().sizeDelta.y;

        float contactsListLength = contactsPadding + contactPrefab.GetComponent<RectTransform>().sizeDelta.y / 2
                    + (contactPrefab.GetComponent<RectTransform>().sizeDelta.y + contactsPadding) * currentContacts.Count;

        ScrollInContainer(contactContainertObject, value, contactContainerObjectDefaultY, contactsAreaLength, contactsListLength);

    }

    void ScrollInContainer(GameObject containerObject, float value, float defaultListY, float containerLength, float contentLength)
    {
        float sizeDifference = contentLength - containerLength;

        if (sizeDifference > 0)
        {
            float scrollDistance = sizeDifference * value;

            containerObject.transform.localPosition = new Vector3(0,
                        defaultListY + scrollDistance,
                        0);
        }
        else
        {

            containerObject.transform.localPosition = new Vector3(0,
                        defaultListY,
                        0);
        }
    }

    void ChangePhoneState(PhoneStates newState)
    {
        switch (currentPhoneState, newState)
        {
            case (PhoneStates.Idle, PhoneStates.Contacts):
                {
                    ChangeMergerState(MergerStates.Unselected);
                    SetMergerActive(false);

                    ShowContactList();
                    DialButton.GetComponentInChildren<TMP_Text>().SetText("Cancel");
                    phoneObject.GetComponent<Image>().sprite = spriteList[spriteNamesList.FindIndex(match: o => o == "PhonePickUp")];

                    break;
                }

            case (PhoneStates.Idle, PhoneStates.Ringing):
                {
                    DialButton.GetComponentInChildren<TMP_Text>().SetText("Answer");
                    break;
                }

            case (PhoneStates.Listening, PhoneStates.Idle):
            case (PhoneStates.Asking, PhoneStates.Idle):
            case (PhoneStates.Contacts, PhoneStates.Idle):
                {
                    SetMergerActive(true);
                    SetCluesActive(true);

                    HideContactList();
                    DialButton.GetComponentInChildren<TMP_Text>().SetText("Dial");

                    SayCallersLine("");
                    currentCaller = null;
                    currentMonologue = null;

                    if (currentPhoneState != PhoneStates.Contacts)
                        OnCallEnd();

                    phoneObject.GetComponent<Image>().sprite = spriteList[spriteNamesList.FindIndex(match: o => o == "PhoneIdle")];

                    break;
                }


            case (PhoneStates.Listening, PhoneStates.Asking):
                {
                    SetCluesActive(true);
                    DialButton.GetComponentInChildren<TMP_Text>().SetText("Cancel");
                    SayCallersLine(currentCaller.greetingLine);

                    break;
                }

            case (PhoneStates.Contacts, PhoneStates.Asking):
                {
                    HideContactList();
                    SetCluesActive(false);

                    // wait for 1 second

                    SayCallersLine(currentCaller.greetingLine);
                    SetCluesActive(true);
                    break;
                }

            case (PhoneStates.Ringing, PhoneStates.Listening):
            case (PhoneStates.Asking, PhoneStates.Listening):
                {

                    if (currentPhoneState == PhoneStates.Ringing)
                    {
                        phoneObject.GetComponent<Image>().sprite = spriteList[spriteNamesList.FindIndex(match: o => o == "PhonePickUp")];
                        ChangeMergerState(MergerStates.Unselected);
                        SetMergerActive(false);



                    }

                    SetCluesActive(false);

                    DialButton.GetComponentInChildren<TMP_Text>().SetText("Advance");
                    break;
                }



            default:
                throw new Exception(string.Format("Unsupported phone state transition: {0} to {1}", currentPhoneState.ToString(), newState.ToString()));
        }

        currentPhoneState = newState;
    }
    public void DialButtonPressed()
    {
        AudioClip audioPickUp = audioClips[1];
        AudioSource aSource = telephoneObject.GetComponent<AudioSource>();
        aSource.clip = audioPickUp;
        aSource.loop = false;
        aSource.Play();

        switch (currentPhoneState)
        {
            case PhoneStates.Idle:
                {
                    ChangePhoneState(PhoneStates.Contacts);
                    break;
                }

            case PhoneStates.Asking:
            case PhoneStates.Contacts:
                {
                    ChangePhoneState(PhoneStates.Idle);
                    break;
                }

            case PhoneStates.Ringing:
            case PhoneStates.Listening:
                {
                    if (currentPhoneState == PhoneStates.Ringing)
                        ChangePhoneState(PhoneStates.Listening);

                    currentMonologueStage++;

                    if (currentMonologueStage < currentMonologue.lines.Count)
                    {
                        SayCallersLine(currentMonologue.lines[currentMonologueStage]);

                        if (currentMonologue.hungUpOnMonoligueEnd && currentMonologueStage == currentMonologue.lines.Count - 1)
                            DialButton.GetComponentInChildren<TMP_Text>().SetText("Cancel");
                    }
                    else
                    {
                        foreach (string newClue in currentMonologue.cluesGotOnMonologueEnd)
                            AddNewClue(clue: newClue);

                        foreach (string newContact in currentMonologue.contactsGotOnMonologueEnd)
                            currentContacts.Add(newContact);

                        foreach (RingEvent rEvent in currentMonologue.ringEventsOnMonologueEnd)
                            ringEventStageArea.Add(rEvent);

                        if (currentMonologue.hungUpOnMonoligueEnd)
                        {
                            ChangePhoneState(PhoneStates.Idle);
                        }
                        else
                        {
                            ChangePhoneState(PhoneStates.Asking);

                        }
                        currentMonologue = null;
                    }

                    break;
                }
        }

    }

    public void ShowContactList()
    {
        contactListObject.SetActive(true);

        GameObject contactsContainer = contactListObject.transform.Find("Contacts").gameObject;

        int contactNumber = 0;
        foreach (string contact in currentContacts)
        {
            GameObject newContactObject = Instantiate(contactPrefab);
            newContactObject.transform.Find("Text").GetComponent<TMP_Text>().SetText(contact);
            newContactObject.transform.SetParent(contactsContainer.transform);
            newContactObject.transform.localPosition = new Vector3(0,
                        -1 * (contactPrefab.GetComponent<RectTransform>().sizeDelta.y + contactsPadding) * contactNumber
                        - contactsPadding - contactPrefab.GetComponent<RectTransform>().sizeDelta.y / 2,
                        0);

            newContactObject.transform.localScale = Vector3.one;

            newContactObject.GetComponent<Button>().onClick.AddListener(() => StartACall(CallersDefinitions.callerDefs[contact]));
            
            contactNumber++;
        }
    }
    public void HideContactList()
    {
        GameObject contactsContainer = contactListObject.transform.Find("Contacts").gameObject;

        foreach (Transform child in contactsContainer.transform)
        {
            Destroy(child.gameObject);

        }

        contactListObject.SetActive(false);

    }

    bool RingThePhone(string caller, string monologueIdentifier)
    {

        if (currentPhoneState == PhoneStates.Idle)
        {
            ChangePhoneState(PhoneStates.Ringing);

            currentCaller = CallersDefinitions.callerDefs[caller];
            currentMonologue = currentCaller.monologues[monologueIdentifier];
            currentMonologueStage = -1;

            AudioClip audioRinging = audioClips[0];
            AudioSource aSource = telephoneObject.GetComponent<AudioSource>();
            aSource.clip = audioRinging;
            aSource.loop = true;
            aSource.Play();

            return true;
        }

        return false;

    }

    void StartACall(Caller caller)
    {

        if (currentPhoneState == PhoneStates.Contacts)
        {
            
            AudioClip audioCalling = audioClips[5];
            AudioSource aSource = telephoneObject.GetComponent<AudioSource>();
            aSource.clip = audioCalling;
            aSource.Play();

            currentCaller = caller;

            ChangePhoneState(PhoneStates.Asking);

        }
    }
    void SayCallersLine(string line)
    {
        phoneSpeechTextBox.GetComponent<TMP_Text>().SetText(line);
    }


    void OnCallEnd()
    {
        for (int i = ringEventStageArea.Count - 1; i >= 0; i--)
        {
            if (ringEventStageArea[i].isRealised)
                ringEventStageArea.RemoveAt(i);
        }

        foreach (RingEvent rEvent in ringEventStageArea)
        {
            rEvent.callsToWait--;

            if (rEvent.callsToWait <= 0)
                StartCoroutine(RunEvent(rEvent));

        }
    }

    IEnumerator RunEvent(RingEvent ringEvent)
    {
        yield return new WaitForSeconds(ringEvent.secondsAfterCallsToWait);

        while (!RingThePhone(caller: ringEvent.caller, monologueIdentifier: ringEvent.monologueIdentifier))
        {
            yield return new WaitForSeconds(1.5f);
        }

        ringEvent.isRealised = true;
    }

    public void OnClueClick(GameObject clueObject)
    {

        AudioClip audioClue = audioClips[2];
        AudioSource aSource = clipboardObject.GetComponent<AudioSource>();
        aSource.clip = audioClue;
        aSource.Play();

        string clue = clueObject.transform.GetChild(0).GetComponent<TMP_Text>().text;

        if (currentPhoneState == PhoneStates.Asking)
        {
            if (currentCaller.monologues.ContainsKey(clue))
            {
                currentMonologue = currentCaller.monologues[clue];
                SayCallersLine(currentMonologue.lines[0]);
                currentMonologueStage = 0;

                ChangePhoneState(PhoneStates.Listening);

            }
            else
            {
                SayCallersLine(currentCaller.cluelessLine);
            }
        }

        if (currentMergerState == MergerStates.FirstSlotSelect)
        {
            mergerSlot1.transform.GetChild(0).GetComponent<TMP_Text>().SetText(clue);
            ChangeMergerState(MergerStates.Unselected);
        }

        if (currentMergerState == MergerStates.SecondSlotSelect)
        {
            mergerSlot2.transform.GetChild(0).GetComponent<TMP_Text>().SetText(clue);
            ChangeMergerState(MergerStates.Unselected);
        }

        SetDescriptionText(clue);
    }

    void SetDescriptionText(string clueName)
    {
        descriptionFiledObject.transform.GetChild(1).GetComponent<TMP_Text>().SetText(clueName);
        descriptionFiledObject.transform.GetChild(2).GetComponent<TMP_Text>().SetText(CluesDefinitions.clueDefs[clueName]);
    }

    void SetMergerActive(bool flag)
    {
        mergerSlot1.GetComponent<Button>().interactable = flag;
        mergerSlot2.GetComponent<Button>().interactable = flag;
        mergerMerge.GetComponent<Button>().interactable = flag;
    }
    void SetCluesActive(bool flag)
    {
        foreach (Transform t in cluesContainerObject.transform)
        {
            t.GetComponent<Button>().interactable = flag;
        }
    }

    void ChangeMergerState(MergerStates newState)
    {
        string selectText = "Select clue";
        switch (currentMergerState, newState)
        {
            case (MergerStates.Unselected, MergerStates.FirstSlotSelect):
                {
                    mergerSlot1.transform.GetChild(0).GetComponent<TMP_Text>().SetText(selectText);
                    break;
                }

            case (MergerStates.Unselected, MergerStates.SecondSlotSelect):
                {
                    mergerSlot2.transform.GetChild(0).GetComponent<TMP_Text>().SetText(selectText);
                    break;
                }


            case (MergerStates.SecondSlotSelect, MergerStates.Unselected):
            case (MergerStates.FirstSlotSelect, MergerStates.Unselected):
                {
                    if (mergerSlot1.transform.GetChild(0).GetComponent<TMP_Text>().text == selectText)
                        mergerSlot1.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Slot 1");
                    if (mergerSlot2.transform.GetChild(0).GetComponent<TMP_Text>().text == selectText)
                        mergerSlot2.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Slot 2");

                    GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null); ;
                    break;
                }
        }

        currentMergerState = newState;
    }

    public void OnSlot1Click()
    {
        if (currentMergerState == MergerStates.FirstSlotSelect)
            ChangeMergerState(MergerStates.Unselected);
        else
        {

            AudioClip audioSlotSelect = audioClips[4];
            AudioSource aSource = cluesMergerObject.GetComponent<AudioSource>();
            aSource.clip = audioSlotSelect;
            aSource.Play();
            
            ChangeMergerState(MergerStates.FirstSlotSelect);
        }
    }
    public void OnSlot2Click()
    {
        if (currentMergerState == MergerStates.SecondSlotSelect)
            ChangeMergerState(MergerStates.Unselected);
        else
        {

            AudioClip audioSlotSelect = audioClips[4];
            AudioSource aSource = cluesMergerObject.GetComponent<AudioSource>();
            aSource.clip = audioSlotSelect;
            aSource.Play();

            ChangeMergerState(MergerStates.SecondSlotSelect);
        }
    }
    public void OnMergeClick()
    {
        string slot1Content = mergerSlot1.transform.GetChild(0).GetComponent<TMP_Text>().text;
        string slot2Content = mergerSlot2.transform.GetChild(0).GetComponent<TMP_Text>().text;

        string combineResult = CluesRecepies.Combine(slot1Content, slot2Content);

        if (combineResult != "" && !currentClues.Contains(combineResult))
        {
            AudioClip audioMerge = audioClips[3];
            AudioSource aSource = cluesMergerObject.GetComponent<AudioSource>();
            aSource.clip = audioMerge;
            aSource.Play();

            mergerResult.transform.GetChild(0).GetComponent<TMP_Text>().SetText(combineResult);
            AddNewClue(combineResult);
            SetDescriptionText(combineResult);

            if (CluesRecepies.deleteOnCombining.ContainsKey(combineResult))
            {
                foreach (string clueToRemove in CluesRecepies.deleteOnCombining[combineResult])
                    currentClues.Remove(clueToRemove);

                ReprintCurrentClues();
            }
        }
        else
        {
            mergerResult.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Merge failed");
        }
    }

}
