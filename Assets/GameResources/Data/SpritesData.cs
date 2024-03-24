namespace Features.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Sprites per level data
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SpritesData), menuName = "Features/Data/" + nameof(SpritesData))]
    public class SpritesData : ScriptableObject
    {
        /// <summary>
        /// Sprites count
        /// </summary>
        public virtual int SpritesCount => _sprites.Count;

        [SerializeField]
        protected  List<Sprite> _sprites = new List<Sprite>();

        /// <summary>
        /// Sprite by level
        /// </summary>
        public virtual Sprite GetSpriteByLevel(int index)
        {
            if (index >= 0 && index < _sprites.Count)
            {
                return _sprites[index];
            }
            Debug.LogError($"{nameof(SpritesData)}: incorrect index value - {index}!");
            return null;
        }
    }
}