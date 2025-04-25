using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum Pages
{
	Main,
	Login
}

[RequireComponent(typeof(CanvasGroup))]
public class BasePage : MonoBehaviour
{
	[SerializeField] protected CanvasGroup canvasGroup;
	[SerializeField] private float fadeDuration = 0.5f;
	private bool isInitialized = false;

	public Pages page;

	public Pages GetPage() => page;
	public void FadeIn() => FadeInPage();
	public void FadeOut() => FadeOutPage();

	// �������� ��� duration�� �޾� ����
	// ���̵�ȿ�� �ʿ������ 0���� ����
	public void SetFadeDuration(float duration) => fadeDuration = duration;

	protected virtual void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();

		StartCoroutine(InitCoroutine());
	}

	protected virtual void OnEnable()
	{
		StartCoroutine(ReInitCoroutine());
	}

	// �ʱ�ȭ 
	// ���ö���¡ �� �����ͷε��� ���� �Ϸ�Ǹ� ����
	protected virtual IEnumerator InitCoroutine()
	{
		yield return null;

		isInitialized = true;
	}

	protected virtual IEnumerator ReInitCoroutine()
	{
		if(!isInitialized)
		{
			yield return InitCoroutine();
		}

		yield return null;
	}

	protected virtual void FadeInPage()
	{
		BeforeFadeIn();
		canvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
		{
			AfterFadeIn();
		});
	}

	protected virtual void FadeOutPage()
	{
		BeforeFadeOut();
		canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
		{
			AfterFadeOut();
		});
	}

	protected virtual void BeforeFadeIn()
	{
		canvasGroup.interactable = false;
	}

	protected virtual void BeforeFadeOut()
	{
		canvasGroup.interactable = false;
	}

	protected virtual void AfterFadeIn()
	{
		canvasGroup.interactable = true;
	}

	protected virtual void AfterFadeOut()
	{
		gameObject.SetActive(false);
	}
}
