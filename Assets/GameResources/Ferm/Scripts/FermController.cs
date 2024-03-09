namespace Features.Ferm
{
    using Features.Ferm.Data;
    using Features.Fruit;
    using Features.Fruit.Data;
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Ferm controller
    /// </summary>
    public class FermController : MonoBehaviour
    {
        /// <summary>
        /// Fruit spawn event
        /// </summary>
        public event Action onFruitSpawned = delegate { };

        /// <summary>
        /// Progress value change event
        /// </summary>
        public event Action onProgressValueChange = delegate { }; 

        /// <summary>
        /// Ferm name
        /// </summary>
        public string Name => fermData.Name;

        /// <summary>
        /// Fruits creation speed
        /// </summary>
        public float Speed => fermData.Speed;

        /// <summary>
        /// Ferm level
        /// </summary>
        public int Level => fermData.Level;

        /// <summary>
        /// Ferm rang
        /// </summary>
        public int Rang => fermData.Rang;

        /// <summary>
        /// Fruit creation progress (0..1)
        /// </summary>
        public float Progress => progress;

        [SerializeField]
        protected FermData fermData = default;
        [SerializeField]
        protected FruitData fruitData = default;
        [SerializeField]
        protected BaseFruit fruitPrefab = default;
        [SerializeField]
        protected Transform spawnPosition = default;
        [SerializeField]
        protected Transform fruitParent = default;
        [SerializeField]
        protected float awaitTime = 0.1f;

        protected Coroutine spawnCoroutine = default;
        protected float progress = default;

        protected const int MAX_PROGRESS_VALUE = 1;
        protected const int SPEED_CONVERT_RATIO = 10;

        protected virtual void Awake()
        {
            SetFruitLevel();
            fermData.onDataChange += SetFruitLevel;
        }

        protected virtual void OnEnable()
            => StartSpawn();

        protected virtual void SetFruitLevel()
            => fruitData.SetLevel(fermData.Rang);

        protected virtual void StartSpawn()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            spawnCoroutine = StartCoroutine(FruitSpawner());
        }

        protected virtual IEnumerator FruitSpawner()
        {
            float curTime;
            float spawnTime = SPEED_CONVERT_RATIO / fermData.Speed;
            BaseFruit baseFruit;
            while (isActiveAndEnabled)
            {
                curTime = 0;
                while (curTime < spawnTime)
                {
                    SetProgress(curTime / spawnTime);
                    curTime += awaitTime;
                    yield return new WaitForSeconds(awaitTime);
                }
                SetProgress(MAX_PROGRESS_VALUE);
                baseFruit = Instantiate(fruitPrefab, spawnPosition);
                baseFruit.transform.parent = fruitParent;
                baseFruit.Init(fruitData);
                onFruitSpawned();
            }
        }

        protected virtual void SetProgress(float val)
        {
            float clampedVal = Mathf.Clamp01(val);
            if (progress != clampedVal)
            {
                progress = clampedVal;
                onProgressValueChange();
            }
        }

        protected virtual void OnDisable()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }

        protected virtual void OnDestroy()
            => fermData.onDataChange -= SetFruitLevel;
    }
}