<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DemoApp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <p>
        <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
        <asp:Button ID="btnSendInfo" runat="server" OnClick="btnSendInfo_Click" Text="Send Info" />
    </p>
</asp:Content>
