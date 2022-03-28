using UnityEngine;

namespace ScriptableObjects.BlockConfigs
{
    [CreateAssetMenu(fileName = "New GraniteBlockConfig", menuName = "Create Granite Block Config")]
    public class GraniteBlockConfig : BaseBlockConfig
    {
        [SerializeField]
        private Color blockColor;

        public Color BlockColor => blockColor;
    }
}