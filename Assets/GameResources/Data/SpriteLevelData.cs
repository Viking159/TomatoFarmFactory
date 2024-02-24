namespace Features.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Sprites per level data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SpriteLevelData), menuName = "Features/Data/" + nameof(SpriteLevelData))]
    public sealed class SpriteLevelData : ScriptableObject
    {
        /// <summary>
        /// Sprites count
        /// </summary>
        public int SpritesCount => _sprites.Count;

        [SerializeField]
        private List<Sprite> _sprites = new List<Sprite>();

        /// <summary>
        /// Sprite by level
        /// </summary>
        /// <param name="level">level</param>
        /// <returns>sprite</returns>
        public Sprite GetSpriteByLevel(int level)
        {
            if (level >= 0 && level < _sprites.Count)
            {
                return _sprites[level];
            }
            Debug.LogError($"{nameof(SpriteLevelData)}: incorrect level value ({level})!");
            return null;
        }
    }
}