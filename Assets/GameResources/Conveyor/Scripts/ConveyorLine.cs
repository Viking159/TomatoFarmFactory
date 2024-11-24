namespace Features.Conveyor
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Conveyor line
    /// </summary>
    [Serializable]
    public sealed class ConveyorLine
    {
        /// <summary>
        /// Conveyor line elements
        /// </summary>
        public List<ConveyorElement> conveyorElements = new List<ConveyorElement>();
    }
}