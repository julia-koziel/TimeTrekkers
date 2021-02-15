using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[AddComponentMenu("UI/TranslatableText")]
public class TranslatableText : Text
{
    public TranslatableString translatableText;
    public override string text { get => translatableText?.TranslatedString ?? ""; }
    protected override void Awake()
    {
        base.Awake();
        m_Text = text;
    }

    

# if UNITY_EDITOR
    protected override void OnValidate() 
    {
        text = translatableText?.TranslatedString ?? "";
        base.OnValidate();
    }

    protected override void Reset()
    {
        base.Reset();
        base.color = Color.black;
        var guids = AssetDatabase.FindAssets("Menlo Regular t:Font");
        if (guids.Length == 1)
        {
            base.font = (Font)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(Font));
        }
    }
# endif

    public void OnLanguageChange()
    {
        text = translatableText?.TranslatedString ?? "";
    }
}