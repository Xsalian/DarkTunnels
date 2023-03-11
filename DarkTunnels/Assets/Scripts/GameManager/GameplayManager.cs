using DarkTunnels.Train;
using DarkTunnels.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DarkTunnels.GameManagment
{
    public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
    {
        [field: SerializeField]
        public int SummarySceneIndex { get; set; }

        public bool PlayerWin { get; private set; }

        protected override void Awake ()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Start ()
        {
            AttachToEvents();
        }

        private void AttachToEvents ()
        {
            TrainController.Instance.OnTrainDestoryed += HandleOnTrainDestroyed;
            TrainController.Instance.OnStationReached += HandleOnStationReached;
        }

        private void DeatchFromEvents ()
        {
            TrainController.Instance.OnTrainDestoryed -= HandleOnTrainDestroyed;
            TrainController.Instance.OnStationReached -= HandleOnStationReached;
        }

        private void HandleOnTrainDestroyed ()
        {
            DeatchFromEvents();
            PlayerWin = false;
            SceneManager.LoadScene(SummarySceneIndex);
        }

        private void HandleOnStationReached ()
        {
            DeatchFromEvents();
            PlayerWin = true;
            SceneManager.LoadScene(SummarySceneIndex);
        }
    }
}