<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="EnBici.Contact" %>
<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls" TagPrefix="cc1" %>


<asp:Content ID="BodyContentConacto" ContentPlaceHolderID="MainContent" runat="server">    
<div>
<h4 style="text-align:center; margin-top:2%;">Si desea alguna otra información, darnos alguna sugerencia o contactar con nosotros, utilice el siguiente formulario:</h4>
<div style="margin-top:2%; margin-left:auto; margin-right:auto;">
    <table style="text-align:left; margin:0 auto;">
        <tr style="margin:0 auto;" >
            <td class="formulario"><strong>Título:</strong></td>
            <td style="width:300px;">
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="txt" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo" ErrorMessage="*El título es obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="formulario">
                <strong>Nombre:</strong></td>
            <td style="width:300px;">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="txt" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre" ErrorMessage="*El nombre es obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="formulario">
                <strong>Correo:</strong></td>
            <td style="width:300px;">
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="txt" Width="300px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCorreo" ErrorMessage="*El formato del correo no es válido" ForeColor="Red" validationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="formulario">
                <strong>Mensaje:</strong></td>
            <td>
                <asp:TextBox ID="txtMensaje" runat="server" Height="180px" TextMode="MultiLine" Width="300px" CssClass="txt"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMensaje" ErrorMessage="*El mensaje es obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="formulario"></td>
            <td><cc1:Recaptcha ID="Recaptcha1" runat="server" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn" onclick="btnEnviar_Click"/>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblAviso" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</div> 
</div>
</asp:Content>
