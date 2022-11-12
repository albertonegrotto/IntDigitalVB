<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="INTeatroDig.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td width="5%" valign="middle">&nbsp;</td>
            <td width="25%" align="right" valign="middle">Fecha de constitución: </td>
            <td width="60%" colspan="2" align="left" valign="middle">
                <asp:TextBox ID="txtFechaONG" runat="server" CssClass="Texto"
                ToolTip="Fecha de constitución de la ONG" ></asp:TextBox>
                
            </td>
            <td width="5%" valign="middle">
            </td>
            <td width="5%" valign="middle">&nbsp;</td>
        </tr>

        <tr>
            <td width="5%" valign="middle">&nbsp;</td>
            <td width="25%" align="right" valign="middle">Fecha de última acta: </td>
            <td width="60%" colspan="2" align="left" valign="middle">
                <asp:TextBox ID="txtFechaActa" runat="server" CssClass="Texto"
                tooltip="Fecha de última acta de designación de autoridades"></asp:TextBox>
                
            </td>
            <td width="5%" valign="middle">
            </td>
            <td width="5%" valign="middle">&nbsp;</td>
        </tr>
            <tr>
            <td width="5%" align="right" valign="middle">&nbsp;</td>
            <td width="25%" align="right" valign="middle"></td>
            <td width="30%" align="left" valign="middle">
            </td>
            <td width="30%" align="left" valign="middle">
                <asp:Button ID="BtnGuardar"  runat="server" Text="Guardar" CssClass="ButtonNormal" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="ButtonNormal" />
            </td>
            <td width="5%" align="left" valign="middle">
            </td>
            <td width="5%" align="left" valign="middle">&nbsp;</td>
        </tr>        
    </table>
    </div>
    </form>
</body>
</html>
