namespace Features.Spawner
{
    using Features.Conveyor;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// Spawn point controller
    /// </summary>
    public class SpawnPointController : MonoBehaviour
    {
        protected const float AWAIT_CONVEYOR_ELEMENT = 0.5f;

        [SerializeField]
        protected AbstractObjectCreator spawner = default;
        [SerializeField]
        protected float radius = 0.25f;
        [SerializeField]
        protected LayerMask layerMask = default;

        protected ConveyorElement conveyorElement = default;
        protected CancellationTokenSource cancelationTokenSource = default;

        protected virtual async void Start()
        {
            cancelationTokenSource = new CancellationTokenSource();
            await InitConveyorElement();
            if (conveyorElement == null)
            {
                Debug.LogError($"{nameof(SpawnPointController)}: Conveyor element not found!");
                enabled = false;
                return;
            }
            conveyorElement.onRidersCountChange += CheckConveyorOverload;
        }

        /// <summary>
        /// Init conveyor element with await to wait for conveyor lines init and move
        /// </summary>
        protected virtual async Task InitConveyorElement()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(AWAIT_CONVEYOR_ELEMENT), cancelationTokenSource.Token);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask, -1, 1);
                if (colliders == null || colliders.Length == 0)
                {
                    return;
                }
                if (colliders.Length == 1)
                {
                    colliders[0].TryGetComponent(out conveyorElement);
                }
                GetClosest(colliders).TryGetComponent(out conveyorElement);
            }
            catch (Exception ex)
            {
                if (cancelationTokenSource.IsCancellationRequested)
                {
                    Debug.Log($"{nameof(SpawnPointController)}: Token cancel requested");
                }
                else
                {
                    Debug.LogError($"{nameof(SpawnPointController)}: InitConveyorElement error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        protected virtual Collider2D GetClosest(Collider2D[] colliders)
        {
            float dist;
            float minDist = float.MaxValue;
            int index = 0;
            for (int i = 0; i < colliders.Length; i++)
            {
                dist = Vector2.Distance(transform.position, colliders[i].transform.position);
                if (dist < minDist)
                {
                    index = i;
                    minDist = dist;
                }
            }
            return colliders[index];
        }

        protected virtual void CheckConveyorOverload()
            => spawner.SetConveyorState(conveyorElement.RidersCount < conveyorElement.LimitRidersCount);

        protected virtual void OnDestroy()
        {
            if (conveyorElement != null)
            {
                conveyorElement.onRidersCountChange -= CheckConveyorOverload;
            }
            if (cancelationTokenSource != null)
            {
                cancelationTokenSource.Cancel();
                cancelationTokenSource.Dispose();
            }
        }
    }
}