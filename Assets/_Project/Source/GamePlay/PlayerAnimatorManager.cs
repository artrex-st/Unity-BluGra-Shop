using Source.EventServices.GameEvents;
using UnityEngine;

namespace GamePlay {
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorManager : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        //private bool _isGameRunning;

        private Animator Animator => GetComponent<Animator>();
        private IEventsService _eventsService;

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Initialize()
        {
            _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
            _eventsService.AddListener<ResponseGameStateUpdateEvent>(HandlerRequestNewGameStateEvent, GetHashCode());
            _eventsService.AddListener<RequestMoveAnimationEvent>(HandlerRequestMoveAnimationEvent, GetHashCode());
        }

        private void HandlerRequestNewGameStateEvent(ResponseGameStateUpdateEvent e)
        {
            Animator.enabled = e.CurrentGameState.Equals(GameStates.GameRunning);
        }

        private void HandlerRequestMoveAnimationEvent(RequestMoveAnimationEvent e)
        {
            //TODO: set Animation blend tree here
            Animator.SetFloat(Move, e.Speed.x);
            Animator.SetBool(IsMoving, e.Speed.x != 0);
        }

        private void Dispose()
        {
            _eventsService.RemoveListener<ResponseGameStateUpdateEvent>(GetHashCode());
            _eventsService.RemoveListener<RequestMoveAnimationEvent>(GetHashCode());
        }
    }
}
