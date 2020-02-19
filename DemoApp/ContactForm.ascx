<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.ascx.cs" Inherits="DemoApp.ContactForm" %>
    <table>
        <tr>
            <td><strong>Name </strong></td>
            <td>
                <asp:TextBox runat="server" ID="txtName" /></td>
        </tr>
        <tr>
            <td><strong>Message </strong></td>
            <td>
                <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" /></td>
        </tr>
    </table>
