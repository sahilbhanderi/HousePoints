<%@ Page Title="PSU Learning Factory Check-In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Welcome to the Learning Factory!</h1>
        
        <p><asp:Label runat="server" id="plsSwipe" Text="Please swipe your PSU ID to earn points."></asp:Label></p>

        
        

        <p><asp:TextBox ID="txtData" runat="server" ></asp:TextBox> </p>
        <p><asp:Label runat="server" id="Label2" Text="Output here."></asp:Label></p>
       
    </div>


</asp:Content>
