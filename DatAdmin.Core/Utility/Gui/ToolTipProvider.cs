using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class ToolTipProvider
    {
        Form m_parentForm;
        CodeEditor m_editor;

        private ToolTipProvider(Form parentForm, CodeEditor editor)
        {
            m_parentForm = parentForm;
            m_editor = editor;
        }

        public static ToolTipProvider Attach(Form parentForm, CodeEditor editor)
        {
            ToolTipProvider tp = new ToolTipProvider(parentForm, editor);
            editor.ActiveTextAreaControl.TextArea.ToolTipRequest += new ICSharpCode.TextEditor.ToolTipRequestEventHandler(TextArea_ToolTipRequest);
            return tp;
        }

        static void TextArea_ToolTipRequest(object sender, ICSharpCode.TextEditor.ToolTipRequestEventArgs e)
        {
            //if (!e.ToolTipShown)
            //if (e.InDocument && !e.ToolTipShown)
            //{
            //    e.ShowToolTip("test\nxx1");
            //}
        }

        //void OnToolTipRequest(object sender, TextEditor.ToolTipRequestEventArgs e)
        //{
        //    if (e.InDocument && !e.ToolTipShown)
        //    {
        //        IExpressionFinder expressionFinder;
        //        if (MainForm.IsVisualBasic)
        //        {
        //            expressionFinder = new VBExpressionFinder();
        //        }
        //        else
        //        {
        //            expressionFinder = new CSharpExpressionFinder(mainForm.parseInformation);
        //        }
        //        ExpressionResult expression = expressionFinder.FindFullExpression(
        //            editor.Text,
        //            editor.Document.PositionToOffset(e.LogicalPosition));
        //        if (expression.Region.IsEmpty)
        //        {
        //            expression.Region = new DomRegion(e.LogicalPosition.Line + 1, e.LogicalPosition.Column + 1);
        //        }

        //        TextEditor.TextArea textArea = editor.ActiveTextAreaControl.TextArea;
        //        NRefactoryResolver resolver = new NRefactoryResolver(mainForm.myProjectContent.Language);
        //        ResolveResult rr = resolver.Resolve(expression,
        //                                            mainForm.parseInformation,
        //                                            textArea.MotherTextEditorControl.Text);
        //        string toolTipText = GetText(rr);
        //        if (toolTipText != null)
        //        {
        //            e.ShowToolTip(toolTipText);
        //        }
        //    }
        //}

        //static string GetText(ResolveResult result)
        //{
        //    if (result == null)
        //    {
        //        return null;
        //    }
        //    if (result is MixedResolveResult)
        //        return GetText(((MixedResolveResult)result).PrimaryResult);
        //    IAmbience ambience = MainForm.IsVisualBasic ? (IAmbience)new VBNetAmbience() : new CSharpAmbience();
        //    ambience.ConversionFlags = ConversionFlags.StandardConversionFlags | ConversionFlags.ShowAccessibility;
        //    if (result is MemberResolveResult)
        //    {
        //        return GetMemberText(ambience, ((MemberResolveResult)result).ResolvedMember);
        //    }
        //    else if (result is LocalResolveResult)
        //    {
        //        LocalResolveResult rr = (LocalResolveResult)result;
        //        ambience.ConversionFlags = ConversionFlags.UseFullyQualifiedTypeNames
        //            | ConversionFlags.ShowReturnType;
        //        StringBuilder b = new StringBuilder();
        //        if (rr.IsParameter)
        //            b.Append("parameter ");
        //        else
        //            b.Append("local variable ");
        //        b.Append(ambience.Convert(rr.Field));
        //        return b.ToString();
        //    }
        //    else if (result is NamespaceResolveResult)
        //    {
        //        return "namespace " + ((NamespaceResolveResult)result).Name;
        //    }
        //    else if (result is TypeResolveResult)
        //    {
        //        IClass c = ((TypeResolveResult)result).ResolvedClass;
        //        if (c != null)
        //            return GetMemberText(ambience, c);
        //        else
        //            return ambience.Convert(result.ResolvedType);
        //    }
        //    else if (result is MethodGroupResolveResult)
        //    {
        //        MethodGroupResolveResult mrr = result as MethodGroupResolveResult;
        //        IMethod m = mrr.GetMethodIfSingleOverload();
        //        if (m != null)
        //            return GetMemberText(ambience, m);
        //        else
        //            return "Overload of " + ambience.Convert(mrr.ContainingType) + "." + mrr.Name;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //static string GetMemberText(IAmbience ambience, IEntity member)
        //{
        //    StringBuilder text = new StringBuilder();
        //    if (member is IField)
        //    {
        //        text.Append(ambience.Convert(member as IField));
        //    }
        //    else if (member is IProperty)
        //    {
        //        text.Append(ambience.Convert(member as IProperty));
        //    }
        //    else if (member is IEvent)
        //    {
        //        text.Append(ambience.Convert(member as IEvent));
        //    }
        //    else if (member is IMethod)
        //    {
        //        text.Append(ambience.Convert(member as IMethod));
        //    }
        //    else if (member is IClass)
        //    {
        //        text.Append(ambience.Convert(member as IClass));
        //    }
        //    else
        //    {
        //        text.Append("unknown member ");
        //        text.Append(member.ToString());
        //    }
        //    string documentation = member.Documentation;
        //    if (documentation != null && documentation.Length > 0)
        //    {
        //        text.Append('\n');
        //        text.Append(CodeCompletionData.XmlDocumentationToText(documentation));
        //    }
        //    return text.ToString();
        //}
    }
}
