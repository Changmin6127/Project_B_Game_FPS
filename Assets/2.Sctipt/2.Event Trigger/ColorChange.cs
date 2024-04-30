namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class ColorChange : MonoBehaviour     //Data Field
    {
        private Color imageOriginColor;
        private Color spriteOriginColor;

        [SerializeField]
        private Image targetImage = null;

        [SerializeField]
        private SpriteRenderer targetSprite = null;
        [SerializeField]
        private float r = 0;
        [SerializeField]
        private float g = 0;
        [SerializeField]
        private float b = 0;
        [SerializeField]
        private float a = 0;
    }

    public partial class ColorChange : MonoBehaviour     //Function Field
    {
        private void Start()
        {
            imageOriginColor = targetImage.color;
            spriteOriginColor = targetSprite.color;
        }

        public void ColorChangeActive()
        {
            if (targetSprite != null)
                targetSprite.color = new Color(r, g, b, a);

            if (targetImage != null)
                targetImage.color = new Color(r, g, b, a);
        }

        public void ColorInitialize()
        {
            if (targetSprite != null)
                targetSprite.color = spriteOriginColor;

            if (targetImage != null)
                targetImage.color = imageOriginColor;
        }
    }
}