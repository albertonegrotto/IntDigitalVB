<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Constan_Publicacion.aspx.vb" Inherits="INTeatroDig.Constan_Publicacion" %>

<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Constancia de Inscripción</title>
    <meta http-equiv="Expires" content="0"/>
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Pragma" content="no-cache"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" ToolPanelView="None" 
            GroupTreeImagesFolderUrl="" HasDrillUpButton="False" HasSearchButton="False" 
            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" 
            Height="896px" PageZoomFactor="75" ReportSourceID="CrystalReportSource1" 
            ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="1168px" />
    
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Constancia_Publicacion.rpt">
            </Report>
        </CR:CrystalReportSource>
    
    </div>
    </form>
</body>
</html>
