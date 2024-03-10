namespace Features.Spawner
{
    using Features.Conveyor;
    using UnityEngine;

    /// <summary>
    /// Spawn point controller
    /// </summary>
    public class SpawnPointController : MonoBehaviour
    {
        [SerializeField]
        protected AbstractObjectCreator spawner = default;
        [SerializeField]
        protected float radius = 0.25f;
        [SerializeField]
        protected LayerMask layerMask = default;

        protected ConveyorElement conveyorElement = default;

        protected virtual void Awake()
        {
            InitConveyorElement();
            if (conveyorElement == null)
            {
                enabled = false;
                return;
            }
            conveyorElement.onRidersCountChange += CheckConveyorOverload;
        }

        protected virtual void InitConveyorElement()
        {
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
        }
    }
}