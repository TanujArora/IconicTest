using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIController 
{
	private CharacterController characterController;
	private StarController starController;

	private TextMeshProUGUI starText;
	private TextMeshProUGUI healthText;
	private Image healthSlider;

	private CanvasGroup GameStartUI;
	private CanvasGroup GameStartPanel1, GameStartPanel2;
	private Button GameStartUIStartButton, GameStartUIContinueButton;

	private CanvasGroup ClueUI;
	private RectTransform ClueUIBox;
	private TextMeshProUGUI ClueText;
	private Button ClueUICloseButton;

	private CanvasGroup GateUI;
	private RectTransform GateUIBox;
	private TMP_InputField Value1, Value2, Value3;
	private Button Submit;
	private Button GateUICloseButton;

	private CanvasGroup GameOverUI;
	private RectTransform GameOverUIBox;
	private Button GameOverUIRetryButton;

	private CanvasGroup GameEndUI;
	private RectTransform GameEndUIBox;
	private Button GameEndUIRetryButton;


	public UIController(Transform UIRoot, CharacterController characterController, StarController starController)
	{
		this.characterController = characterController;
		this.starController = starController;

		starText = UIRoot.Find("GameUI/Stars/Value").GetComponent<TextMeshProUGUI>();
		healthText = UIRoot.Find("GameUI/Health/Bar/Value").GetComponent<TextMeshProUGUI>();
		healthSlider = UIRoot.Find("GameUI/Health/Bar").GetComponent<Image>();

		GameStartUI = UIRoot.Find("GameStartUI").GetComponent<CanvasGroup>();
		GameStartPanel1 = UIRoot.Find("GameStartUI/Panel1").GetComponent<CanvasGroup>();
		GameStartPanel2 = UIRoot.Find("GameStartUI/Panel2").GetComponent<CanvasGroup>();
		GameStartUIStartButton = UIRoot.Find("GameStartUI/Panel1/Start").GetComponent<Button>();
		GameStartUIContinueButton = UIRoot.Find("GameStartUI/Panel2/Continue").GetComponent<Button>();

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

		GameOverUI = UIRoot.Find("GameOverUI").GetComponent<CanvasGroup>();
		GameOverUIBox = UIRoot.Find("GameOverUI/Box").GetComponent<RectTransform>();
		GameOverUIRetryButton = UIRoot.Find("GameOverUI/Box/Retry").GetComponent<Button>();

		GameEndUI = UIRoot.Find("GameEndUI").GetComponent<CanvasGroup>();
		GameEndUIBox = UIRoot.Find("GameEndUI/Box").GetComponent<RectTransform>();
		GameEndUIRetryButton = UIRoot.Find("GameEndUI/Box/Retry").GetComponent<Button>();

		GameStartUI.alpha = 1;
		GameStartPanel1.gameObject.SetActive(true);
		GameStartPanel2.gameObject.SetActive(false);
		GameStartPanel1.alpha = 1;
		GameStartPanel2.alpha = 0;
		GameStartUIStartButton.onClick.AddListener(OnStartPressed);
		GameStartUIContinueButton.onClick.AddListener(OnContinuePressed);


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

		GameOverUI.alpha = 0;
		GameOverUI.gameObject.SetActive(false);
		GameOverUIBox.localScale = Vector3.zero;
		GameOverUIRetryButton.onClick.AddListener(OnGameOverRetryPressed);

		GameEndUI.alpha = 0;
		GameEndUI.gameObject.SetActive(false);
		GameEndUIBox.localScale = Vector3.zero;
		GameEndUIRetryButton.onClick.AddListener(OnGameOverRetryPressed);
	}

	public void OnStartPressed()
	{
		GameStartPanel1.DOFade(0, 0.2f).OnComplete(() => 
		{
			GameStartPanel1.gameObject.SetActive(false);
			GameStartPanel2.gameObject.SetActive(true);
			GameStartPanel2.DOFade(1, 0.2f);
		});
	}

	public void OnContinuePressed()
	{
		GameStartUI.DOFade(0, 0.2f).OnComplete(() => 
		{
			GameStartUI.gameObject.SetActive(false);
			characterController.disableControls = false;
		});
	}

	public void Update()
	{
		healthSlider.fillAmount = (float)characterController.Health / characterController.MaxHealth;
		healthText.text = characterController.Health + "/" + characterController.MaxHealth;
		starText.text = starController.Star.ToString();
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
		Time.timeScale = 0;
		this.gatePassword = password;
		this.action = action;

		GateUI.gameObject.SetActive(true);

		GateUI.DOFade(1, 0.5f).OnComplete(() =>
		{
			GateUIBox.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
		}).SetUpdate(true);
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
				Time.timeScale = 1;
			}).SetUpdate(true);
		}).SetUpdate(true);

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

	public void DisplayGameOverUI()
	{
		characterController.disableControls = true;

		GameOverUI.gameObject.SetActive(true);
		GameOverUI.DOFade(1, 0.5f).OnComplete(() =>
		{
			GameOverUIBox.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		});
	}

	public void DisplayGameEndUI()
	{
		characterController.disableControls = true;
		characterController.gameObject.SetActive(false);

		GameEndUI.gameObject.SetActive(true);
		GameEndUI.DOFade(1, 0.5f).OnComplete(() =>
		{
			GameEndUIBox.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		});
	}

	public void OnGameOverRetryPressed()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
