using Score;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private IScoreService _scoreService;

        public void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
            _scoreService.ScoreChanged += OnScoreChanged;

            OnScoreChanged(_scoreService.Score);
        }

        private void OnScoreChanged(int score)
        {
            scoreText.text = $"Score: {score}";
        }

        private void OnDestroy()
        {
            if (_scoreService != null)
                _scoreService.ScoreChanged -= OnScoreChanged;
        }
    }
}