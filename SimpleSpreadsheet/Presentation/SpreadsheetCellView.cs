using System.Collections;
using TMPro;
using UniMob.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.SimpleSpreadsheet.Presentation
{
    public class SpreadsheetCellView : View<SpreadsheetCellState>
    {
        [SerializeField] private TMP_Text nameText = default;
        [SerializeField] private TMP_Text valueText = default;
        [SerializeField] private TMP_InputField formulaInput = default;

        [SerializeField] private Image cellImage = default;
        [SerializeField] private Color cellActiveColor = Color.yellow;
        [SerializeField] private Color cellDefaultColor = Color.white;
        [SerializeField] private float cellFadeTime = 0.5f;

        private Coroutine _colorAnimation;

        protected override void Awake()
        {
            base.Awake();

            formulaInput.onValueChanged.AddListener(OnFormulaChanged);
        }

        protected override void Render()
        {
            nameText.text = State.Name;
            valueText.text = State.Value;
            formulaInput.text = State.Formula;

            if (_colorAnimation != null)
            {
                StopCoroutine(_colorAnimation);
            }

            _colorAnimation = StartCoroutine(AnimateColor());
        }

        private IEnumerator AnimateColor()
        {
            var time = cellFadeTime;
            while (time > 0.0f)
            {
                time -= Time.deltaTime;
                cellImage.color = Color.Lerp(cellDefaultColor, cellActiveColor, time / cellFadeTime);
                yield return null;
            }
        }

        private void OnFormulaChanged(string newFormula)
        {
            State.OnFormulaChanged(newFormula);
        }
    }
}