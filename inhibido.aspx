<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="inhibido.aspx.vb" Inherits="INTeatroDig.inhibido" 
    title="Inhibido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="tablaDatos" runat="server" class="container-fluid">
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
            <div id="divInhabil" style="width:100%; color:#610B0B; text-align:center; font-family:Lucida Sans Unicode; padding-top:10px; padding-bottom: 10px; background-color:#F7819F">
                 <label>El Titular del CUIL/CUIT ingresado se encuentra actualmente &#39;Inhabilitado&#39; por este INT</label>
            </div>
          </div>
          <div class="col-md-2 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
             <div id="centro2" class="centro" style="width:100% !important">

		        <div id="upanddown">
		           <div id="MisDatosHeader">
                       <asp:Label ID="lblNombre" class="title" runat="server" Text="Label"></asp:Label>
		           </div>
				    <div id="DivUp">
				    <div id="divcodigo" class="divdato"><%=getCodigo()%></div>
				    <div id="divpersona" class="divdato"><%=getpersona()%></div>
				    <div id="divcuit" class="divdato"><%=getCuit()%></div>
				    <div id="divsexo" class="divdato"><%=getSexo()%></div>
				    <div id="divdomicilio" class="divdato"><%=getDomicilio()%></div>
				    <div id="divcpostal" class="divdato"><%=getLocalidad()%></div>
				    <div id="divprovincia" class="divdato"><%=getProvincia()%></div>
				    <div id="divemail" class="divdato"><%=getEmail()%></div>
				    <div id="divtel" class="divdato"><%=getTel()%></div>
				    <div id="divcelu" class="divdato"><%=getCelu()%></div>
		        </div>

                <ul class="nav nav-tabs" id="maincontent" role="tablist">
                   <li class="active"><a href="#registros" role="tab" data-toggle="tab">Mis Registros</a></li>
                   <li><a href="#lao" role="tab" data-toggle="tab">Mis Vinculaciones</a></li>
                </ul><!--/.nav-tabs.content-tabs -->

                <div class="tab-content">
                    <div class="tab-pane fade in active" id="registros">
                           <asp:GridView ID="grillamisregistros" runat="server" CssClass="table table-bordered" DataSourceID="SqlDataSource1">
                           </asp:GridView>
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                               ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" 
                               SelectCommand="SELECT  sector [Tipo de Registro], denominacion,convert(varchar, fechAlta, 103) [Fecha de Registro] 
                                                FROM registro WHERE responsable = cast(@codigo as integer) AND fechBaja IS NULL ORDER BY codigo">
                              <SelectParameters>
                                 <asp:SessionParameter Name="codigo" SessionField="codigo" />
                              </SelectParameters>
                           </asp:SqlDataSource>
                    </div><!--/.tab-pane -->

                    <div class="tab-pane fade" id="vinculaciones">
                           <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" >
                                   <Columns>
                                      <asp:BoundField DataField="IDregisdig" HeaderText="IDregisdig" 
                                           InsertVisible="False" ReadOnly="True" SortExpression="IDregisdig" Visible="false"/>
                                      <asp:BoundField DataField="IDIntegrante" HeaderText="IDIntegrante" 
                                           InsertVisible="False" ReadOnly="True" SortExpression="IDIntegrante" Visible="false" />
                                      <asp:BoundField DataField="TipoRegistro" HeaderText="TipoRegistro" 
                                           SortExpression="TipoRegistro" />
                                      <asp:BoundField DataField="IDRegistro" HeaderText="IDRegistro" 
                                           InsertVisible="False" ReadOnly="True" SortExpression="IDRegistro" Visible="false"/>
                                      <asp:BoundField DataField="Denominacion" HeaderText="Denominacion" 
                                           SortExpression="Denominacion" />
                                      <asp:BoundField DataField="Alta" HeaderText="Fecha de Vinculación" SortExpression="Alta" />           
                                   </Columns>
                           </asp:GridView>  
                           <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:INTeatroDig %>"
                                SelectCommand="select d.CODIGO IDregisdig, a.IDIntegrante, c.Descrip TipoRegistro, b.codigo IDRegistro, b.denominacion Denominacion , b.FechAlta Alta
                                from integrantes a inner join registro b on a.codigoRegistro = b.CODIGO inner join sectores c on b.sector = c.codigo 
                                inner join regisdig d on a.CUIL = d.CUIL where d.Codigo=@id  and a.fechaBaja is null"
                                DeleteCommand = "DELETE FROM IntegrantesTemp WHERE idIntegrante = @idIntegrante"
                                CancelSelectOnNullParameter="false">
                                <SelectParameters>
                                    <asp:SessionParameter Name="id" SessionField="codigo" Type="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="idIntegrante" Type="Int32" />
                                </DeleteParameters>
                           </asp:SqlDataSource>
                    </div><!--/.tab-pane -->

                </div><!--/.tab-content -->

<%--                <div id="DivDown">
			        <div id="tabs" class="tab-content">    
					    <ul>        
						    <li><a href="#tabs-1">Mis Registros</a></li>        
						    <li><a href="#tabs-2">Mis Vinculaciones</a></li>        
					    </ul>    
					    <div id="tabs-1" class="tab-pane active">  
                           <asp:GridView ID="grillamisregistros" runat="server" CssClass="table table-bordered" DataSourceID="SqlDataSource1">
                           </asp:GridView>
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                               ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" 
                               SelectCommand="SELECT  sector [Tipo de Registro], denominacion,convert(varchar, fechAlta, 103) [Fecha de Registro] 
                                                FROM registro WHERE responsable = cast(@codigo as integer) AND fechBaja IS NULL ORDER BY codigo">
                              <SelectParameters>
                                 <asp:SessionParameter Name="codigo" SessionField="codigo" />
                              </SelectParameters>
                           </asp:SqlDataSource>
					    </div>    
					    <div id="tabs-2" class="tab-pane">      
                           <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" >
                                   <Columns>
                                      <asp:BoundField DataField="IDregisdig" HeaderText="IDregisdig" 
                                           InsertVisible="False" ReadOnly="True" SortExpression="IDregisdig" Visible="false"/>
                                      <asp:BoundField DataField="IDIntegrante" HeaderText="IDIntegrante" 
                                           InsertVisible="False" ReadOnly="True" SortExpression="IDIntegrante" Visible="false" />
                                      <asp:BoundField DataField="TipoRegistro" HeaderText="TipoRegistro" 
                                           SortExpression="TipoRegistro" />
                                      <asp:BoundField DataField="IDRegistro" HeaderText="IDRegistro" 
                                           InsertVisible="False" ReadOnly="True" SortExpression="IDRegistro" Visible="false"/>
                                      <asp:BoundField DataField="Denominacion" HeaderText="Denominacion" 
                                           SortExpression="Denominacion" />
                                      <asp:BoundField DataField="Alta" HeaderText="Fecha de Vinculación" SortExpression="Alta" />           
                                   </Columns>
                           </asp:GridView>  
                           <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:INTeatroDig %>"
                                SelectCommand="select d.CODIGO IDregisdig, a.IDIntegrante, c.Descrip TipoRegistro, b.codigo IDRegistro, b.denominacion Denominacion , b.FechAlta Alta
                                from integrantes a inner join registro b on a.codigoRegistro = b.CODIGO inner join sectores c on b.sector = c.codigo 
                                inner join regisdig d on a.CUIL = d.CUIL where d.Codigo=@id  and a.fechaBaja is null"
                                DeleteCommand = "DELETE FROM IntegrantesTemp WHERE idIntegrante = @idIntegrante"
                                CancelSelectOnNullParameter="false">
                                <SelectParameters>
                                    <asp:SessionParameter Name="id" SessionField="codigo" Type="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="idIntegrante" Type="Int32" />
                                </DeleteParameters>
                           </asp:SqlDataSource>
					    </div>    
				      </div>
			       </div>--%>

		        </div>
	         </div>	
          </div>
          <div class="col-md-2 col-left">
          </div>
      </div> 
 </div>
</asp:Content>
