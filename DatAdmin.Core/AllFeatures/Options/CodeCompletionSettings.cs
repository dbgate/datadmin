using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [SettingsPage(Name = "codecompletion", Title = "s_code_completion", Targets = SettingsTargets.All, ImageName = CoreIcons.sqlName, RequiredFeature = CodeCompletionFeature.Test)]
    public class CodeCompletionSettings : SettingsPageBase
    {
        CharacterCase2 m_keywordCase = CharacterCase2.Upper;

        [DisplayName("s_keywords")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("gui.completion.keywordcase")]
        public CharacterCase2 KeywordCase
        {
            get { return m_keywordCase; }
            set { m_keywordCase = value; }
        }

        bool m_quoteIdentifiers = false;
        [DisplayName("s_quote_identifiers")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.completion.quoteidentifiers")]
        public bool QuoteIdentifiers
        {
            get { return m_quoteIdentifiers; }
            set { m_quoteIdentifiers = value; }
        }

        bool m_addSchema = false;
        [DisplayName("s_show_schema")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.completion.addschema")]
        public bool AddSchema
        {
            get { return m_addSchema; }
            set { m_addSchema = value; }
        }

        bool m_useCompletation = true;
        [DisplayName("s_code_completion")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.completion.use")]
        public bool UseCompletion
        {
            get { return m_useCompletation; }
            set { m_useCompletation = value; }
        }
    }

    public static class SettingsPageCollection_CodeCompletion
    {
        public static CodeCompletionSettings CodeCompletion(this SettingsPageCollection col)
        {
            return (CodeCompletionSettings)col.PageByName("codecompletion");
        }
    }
}
