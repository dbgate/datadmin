<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="TestTrial.aspx.cs" Inherits="TestTrial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        Name:</p>
    <p>
        <asp:TextBox ID="tbxName" runat="server"></asp:TextBox>
    </p>
    <p>
        E-mail:</p>
    <p>
        <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
    </p>
    <p>
        Product:</p>
    <p>
        <asp:DropDownList ID="cbxProduct" runat="server"></asp:DropDownList>
    </p>

    <p>
    Check:</p>
    <p>
    <asp:TextBox ID="tbxCheck" runat="server" Width="260px"></asp:TextBox>
    </p>
    
    <p>
        Language:</p>
    <p>
        <asp:DropDownList ID="cbxLanguage" runat="server">
            <asp:ListItem Value="en">English</asp:ListItem>
            <asp:ListItem Value="cz">Czech</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit license" 
            onclick="btnSubmit_Click" />
    </p>
    <p>
        &nbsp;</p>
</asp:Content>

