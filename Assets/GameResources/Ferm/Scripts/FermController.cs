namespace Features.Ferm
{
    using Features.Data;
    using Features.Ferm.Data;
    using Features.Fruit;
    using Features.Fruit.Data;
    using Features.Spawner;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Ferm controller
    /// </summary>
    public class FermController : AbstractObjectCreator
    {
        /// <summary>
        /// Ferm name
        /// </summary>
        public string Name => fermData.Name;

        /// <summary>
        /// Fruits creation speed
        /// </summary>
        public override float Speed => fermData.Speed;

        /// <summary>
        /// Ferm level
        /// </summary>
        public int Level => fermData.Level;

        /// <summary>
        /// Ferm rang
        /// </summary>
        public int Rang => fermData.Rang;

        [SerializeField]
        protected FermData fermData = default;
        [SerializeField]
        protected FruitData fruitData = default;
        [SerializeField]
        protected BaseFruit fruitPrefab = default;

        protected float spawnTime = default;

        protected virtual void Awake()
        {
            UpdateParams();
            fermData.onDataChange += UpdateParams;
        }

        protected virtual void OnEnable()
            => StartSpawn();

        protected virtual void UpdateParams()
        {
            spawnTime = GlobalData.SPEED_CONVERT_RATIO / fermData.Speed;
            SetFruitData();
        }

        protected virtual void SetFruitData()
            => fruitData.SetLevel(fermData.Rang);

        protected virtual void StartSpawn()
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            spawnCoroutine = StartCoroutine(Spawn());
        }

        protected override IEnumerator Spawn()
        {
            float curTime;
            BaseFruit baseFruit;
            while (isActiveAndEnabled)
            {
                curTime = 0;
                while (curTime < spawnTime)
                {
                    while (!isSpawning)
                    {
                        yield return null;
                    }
                    SetProgress(curTime / spawnTime);
                    curTime += Time.deltaTime;
                    yield return null;
                }
                SetProgress(MAX_PROGRESS_VALUE);
                baseFruit = Instantiate(fruitPrefab, spawnPosition);
                baseFruit.transform.parent = fruitParent;
                baseFruit.Init(fruitData);
                NotifySpawn();
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
            => fermData.onDataChange -= UpdateParams;
    }
}