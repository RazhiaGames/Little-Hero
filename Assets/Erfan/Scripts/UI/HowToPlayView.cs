using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayView : View
{
    public RTLTextMeshPro howToPlayText;
    public Button closeBtn;
    private CancellationTokenSource _cts;


    private void OnEnable()
    {
        closeBtn.onClick.AddListener(Hide);
        
    }

    private void OnDisable()
    {
        closeBtn.onClick.RemoveAllListeners();
    }

    public void Initialize(string text, Action onHideComplete = null, bool isCloseBtn = false)
    {
        closeBtn.gameObject.SetActive(isCloseBtn);
        // Cancel any ongoing task before starting a new one
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();

        howToPlayText.text = text;
        HideAfterDelay(onHideComplete, _cts.Token).Forget();
    }

    private async UniTaskVoid HideAfterDelay(Action onHideComplete, CancellationToken token)
    {
        try
        {
            await UniTask.Delay(GS.INS.ChooseSimilarDelayAfterFinish * 2, cancellationToken: token);
            await AnimateDown();
            onHideComplete?.Invoke();
        }
        catch (OperationCanceledException)
        {
            // Task was cancelled; do nothing
        }
    }

    protected override void OnBackBtn()
    {
        _cts?.Cancel();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}