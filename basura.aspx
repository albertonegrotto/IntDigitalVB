<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="basura.aspx.vb" Inherits="INTeatroDig.basura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Servicios Web - INTeatro Digital</title>22
    22
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form11" runat="server" method="post">
  
    <div id="topContent" style="text-align: center;width: 800px; background-color: #FF0000;">
        <div id="Encabezado" style="text-align: left;width: 200px; height: 70px; position:absolute; left:5px; top: 5px;">         
          <table border="1" cellpadding="1" cellspacing="0" style="border-collapse:collapse;"> 
            <tr>
             <td align="center" colspan="2" style="height: 70px ;width: 200px; text-align: center;">
                 &nbsp;
             </td>  
            </tr>    
          </table>         
        </div>                     
        <div id="Titulos" 
            style="text-align: center;width: 600px; height: 70px; position:absolute; left:210px; top: 5px;">         
          <table border="1" cellpadding="1" cellspacing="0" style="border-collapse:collapse;"> 
            <tr>
             <td align="center" colspan="2" style="height: 23px ;width: 050px; text-align: left;">
                 &nbsp;               
             </td>
             <td align="center" colspan="2" style="height: 23px ;width: 500px; text-align: center;">             
                 <asp:Label ID="Label1" runat="server" CssClass="Texto" 
                     Text="Instituto Nacional Nacional del Teatro" Font-Size="Large">
                 </asp:Label>
             </td>
             <td align="center" colspan="2" style="height: 23px ;width: 050px; text-align: left;">
                 &nbsp;               
             </td>
           </tr>
            <tr>
             <td align="center" colspan="2" style="height: 23px ;width: 050px; text-align: left;">              
                 &nbsp;
             </td>
             <td align="center" colspan="2" style="height: 23px ;width: 500px; text-align: center;">               
                 <asp:Label ID="Label2" runat="server" CssClass="Texto" 
                     Text="INTeatro Digital" Font-Size="Large">
                 </asp:Label>
             </td>
             <td align="center" colspan="2" style="height: 23px ;width: 050px; text-align: left;">
                 &nbsp;               
             </td>
           </tr>
            <tr>
             <td align="center" colspan="2" style="height: 24px ;width: 050px; text-align: left;">               
                 &nbsp;
             </td>
             <td align="center" colspan="2" style="height: 24px ;width: 500px; text-align: center;">              
                 &nbsp;
             </td>
             <td align="center" colspan="2" style="height: 24px ;width: 050px; text-align: left;">
                 &nbsp;               
             </td>
           </tr>
          </table>      
        </div>                     
    </div>
    
    <div id="mainContent" style="text-align: center;width: 640px;height: 402px;">22
        22
    </div>
  
    <div id="leftContent">
       <div id="Div1" style="text-align: left;width: 160px; height: 70px; position:absolute; left:5px; top: 0px;">           
         <table border="2" cellpadding="1" cellspacing="0" style="border-collapse:collapse"> 
           <tr>
            <td align="center" colspan="2" style="width: 160px; height: 2px; background-color: #D8E1E2  ; text-align: center;">
             <asp:Label ID="Titulo" runat="server" BackColor="White" 
                Height="20px" style="margin-left: 0px" Width="160px" Font-Bold="True" 
                    CssClass="Texto" BorderColor="#0772B1" BorderWidth="1px">Tipo de Trámite</asp:Label>
            </td>
           </tr>
         </table>      
         <table border="2" cellpadding="1" cellspacing="0" style="border-collapse:collapse"> 
           <tr>
            <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
               &nbsp;
           </td>
          </tr>
           <tr>
            <td align="center" colspan="2" style="width: 160px; height: 25px; background-color: #D8E1E2  ; text-align: center;">
                <asp:Button ID="BtnCarga" runat="server" Text="Alta de Datos Individual" Height="25px" 
                    Width="160px" BackColor="White" PostBackUrl="~/InicioIndiv.aspx" 
                    ToolTip="Alta de Datos Individual para el registro del INT" 
                    ForeColor="#0772B1" CssClass="ButtonNormal" />
            </td>    
           </tr>
           <tr>
            <td align="center" colspan="2" style="width: 160px; height: 25px; background-color: #D8E1E2  ; text-align: center;">
            </td>    
           </tr>  
           <tr>
            <td align="center" colspan="2" style="width: 160px; height: 25px; background-color: #D8E1E2  ; text-align: center;">
                <asp:Button ID="BtnConsul" runat="server" Text="Actualización Individual" Height="25px" 
                    Width="160px" BackColor="White" PostBackUrl="~/LoginInicio.aspx" 
                    ToolTip="Actualización de datos Individuales" ForeColor="#0772B1" 
                    CssClass="ButtonNormal" />
            </td>    
           </tr>  
         </table>
         <table border="2" cellpadding="1" cellspacing="0" style="border-collapse:collapse;">    
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
               &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
               &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;</td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 5px; background-color: #D8E1E2 ; text-align: center;">       
                &nbsp;
           </td>
          </tr>
          <tr>
           <td align="center" colspan="2" style="width: 160px; height: 25px; background-color: #D8E1E2 ; text-align: center;">       
               <asp:Button ID="BtnSalida" runat="server" Text="S  a  l  i  r" Height="25px" 
                   Width="160px" BackColor="#0772B1" Font-Bold="True" ForeColor="White" 
                   CssClass="ButtonNormal" />
           </td>
          </tr>
         </table>  
      </div>   
    </div>
  
    <div id="footContent" style="text-align: center;width: 800px">
       <table border="1" cellpadding="1" cellspacing="0" style="border-collapse:collapse;"> 
         <tr>      
          <td align="center" colspan="2" style="width: 800px; height: 20px; text-align: center;">
          
          </td>
         </tr>
       </table>                
    </div>
  
  </form>
</body>
</html>
