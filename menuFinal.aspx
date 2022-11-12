<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="menuFinal.aspx.vb" Inherits="INTeatroDig.menuFinal" %>

<!DOCTYPE html>
<html lang="en" class="no-js">
	<head>
		<meta charset="UTF-8" />
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
		<meta name="viewport" content="width=device-width, initial-scale=1.0"> 
		<title>Instituto Nacional del Teatro</title>
		<meta name="description" content="Responsive Retina-Friendly Menu with different, size-dependent layouts" />
		<meta name="keywords" content="responsive menu, retina-ready, icon font, media queries, css3, transition, mobile" />
		<meta name="author" content="Codrops" />
		    <link href="bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="estilo.css" />
    <link type="text/css" rel="stylesheet" href="style.css" />
    <link type="text/css"  rel="stylesheet" href="estilos.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>
    <script src="js/jquery-ui-1.9.0.custom/js/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <link rel="icon" href="favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
    <link href='https://fonts.googleapis.com/css?family=Lato:400,300,700,400italic,700italic,300italic|PT+Serif:400,700,400italic' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
    <link href="fonts/muller-font.css" rel="stylesheet" />

    <script type="text/javascript" src="jquery.easing.1.3.js"></script>
    <script src="js/s3Slider.js" type="text/javascript"></script>
    <script type="text/javascript" src="jquery.easing.1.3.js"></script>
    <link href="menu_assets/styles.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css' />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" integrity="sha512-1ycn6IcaQQ40/MKBW2W4Rhis/DbILU74C1vSrLJxCq57o941Ym01SwNsOMqvEBFlcgUa6xLiPY/NS5R+E6ztJQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <link rel="shortcut icon" href="../favicon.ico"> 
	<link rel="stylesheet" type="text/css" href="css/default.css" />
	<link rel="stylesheet" type="text/css" href="css/component.css" />
	<link rel="stylesheet" type="text/css" href="css/icomoon/style.css" />
	<link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="css/style2.css" />
    <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
    <script src="js/modernizr.custom.js"></script>

    <style>
        .pregunta{
            color:red !important;
            font-weight: bolder !important;
        }
        .contacto{
            color:blue !important;
            font-weight: bolder !important;
        }
    </style>

<!-- <script type="text/javascript">
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-7243260-2']);
_gaq.push(['_trackPageview']);
(function() {
var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
</script> -->
	</head>
	<body>
		<header>

            <div class="menu" id="menu" >
                <div class="contenedor contenedor-botones-menu">
                    <button id="btn-menu-barras" class="btn-menu-barras"><i class="fas fa-bars"></i></button>
                    <button id="btn-menu-cerrar" class="btn-menu-cerrar"><i class="fas fa-times"></i></button>
                </div>
                <div class="contenedor contenedor-enlaces-nav">
                    <div class="btn-departamentos" id="btn-departamentos">
                       <div> <img src="favicon.ico"  alt ="INT" style="margin-right:10px" />Menú Principal</div>
                       <span class="icono"> <i class="fas fa-caret-down"></i></span>
                    </div>
                    <div class="enlaces">
                        <a href="menufinal.aspx"><i class="fas fa-home"></i> Inicio</a>
                        <a href="preguntasFrecuentes.aspx"><i class="far fa-question-circle"></i> Preguntas Frecuentes</a>
                        <a href="contacto.aspx"><i class="fas fa-file-signature"></i> Formulario de contacto</a>
                        <a href="http://inteatro.gob.ar"><i class="fas fa-globe"></i> Web INT</a>
                        <a href="logout.aspx"><i class="fas fa-sign-out-alt"></i> Salir</a>
                    </div>
                </div>
                <div class="contenedor contenedor-grid">
                    <div class="grid" id="grid" style="box-shadow:3px 3px 3px rgba(0,0,0,0.4">
                        <div class="categorias">
                            <div class="header_mobile">
                                <%--<a class="navbar-brand" href="/">
                                    <img src="//inteatro.gob.ar/content/images/logo.png" width="120" height="40" alt="Instituto Nacional de Teatro">
                                </a>--%>
                                <button class="btn-regresar"><i class="fas fa-arrow-left"></i>Regresar</button>
                            </div>
                            <div class="botonera">
                                <a href="cambioDeClave.aspx" class="mobile btn-menu-ppal yellow" data-categoria="cambiar-contraseña"><i class="fas fa-key icon-menu"></i>  cambiar contraseña </a>
                                <a href="cambioDeMail.aspx" class="mobile btn-menu-ppal blue" data-categoria="cambiar-mail"><i class="fas fa-at icon-menu"></i>  cambiar mail </a>
                                <a href="#" class="mobile btn-menu-ppal red" data-categoria="datos-personales"><i class="far fa-id-card icon-menu"></i>  datos personales <span><i class="fas fa-angle-right icon-menu flecha"></i></span></a>
                                <a href="#" class="mobile btn-menu-ppal yellow" data-categoria="registros"><i class="fas fa-clipboard icon-menu"></i>  registros <span><i class="fas fa-angle-right icon-menu flecha"></i></span></a>
                                <a href="registroImpresion.aspx" class="mobile btn-menu-ppal blue" data-categoria="imprimir-constancias"><i class="fas fa-print icon-menu"></i>  imprimir constancias </a>
                                <a href="convocatorias.aspx" class="mobile btn-menu-ppal red" data-categoria="convocatorias-extraordinarias-vigentes"><i class="fas fa-bullhorn icon-menu"></i>  convocatorias extraordinarias vigentes</a>

                            </div>
                            <%--<div class="clip" style="width:50%">
                                <video controls="controls">
                                     <source src="teatroClip.mp4"  type="video/mp4" />
                                </video>
                            </div>--%>
                        </div>
                        <div class="contenedor-subcategorias">
                            <div class="subcategoria" data-categoria="datos-personales">
                                <div class="enlaces-subcategoria">
                                    <button class="btn-regresar">
                                        <span><i class="fas fa-arrow-left"></i></span>Regresar
                                    </button>
                                    <h3 class="subtitulo">Datos Personales</h3>
                                    <a href="ActualIndivFis.aspx"><i class="fas fa-database"></i> Actualización de Datos</a>
                                    <a href="Desvincular.aspx"><i class="fas fa-unlink"></i> Desvincularme de un Registro</a>
                                    <a href="AltaBeneficiario.aspx"><i class="fas fa-clipboard-list"></i> Datos de Alta de Beneficiario</a>
                                    <a href="AdjuntosLista.aspx"><i class="fas fa-paperclip"></i> Documentación Adjunta</a>
                                </div>
                             </div>       
                             <div class="subcategoria" data-categoria="registros">
                                <div class="enlaces-subcategoria">
                                    <button class="btn-regresar">
                                        <span><i class="fas fa-arrow-left"></i></span>Regresar
                                    </button>
                                    <h3 class="subtitulo">Registros</h3>
                                    <a id="nuevoRegistro" href="#"><i class="fas fa-file-alt"></i>Nuevo Registro<span><i class="fas fa-angle-right"></i></span></a>
                                    <ul  class="newRecord ">
                                        <li>
                                           <a href="registroTeatro.aspx?accion=A&codigo=0"> <i class="fas fa-theater-masks"></i>Sala Teatro Independiente</a>
                                        </li>
                                        <li>
                                            <a href="registroGrupo.aspx?accion=A&codigo=0&S=2"><i class="fas fa-theater-masks"></i>Grupo Teatro Independiente</a>
                                        </li>
                                        <li>
                                            <a href="registroGrupo.aspx?accion=A&codigo=0&S=3"><i class="fas fa-theater-masks"></i>Grupo Comunitario</a>
                                        </li>
                                        <li>
                                            <a href="registroGrupo.aspx?accion=A&codigo=0&S=4"><i class="fas fa-theater-masks"></i>Grupo Vocacional</a>
                                        </li>
                                        <li>
                                            <a href="registroAsistenteTecnico.aspx?accion=A&codigo=0"><i class="fas fa-theater-masks"></i>Capacitador Técnico</a>
                                        </li>
                                        <li>
                                            <a href="registroEspectaculo.aspx?accion=A&codigo=0"><i class="fas fa-theater-masks"></i>Espectáculo Concertado</a>
                                        </li>
                                        <li>
                                            <a href="registroPublicacion.aspx?accion=A&codigo=0"><i class="fas fa-theater-masks"></i>Publicación</a>
                                        </li>
                                        <li>
                                            <a href="registroEvento.aspx?accion=A&codigo=0"><i class="fas fa-theater-masks"></i>Evento</a>
                                        </li>
                                    </ul>
                                    <a href="registroLista.aspx"><i class="far fa-edit"></i>Actualización de Registro</a>
                                    <a href="cambioResponsable.aspx"><i class="fas fa-user-check"></i>Cambio de Responsable</a>
                                </div>
                             </div>                                       
                            <div class="banner-subcategoria"></div>
                            <div class="galeria-subcategoria"></div>
                        </div>
                    </div>           
                </div>
            </div>
            <nav class="navbar navbar-default" id="menu_desktop" role="navigation" >
                <div class="container main-container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#header-navbar">
                            <span class="sr-only">Menú</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a id="Home" href="javascript:void(0)"></a>
                        <a class="navbar-brand" href="menufinal.aspx">
                            <img src="//inteatro.gob.ar/content/images/logo.png" alt="Instituto Nacional de Teatro">
                        </a>
                    </div>
                    <!-- Collect the nav links, forms, and other content for toggling -->
                    <div class="collapse navbar-collapse" id="header-navbar">
                        <ul class="nav navbar-nav navbar-right nav-social-footer header">                        
                            <li><a href="https://www.facebook.com/inteatro/" target="_blank" title="Seguinos en Facebook" class="social social-facebook"></a></li>
                            <li><a href="https://www.instagram.com/inteatro_arg/" target="_blank" title="Nuestro Instagram" class="social social-instagram"></a></li>
                            <li><a href="https://twitter.com/inteatro_arg/" target="_blank" title="Seguinos en Twitter" class="social social-twitter"></a></li>
                            <li><a href="https://www.youtube.com/channel/UCtMHAAu6xiF-4ZCHvfYo4pA" target="_blank" title="Seguinos en Youtube" class="social social-youtube"></a></li>                        
                        </ul>
                        <div class="row clearfix">
                            <ul class="nav navbar-nav navbar-menu">
                                <li><a href='menufinal.aspx' class="menu menu-inicio active"><span>INICIO</span></a></li>
                                <li><a href='misdatos.aspx' class="menu menu-inicio"><span>MIS DATOS</span></a></li>
                                <li><a href='preguntasFrecuentes.aspx' class="menu menu-inicio" target="_blank"><span class="pregunta">PREGUNTAS FRECUENTES</span></a></li>
                                <li><a href='contacto.aspx' class="menu menu-inicio" target="_blank"><span class="contacto">FORMULARIO DE CONTACTO</span></a></li>
                                <li><a href='http://inteatro.gob.ar' target="_blank" class="menu menu-inicio"><span>WEB INT</span></a></li>
                                <li><a href='logout.aspx' class="menu  menu-inicio" runat="server" id="CerrarSesion"><span>CERRAR SESIÓN</span></a></li> 
                            </ul>
                        </div>
                    </div><!-- /.navbar-collapse -->
                </div><!-- /.container-fluid -->
            </nav>
                        <h1>Instituto Nacional del Teatro <span>Nuevo diseño, el mismo compromiso</span></h1>
        </header>
		<div class="container mainContainer" >	
<%--			<header>								
				<img src="images/logoINT.png" alt="" width="150px" height="50px" style="margin-top:20px; margin-left:20px" >					
				<h1>Instituto Nacional del Teatro <span>nuevo diseño, el mismo compromiso</span></h1>
                <ul>
                    <li>
                        <a href="index.aspx" style="margin-right:20px" >
							<div class="bie">
								<span class="icon-exit"></span>								
							</div>
							Cerrar Sesión
                        </a>						
                    </li>
                </ul>				                							
			</header>--%>

			<div class="main clearfix">
				<div id="centro2" class="centro" style="display:none" >
				<form id="form1" runat="server">
					<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
					 </asp:ScriptManager>
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
                            <div id="DivDown">
                                <div>
                                    <!-- Nav tabs -->
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li role="presentation" class="active"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Mis Vinculaciones</a></li>
                                        <li role="presentation"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Mis Registros</a></li>
                                    </ul>
                                    <!-- Tab panes -->
                                    <div class="tab-content">
                                        <div role="tabpanel" class="tab-pane  active" id="profile">
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" CellPadding="2"                                            
                                            DataSourceID="SqlDataSource2" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="IDregisdig" HeaderText="IDregisdig"
                                                    InsertVisible="False" ReadOnly="True" SortExpression="IDregisdig" Visible="false" />
                                                <asp:BoundField DataField="IDIntegrante" HeaderText="IDIntegrante"
                                                    InsertVisible="False" ReadOnly="True" SortExpression="IDIntegrante" Visible="false" />
                                                <asp:BoundField DataField="TipoRegistro" HeaderText="Tipo de Registro"
                                                    SortExpression="TipoRegistro" />
                                                <asp:BoundField DataField="IDRegistro" HeaderText="Código de Ingreso"
                                                    InsertVisible="False" ReadOnly="True" SortExpression="IDRegistro" />
                                                <asp:BoundField DataField="Denominacion" HeaderText="Denominación"
                                                    SortExpression="Denominacion" />
                                                <asp:BoundField DataField="Alta" HeaderText="Fecha de Incorporación" SortExpression="Alta" />
                                                <asp:BoundField DataField="verificado" HeaderText="Fecha de Confirmación" SortExpression="verificado" />
                                                <asp:TemplateField HeaderText="Confirmar Vinculación">
                                                  <ItemTemplate>
                                                     <asp:CheckBox ID="CheckBox1" Checked='<%# Eval("selec") %>' runat="server"
                                                           OnCheckedChanged="verificar" AutoPostBack="True" />
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" SelectCommand="select d.CODIGO IDregisdig, a.IDIntegrante, c.Descrip TipoRegistro, b.codigo IDRegistro, b.denominacion Denominacion , b.FechAlta Alta, a.verificado,
                                                   case when a.verificado is null then 0 else 1 end as selec
                                                 from integrantes a inner join registro b on a.codigoRegistro = b.CODIGO inner join sectores c on b.sector = c.codigo 
                                                 inner join regisdig d on a.CUIL = d.CUIL  where d.Codigo=@id and a.fechaBaja is null"
                                            DeleteCommand="DELETE FROM IntegrantesTemp WHERE idIntegrante = @idIntegrante"
                                            CancelSelectOnNullParameter="false">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="id" SessionField="codigo" Type="Int32" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="idIntegrante" Type="Int32" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                        </div>
                                        <div role="tabpanel" class="tab-pane" id="home">
                                            <asp:GridView ID="grillamisregistros" runat="server" CssClass="table table-bordered"
                                            CellPadding="2"
                                            DataSourceID="SqlDataSource1">
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:INTeatroDig %>"
                                            SelectCommand="SELECT b.descrip [Tipo de Registro], a.denominacion as Denominación, convert(varchar, a.fechAlta, 103) [Fecha de Registro] 
                                            FROM registro a inner join sectores b on a.sector = b.codigo
                                            WHERE responsable = cast(@codigo as integer) AND fechBaja IS NULL ORDER BY a.codigo">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="codigo" SessionField="codigo" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                                  <asp:HiddenField ID="hfTab" runat="server" />  
                            </div>
                        </div>
				</form>                   
				</div>				
				<nav id="menuFine" class="nav">					
					<ul class="menuContainer">
						<li id="Uno" class="beHere" onclick="clickOnMe(this)">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-profile"></i>
								</span>
								<span class="text">Datos Personales</span>
							</a>
						</li>
						<li id="Tres" class="beHere"  onclick="clickOnMe(this)">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-copy"></i>
								</span>
								<span class="text">Nuevo Registro</span>
							</a>
						</li> 
						<li id="Cuatro" class="beHere"  onclick="clickOnMe(this)">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-drawer"></i>
								</span>
								<span class="text">Mis Registros</span>
							</a>
						</li>						
						<li id="Cinco" class="beHere">
							<a href="MisTramites.aspx">
								<span class="icon">
									<i aria-hidden="true" class="icon-folder-open-o"></i>
								</span>
								<span class="text">Mis Trámites</span>
							</a>
						</li>
                        <li id="Siete" class="beHere">
							<a href="registroImpresion.aspx">
								<span class="icon">
									<i aria-hidden="true" class="icon-print"></i>
								</span>
								<span class="text longText">Imprimir Constancias</span>
							</a>
						</li>
                        <%--<li id="Seis" class="beHere">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-stack"></i>
								</span>
								<span class="text">Mis Rendiciones</span>
							</a>
						</li>	--%>
                        <li id="Dos" class="beHere" onclick="clickOnMe(this)">
							<a href="#">
								<span class="icon"> 
									<i aria-hidden="true" class="icon-services"></i>
								</span>
								<span class="text">Configuración</span>
							</a>
						</li>                       
                        <%--<li id="Siete" class="beHere">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-print"></i>
								</span>
								<span class="text longText">Imprimir Constancias</span>
							</a>
						</li>
						<li id="Ocho" class="beHere" >
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-bullhorn"></i>
								</span>
								<span class="text longText">Convocatorias Extraordinarias</span>
							</a>
						</li>--%>
					</ul>
				</nav>
    				<ul class="ca-menu" style="margin:0">
					<div class="datosPersonales">
						<li>
							<a href="ActualIndivFis.aspx" id="actual" runat="server">
								<span class="ca-icon">P</span>
								<div class="ca-content">
									<h2 class="ca-main">Actualización de Datos</h2>
									<!-- <h3 class="ca-sub">Personalized to your needs</h3> -->
								</div>
							</a>
						</li>
						<li>
							<a href="Desvincular.aspx">
								<span class="ca-icon">X</span>
								<div class="ca-content">
									<h2 class="ca-main">Desvincularme de un Registro</h2>					
								</div>
							</a>
						</li>
						<li>
							<a href="AltaBeneficiario.aspx">
								<span class="ca-icon">+</span>
								<div class="ca-content">
									<h2 class="ca-main">Datos de Alta de Beneficiario</h2>					
								</div>
							</a>
						</li>
						<li>
							<a href="AdjuntosLista.aspx">
								<span class="ca-icon">A</span>
								<div class="ca-content">
									<h2 class="ca-main">Documentación Adjunta</h2>					
								</div>
							</a>
						</li>
					</div>
					<div class="config">
						<li>
							<a href="cambioDeClave.aspx">
								<span class="ca-icon">x</span>
								<div class="ca-content">
									<h2 class="ca-main">Cambiar Contraseña</h2>					
								</div>
							</a>
						</li>
						<li>
							<a href="cambioDeMail.aspx">
								<span class="ca-icon">@</span>
								<div class="ca-content">
									<h2 class="ca-main">Cambiar Correo Electrónico</h2>					
								</div>
							</a>
						</li>
					</div>
					<div class="nuevoRegistro">
						<!-- <div class="subCategorias"></div> -->
							<li >
								<a href="registroTeatro.aspx?accion=A&codigo=0">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Sala Teatro Independiente</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="registroGrupo.aspx?accion=A&codigo=0&S=2">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Grupo Teatro Independiente</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="registroGrupo.aspx?accion=A&codigo=0&S=3">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Grupo Comunitario</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="registroGrupo.aspx?accion=A&codigo=0&S=4">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Grupo Vocacional</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="registroAsistenteTecnico.aspx?accion=A&codigo=0">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Capacitador Técnino</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="registroEspectaculo.aspx?accion=A&codigo=0">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Espectáculo Concertado</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="registroPublicacion.aspx?accion=A&codigo=0">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Publicación</h2>
									</div>
								</a>
							</li>
						    <li runat="server" id="ong" >
								<a href="registroONG.aspx?accion=A&codigo=0">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Entidad/Sociedad</h2>
									</div>								
								</a>

						    </li>
							<li >
								<a href="registroEvento.aspx?accion=A&codigo=0">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Evento</h2>
									</div>
								</a>
							</li>                        
					
					</div>
					<div class="registros">
						<li >
							<a href="registroLista.aspx">
								<span class="ca-icon">*</span>
								<div class="ca-content">
									<h2 class="ca-main">Actualización de Registro</h2>
								</div>
							</a>
						</li>
						<li >
							<a href="CambioResponsable.aspx">
								<span class="ca-icon">U</span>
								<div class="ca-content">
									<h2 class="ca-main">Cambio de Responsable</h2>
								</div>
							</a>
						</li>
					</div>
				</ul>
			</div>


		</div><!-- /container -->
                            <footer style="margin-top: 30px">
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                    <div class="footer-logo">
                        <img src="//inteatro.gob.ar/content/images/logo-blanco.png" />
                    </div>
                    <div class="footer-text">
                        <strong>Av. Santa Fe 1243 - 7º Piso<br />+54 (11) 48.15.66.61</strong><br />
                        Ciudad Autónoma de Buenos Aires<br />Argentina
                    </div>
                </div>
                <div class="col-md-3">
                    <ul>
                        <li><a href="index.aspx">Inicio</a></li>
                        <li><a href="preguntasFrecuentes.aspx">Preguntas Frecuentes</a></li>                        
                    </ul>
                </div>
                <div class="col-md-3">
                    <ul>
                        <li><a href="contacto.aspx">Formulario de contacto</a></li>
                        <li><a href="//inteatro.gob.ar" target="_blank">Web INT</a></li>                        
                    </ul>
                </div>
                <div class="col-md-3">
                    <ul class="navbar-right nav-social-footer">
                        <li><a href="https://www.facebook.com/inteatro/" target="_blank" title="Seguinos en Facebook" class="social social-facebook"></a></li>
                        <li><a href="https://www.instagram.com/inteatro_arg/" target="_blank" title="Nuestro Instagram" class="social social-instagram"></a></li>
                        <li><a href="https://twitter.com/inteatro_arg/" target="_blank" title="Seguinos en Twitter" class="social social-twitter"></a></li>
                        <li><a href="https://www.youtube.com/channel/UCtMHAAu6xiF-4ZCHvfYo4pA" target="_blank" title="Seguinos en Youtube" class="social social-youtube"></a></li>
                    </ul>
                    <div class="clearfix footer-home-link">
                        <%--<a href="#Home" class="footer-text up"><i class="fa fa-long-arrow-up"></i> Subir al inicio</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </footer>
		<script>

            
            function nuevoR() {
                $('.subCategorias').toggle(1000);
			}

			function scroll() {
                window.scrollTo(0, document.body.scrollHeight);
            }

            function clickOnMe(li) {
                const container = $('.mainContainer');
                const alto = $('.mainContainer').css('height');
                console.log(alto);
                for (let i = 0; i < 8; i++) {
                    let current = $('.beHere')[i];
                    if (current.id !== li.id) {
                        $(current).toggle(1000);
                    }
                    else {
                        switch (current.id) {
                            case 'Uno': {
                                const dp = $('.datosPersonales').css('display');
                                if (dp == 'none') {
                                    setTimeout(() => {
                                        container.css('height', '300px');
                                    }, 500);
                                   

                                }
                                else {
                                    setTimeout(() => {
                                        container.css('height', '300px');
                                    }, 500);
                                }

                                $('.datosPersonales').toggle(1000);
                                break;
                            }
                            case 'Dos': {
                                const cfg = $('.config').css('display');
                                $('.config').toggle(1000);
                                break;
                            }
                            case 'Tres': {
                                const nwr = $('.nuevoRegistro').css('display');
                                if (nwr == 'none') {
                                    container.css('height', '500px');

                                }
                                else {
                                    setTimeout(() => {
                                        container.css('height', '300px');
                                    }, 500);

                                }
                                $('.nuevoRegistro').toggle(1000);
                                break;
                            }
                            case 'Cuatro': {
                                $('.registros').toggle(1000);
                                break;
                            }
                        }
                       // reSize();
                    }

				}
				
                window.scrollTo(0, document.body.scrollHeight);

            }

            //  The function to change the class
            var changeClass = function (r, className1, className2) {
                var regex = new RegExp("(?:^|\\s+)" + className1 + "(?:\\s+|$)");
                if (regex.test(r.className)) {
                    r.className = r.className.replace(regex, ' ' + className2 + ' ');
                }
                else {
                    r.className = r.className.replace(new RegExp("(?:^|\\s+)" + className2 + "(?:\\s+|$)"), ' ' + className1 + ' ');
                }
				return r.className;
                window.scrollTo(0, document.body.scrollHeight);
            };

            const reSize = (value) => {
                console.log('value', value);
            }

            //  Creating our button in JS for smaller screens
            //var menuElements = document.getElementById('menuFine');
            //menuElements.insertAdjacentHTML('afterBegin', '<button type="button" id="menutoggle" class="navtoogle" aria-hidden="true"><i aria-hidden="true" class="icon-menu"> </i> Menu</button>');
            //window.scrollTo(0, document.body.scrollHeight);
            //  Toggle the class on click to show / hide the menu
            document.getElementById('menutoggle').onclick = function () {
				changeClass(this, 'navtoogle active', 'navtoogle');
                window.scrollTo(0, document.body.scrollHeight);
            }

            // http://tympanus.net/codrops/2013/05/08/responsive-retina-ready-menu/comment-page-2/#comment-438918
            document.onclick = function (e) {
                var mobileButton = document.getElementById('menutoggle'),
                    buttonStyle = mobileButton.currentStyle ? mobileButton.currentStyle.display : getComputedStyle(mobileButton, null).display;

                if (buttonStyle === 'block' && e.target !== mobileButton && new RegExp(' ' + 'active' + ' ').test(' ' + mobileButton.className + ' ')) {
                    changeClass(mobileButton, 'navtoogle active', 'navtoogle');
				}
				window.scrollTo(0, document.body.scrollHeight);
				
            }
        </script>
		<!-- <script src="//tympanus.net/codrops/adpacks/demoad.js"></script> -->
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
        <script type="text/javascript" src="menuIndex.js"></script>

	</body>
</html>