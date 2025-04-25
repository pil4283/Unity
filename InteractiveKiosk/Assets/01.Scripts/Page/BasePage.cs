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

	// 설정파일 등에서 duration값 받아 적용
	// 페이드효과 필요없으면 0으로 설정
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

	// 초기화 
	// 로컬라이징 등 데이터로딩이 전부 완료되면 실행
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
