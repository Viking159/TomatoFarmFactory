namespace Features.Conveyor
{
    using UnityEngine;

    /// <summary>
    /// Conveyor line controller
    /// </summary>
    public class ConveyorLineController : MonoBehaviour
    {
        /// <summary>
        /// Conveyor line
        /// </summary>
        public ConveyorLine ConveyorLine => conveyorLine;
        [SerializeField]
        protected ConveyorLine conveyorLine = default;

        /// <summary>
        /// Line height
        /// </summary>
        public float Height => height;
        [SerializeField, Min(0)]
        protected float height = 1f;
    }
}

