d<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="INTeatroDig._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>INTeatro Digital</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Label ID="Label1" runat="server" BackColor="#EEEEEE" 
         Text="Servicios Web INTeatro Digital" Font-Names="Arial">
      </asp:Label>
        <br /> 
    </div>
    <div>
        <asp:TextBox ID="Username" runat="server" Width="90px"></asp:TextBox>
        <br /> 
    </div>
    <div>
        <asp:CheckBox ID="Remember" runat="server" Font-Names="Arial" Font-Size="Small" 
            Text="  " />
        <br /> 
    </div>
    </form>
</body>
</html>
