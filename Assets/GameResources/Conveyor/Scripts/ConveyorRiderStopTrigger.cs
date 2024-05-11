namespace Features.Conveyor
{
    using Features.Spawner;
    using UnityEngine;

    /// <summary>
    /// Conveyor collision on conveyor to stop
    /// </summary>
    public class ConveyorRiderStopTrigger : MonoBehaviour
    {
        [SerializeField]
        protected ConveyorRider conveyorRider = default;
        [SerializeField]
        protected AbstractSpawnObject spawnObject = default;

        protected AbstractSpawnObject collisionObject = default;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Unsubscribe();
            collisionObject = collision.GetComponent<AbstractSpawnObject>();
            if (collisionObject != null && ValidStopCondition())
            {
                collisionObject.onObjectDisable += OnCollisionObjectDisabled;
                conveyorRider.PauseRiding();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (conveyorRider.IsPaused && collision.GetComponent<AbstractSpawnObject>() != null)
            {
                OnCollisionObjectDisabled();
            }
        }

        protected virtual void OnCollisionObjectDisabled()
        {
            Unsubscribe();
            conveyorRider.ResumeRiding();
        }

        protected virtual void Unsubscribe()
        {
            if (collisionObject != null)
            {
                collisionObject.onObjectDisable -= OnCollisionObjectDisabled;
            }
        }

        protected virtual bool ValidStopCondition()
            => !conveyorRider.IsPaused
            && spawnObject.ObjectNumber > collisionObject.ObjectNumber;

        protected virtual void OnDestroy()
        {
            Unsubscribe();
        }
    }
}