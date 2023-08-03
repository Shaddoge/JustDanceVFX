using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.VFX;

public class CardPlayer : MonoBehaviour
{
    [Header("Card References")]
    [SerializeField] private RectTransform cardBorder;
    [SerializeField] private Image cardFill;
    [SerializeField] private Image cardBG;

    [Header("Card Values")]
    [SerializeField] private float cardOpenDuration = 1f;
    [SerializeField] private float cardFadeDuration = 1.5f;

    [Header("Character References")]
    [SerializeField] private GameObject charDisplay;
    [SerializeField] private PlayableDirector charDirector;
    [SerializeField] private Image charImageFade;

    [Header("Character Values")]
    [SerializeField] private float charInitScale = 0.75f;
    [SerializeField] private float charScaleTime = 1f;
    [SerializeField] private float charInitXOffset = -50f;

    [Header("VFX")]
    [SerializeField] private VisualEffect cardCharge;
    [SerializeField] private VisualEffect charVFX;

    private float cardOrigXSize = 0f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Initialize Cards
        cardOrigXSize = cardBorder.sizeDelta.x;
        cardBorder.sizeDelta = new Vector2(0, cardBorder.sizeDelta.y);
        Color initialColor = new Color(1, 1, 1, 0);
        cardFill.color = initialColor;
        cardBG.color = initialColor;

        // Initialize Character
        charDisplay.SetActive(false);
        charDisplay.transform.localScale = new Vector2(charInitScale, charInitScale);
        charDisplay.transform.localPosition = new Vector2(charInitXOffset, charDisplay.transform.localPosition.y);
        charImageFade.gameObject.SetActive(false);
    }

    public void Play()
    {
        // Play VFX
        cardCharge.Play();

        Sequence cardSequence = DOTween.Sequence();

        cardSequence.Append(cardBorder.DOSizeDelta(new Vector2(cardOrigXSize, 0), 0.5f).SetEase(Ease.OutSine));
        cardSequence.Append(cardFill.DOColor(Color.white, cardFadeDuration * .4f).SetEase(Ease.OutSine));
        cardSequence.Append(cardBG.DOColor(Color.white, cardFadeDuration * .6f).SetEase(Ease.OutSine));
        cardSequence.OnComplete(() => { ShowCharacter(); });
    }

    private void ShowCharacter()
    {
        charDisplay.SetActive(true);
        charDisplay.transform.DOScale(new Vector2(1, 1), charScaleTime);
        charDisplay.transform.DOLocalMoveX(0f, charScaleTime);

        charImageFade.gameObject.SetActive(true);
        charImageFade.DOFade(0f, charScaleTime).SetEase(Ease.OutSine).OnComplete(() => { charImageFade.gameObject.SetActive(false); charVFX.Play(); });
    }
}
