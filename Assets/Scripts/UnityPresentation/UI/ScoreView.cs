using Domain.Score;
using TMPro;
using UnityEngine;

namespace UnityPresentation.UI
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private IScoreService _scoreService;

        public void Bind(IScoreService scoreService)
        {
            _scoreService = scoreService;
            _scoreService.Changed += OnScoreChanged;

            OnScoreChanged(_scoreService.Value);
        }

        public void Unbind()
        {
            if (_scoreService == null)
                return;

            _scoreService.Changed -= OnScoreChanged;
            _scoreService = null;
        }

        private void OnScoreChanged(int score)
        {
            scoreText.text = $"Score: {score}";
        }

        private void OnDestroy()
        {
            Unbind();
        }
    }
}
