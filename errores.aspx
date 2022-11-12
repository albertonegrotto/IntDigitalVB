<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.master" CodeBehind="errores.aspx.vb" Inherits="INTeatroDig.errores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  
    <p>
      <asp:Label ID="LabelError" runat="server" Font-Names="Arial" Font-Size="Small" >Se ha producido un Error</asp:Label>
      <br />
    </p>
    <p>
        <asp:TextBox ID="TextError" runat="server" BackColor="#F4F3F2" 
            BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
            Height="340px" ReadOnly="True" Rows="5" Width="650px" Wrap="False" 
            TextMode="MultiLine" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
    </p>   
</asp:Content>
