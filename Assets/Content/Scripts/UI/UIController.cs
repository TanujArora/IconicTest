using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIController 
{
	private CharacterController characterController;
	private StarController starController;

	private TextMeshProUGUI starText;
	private TextMeshProUGUI healthText;

	private CanvasGroup ClueUI;
	private RectTransform ClueUIBox;
	private TextMeshProUGUI ClueText;
	private Button ClueUICloseButton;

	private CanvasGroup GateUI;
	private RectTransform GateUIBox;
	private TMP_InputField Value1, Value2, Value3;
	private Button Submit;
	private Button GateUICloseButton;


	public UIController(Transform UIRoot, CharacterController characterController, StarController starController)
	{
		this.characterController = characterController;
		this.starController = starController;

		starText = UIRoot.Find("GameUI/Stars/Value").GetComponent<TextMeshProUGUI>();
		healthText = UIRoot.Find("GameUI/Health/Bar/Value").GetComponent<TextMeshProUGUI>();

		ClueUI = UIRoot.Find("ClueUI").GetComponent<CanvasGroup>();
		ClueUIBox = UIRoot.Find("ClueUI/Box").GetComponent<RectTransform>();
		ClueText = UIRoot.Find("ClueUI/Box/Value").GetComponent<TextMeshProUGUI>();
		ClueUICloseButton = UIRoot.Find("ClueUI/Box/Close").GetComponent<Button>();

		GateUI = UIRoot.Find("GateUI").GetComponent<CanvasGroup>();
		GateUIBox = UIRoot.Find("GateUI/Box").GetComponent<RectTransform>();
		Value1 = UIRoot.Find("GateUI/Box/Value1").GetComponent<TMP_InputField>();
		Value2 = UIRoot.Find("GateUI/Box/Value2").GetComponent<TMP_InputField>();
		Value3 = UIRoot.Find("GateUI/Box/Value3").GetComponent<TMP_InputField>();
		GateUICloseButton = UIRoot.Find("GateUI/Box/Close").GetComponent<Button>();
		Submit = UIRoot.Find("GateUI/Box/Submit").GetComponent<Button>();


		ClueUI.alpha = 0;
		ClueUI.gameObject.SetActive(false);
		ClueUIBox.localScale = Vector3.zero;
		ClueUICloseButton.onClick.AddListener(CloseClueUI);

		GateUI.alpha = 0;
		GateUI.gameObject.SetActive(false);
		GateUIBox.localScale = Vector3.zero;
		GateUICloseButton.onClick.AddListener(() => 
		{
			CloseGateUI();
		});
		Submit.onClick.AddListener(OnGateUISubmitPressed);
	}

	public void DisplayClue(ClueCollectable clueCollectable)
	{
		characterController.disableControls = true;

		ClueUI.gameObject.SetActive(true);
		ClueText.text = clueCollectable.getClueValueString();
		ClueUI.DOFade(1, 0.5f).OnComplete(() => 
		{
			ClueUIBox.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		});
	}

	public void CloseClueUI()
	{
		ClueUIBox.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => 
		{
			ClueUI.DOFade(0, 0.3f).OnComplete(() => 
			{
				ClueUI.gameObject.SetActive(false);
				characterController.disableControls = false;
			});
		});
	}

	private int[] gatePassword;
	private IInteractableAction action;

	public void DisplayGateUI(int[] password, IInteractableAction action)
	{
		characterController.disableControls = true;

		this.gatePassword = password;
		this.action = action;

		GateUI.gameObject.SetActive(true);

		GateUI.DOFade(1, 0.5f).OnComplete(() =>
		{
			GateUIBox.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		});
	}

	public void CloseGateUI(System.Action onComplete = null)
	{
		GateUIBox.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
		{
			GateUI.DOFade(0, 0.3f).OnComplete(() =>
			{
				GateUI.gameObject.SetActive(false);
				characterController.disableControls = false;

				onComplete?.Invoke();

				this.gatePassword = null;
				this.action = null;
			});
		});

	}

	public void OnGateUISubmitPressed()
	{
		if (this.gatePassword == null)
		{
			Debug.LogError("Password not received");
			return;
		}

		if (Value1.text == gatePassword[0].ToString()
			&& Value2.text == gatePassword[1].ToString()
			&& Value3.text == gatePassword[2].ToString())
		{
			Debug.Log("Correct Password");
			CloseGateUI(() => 
			{
				action.Start();
				action = null;
				gatePassword = null;
			});
		}
		else
		{
			Debug.Log("Incorrect password!");

		}

	}
}
