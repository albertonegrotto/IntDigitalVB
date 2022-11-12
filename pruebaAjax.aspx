<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pruebaAjax.aspx.vb" Inherits="INTeatroDig.pruebaAjax" 
    Culture="es-AR"
    title="prueba Ajax"
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>


        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="Texto"></asp:TextBox>
        <asp:CalendarExtender ID="txtFechaDesde_CalendarExtender" runat="server" 
            CssClass="Texto" Enabled="True" FirstDayOfWeek="Sunday" Format="dd/MM/yyyy" 
            TargetControlID="txtFechaDesde">
        </asp:CalendarExtender>
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
            EnableScriptLocalization="true" 
            EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>        
        <br />
        <br />n
        <asp:TextBox ID="TextBox1" runat="server" CssClass="Texto"></asp:TextBox>
        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
            TargetControlID="TextBox1" 
            Mask="99 99999999-9"
            MessageValidatorTip="true" 
            MaskType="Number" 
            AcceptNegative="Left" 
            ErrorTooltipEnabled="True">
        </asp:MaskedEditExtender>
        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
            ControlExtender="MaskedEditExtender1"  
            ControlToValidate="TextBox1"   
            IsValidEmpty="False"   
            MaximumValue="12000"   
            EmptyValueMessage="Debe ingresar el CUIT o CUIL"  
            InvalidValueMessage="CUIT / CUIL inválido"  
            EmptyValueBlurredText=""   
            InvalidValueBlurredMessage=""   
            MaximumValueBlurredMessage=""   
            MinimumValueBlurredText=""  
            Display="Dynamic"   
            TooltipMessage="Ingrese su CUIT o CUIL sin espacios ni caracteres especiales, solo números" >
        </asp:MaskedEditValidator>        
        
        <br />
        <br />
        <br />

    
    </div>
    </form>
</body>
</html>


    
    </div>
    </form>
</body>
</html>
