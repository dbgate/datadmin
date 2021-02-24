<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label runat="server" ID="labAction" EnableViewState="false" ForeColor="Green"/>
    <p>
        Name:</p>
    <p>
        <asp:TextBox ID="tbxName" runat="server" Width="259px"></asp:TextBox>
    </p>
    
    <p>
    E-mail:</p>
    <p>
    <asp:TextBox ID="tbxEmail" runat="server" Width="260px"></asp:TextBox>
    </p>

    <p>
    E-mail (where to send license, by default the same as E-mail):</p>
    <p>
    <asp:TextBox ID="tbxSendEmail" runat="server" Width="260px"></asp:TextBox>
    </p>
    
    <p>
    Check:</p>
    <p>
    <asp:TextBox ID="tbxCheck" runat="server" Width="260px"></asp:TextBox>
    </p>
    
<p>
    License:</p>
<p>
    <asp:DropDownList ID="cbxLicense" runat="server" Width="263px">
    </asp:DropDownList>
</p>

<p>
    E-mail body:</p>
<p>
    <asp:TextBox ID="tbxEmailBody" TextMode="MultiLine" runat="server" Height="293px" Width="568px"></asp:TextBox>
</p>
    <asp:Button ID="btnGenerate" runat="server" onclick="btnGenerate_Click" 
        Text="Generate license" Width="116px" />
        
        
</asp:Content>

