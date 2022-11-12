<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Prueba.aspx.vb" Inherits="INTeatroDig.Prueba" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Agregar" />
        <br />
    
        <br />
        <br />
        <br />
                        <asp:GridView ID="GridView1" runat="server" 
                                AllowSorting="True"
                                AllowPaging="True" 
                                CssClass="mGrid"
                                PagerStyle-CssClass="pgr"
                                GridLines="None"
                                AlternatingRowStyle-CssClass="alt"
                                AutoGenerateColumns="False"
                                DataKeyNames = "idIntegrante" >

                            <Columns>
                                <asp:BoundField DataField="idIntegrante" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="idIntegrante" 
                                    Visible="False" ShowHeader="False">
                                    <HeaderStyle CssClass="columnaOculta" />
                                    <ItemStyle CssClass="columnaOculta" Width="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigoRegistro" HeaderText="codigoRegistro"  Visible="false"
                                    SortExpression="codigoRegistro" />
                                <asp:BoundField DataField="cuil" HeaderText="cuil" SortExpression="cuil" />
                                <asp:BoundField DataField="apellidoynombre" HeaderText="apellidoynombre" 
                                    SortExpression="apellidoynombre" />
  
                         <asp:CommandField
                            ItemStyle-Width="160"
                            ButtonType = "Image"
                            ShowEditButton = "False"
                            ShowDeleteButton = "True"
                            DeleteImageUrl = "images/btnDesvincular.gif" DeleteText="Desvincular"
                            EditImageUrl = "images/btnEdit.gif" EditText="Edit"
                            UpdateImageUrl = "images/btnUpdate.gif" UpdateText="Update" 
                            CancelText = "Cancel" CancelImageUrl = "images/btnCancel.gif" 
                            ControlStyle-Font-Bold="True" >

                            <ControlStyle Font-Bold="True"></ControlStyle>

                            <ItemStyle Width="160px"></ItemStyle>
                        </asp:CommandField>                          </Columns>

                            <PagerStyle CssClass="pgr"></PagerStyle>

                    <HeaderStyle Height="12" BackColor="#336699" ForeColor="#FFFFFF" Font-Bold="true" />
 

                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    
                </asp:GridView>

    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DeleteMethod="Delete" InsertMethod="Insert" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="INTeatroDig.dsIntegrantesTableAdapters.IntegrantesTableAdapter" 
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_idIntegrante" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="codigoRegistro" Type="Int32" />
            <asp:Parameter Name="cuil" Type="String" />
            <asp:Parameter Name="apellidoynombre" Type="String" />
            <asp:Parameter Name="Original_idIntegrante" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="codigoRegistro" Type="Int32" />
            <asp:Parameter Name="cuil" Type="String" />
            <asp:Parameter Name="apellidoynombre" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <p>
    
        <asp:Button ID="Button1" runat="server" Text="Aceptar" />
    
        </p>
    </form>
</body></html>
