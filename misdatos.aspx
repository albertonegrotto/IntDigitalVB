<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="misdatos.aspx.vb" Inherits="INTeatroDig.misdatos" EnableViewState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="icon" href="/favicon.ico" type="image/x-icon" />
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
    <title>INTeatroDigital</title>
    <style type="text/css">
        .divcssmenu {
            position: absolute;
            top: 0px;
            left: 97%;
            display: none;
            width: auto !important;
            z-index: 100;
        }
            .divcssmenu li:hover ul {
                display: block;
            }

            .divcssmenu li ul {
                display: none;
            }
        .mnu1 {
            position: relative;
            display: block;
            margin: 0;
            padding: 6px;
            background: orange url(images/ovr.png) repeat-x 0 -350px;
            /*line-height: 100%; */
            border-radius: 1em;
            font: normal 1em Arial, Helvetica, sans-serif;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            /*-webkit-box-shadow: 0 1px 3px rgba(0,0,0, .4); 
			-moz-box-shadow: 0 1px 3px rgba(0,0,0, .4);
			box-shadow: 0px 0px 25px 5px rgba(0, 0, 0, 0.4);			
			border-bottom: solid 0.1em #ababab;
			border-right: solid 0.1em #ababab;
			border-top: solid 0.1em white;
			border-left: solid 0.1em white;	*/
        }
        #rojo {
            background: red url(images/ovr.png) repeat-x 0 -350px;
        }
        .li1 {
            list-style: none;
            height: auto !important;
            width: 100% !important;
            height: auto !important;
            margin: 2px !important;
        }
            .li1 a {
                border-bottom: solid 0.1em #ababab;
                border-right: solid 0.1em #ababab;
                border-top: solid 0.1em white;
                border-left: solid 0.1em white;
                background-color: white;
                border-radius: .2em;
                display: block !important;
                width: 99% !important;
                height: auto !important;
                position: relative !important;
            }
                .li1 a:hover {
                    border-bottom: solid 0.1em white;
                    border-right: solid 0.1em white;
                    border-top: solid 0.1em #ababab;
                    border-left: solid 0.1em #ababab;
                    background-color: #58afde;
                    color: white;
                }
        .icon {
            border: none !important;
            width: 48px !important;
            height: 48px !important;
            position: relative !important;
            display: inline !important;
            border: none !important;
            float: none !important;
            -webkit-box-shadow: none !important;
            box-shadow: none !important;
        }
        .divcssmenu li img {
        }
        .spanli {
            vertical-align: super !important;
            padding: 5px;
            display: inline-block !important;
        }
        .tabla {
            position: absolute;
            display: none;
            margin-left: 94%;
            top: -0.1em;
            height: auto !important;
            padding: 6px;
            background: orange url(images/ovr.png) repeat-x 0 -350px;
            font: normal 0.8em Arial, Helvetica, sans-serif;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 3px rgba(0,0,0, .4);
            -moz-box-shadow: 0 1px 3px rgba(0,0,0, .4);
            box-shadow: 0px 0px 25px 5px rgba(0, 0, 0, 0.4);
        }
        .fila {
            display: table-row;
            font-size: normal;
            height: auto !important;
        }
        .col {
            display: table-cell;
            height: auto !important;
            width: 30% !important;
        }
        li li a {
            white-space: nowrap;
        }
        /*    
        .icon
        {
        	width:auto !important;
        	height:auto !important;
        	position:relative;
        	display: inline-block !important;
        	margin: 5px 0 !important;
        }
*/
        .volver{
            padding-left: 20%;
        }
    </style>
    <script type="text/javascript">
        function drilldown() {
            if ($('.mnuli2').is(':hidden')) {
                $('.mnuli2').slideDown();
            }
            else {
                $('.mnuli2').slideUp();
            }

        }
    </script>
    <script type="text/javascript">
        function updateBackground() {
            //alturamain = jQuery("#main").height();
            //$("#main").height(alturamain);
            //anchomisreg = jQuery("#misRegistros").width();
            //$("#misRegistros ul").width(anchomisreg - 40);
            screenWidth = jQuery(".sdt_wrap").width();
            screenHeight = jQuery(".sdt_wrap").height();
            var bg = jQuery(".bg");
            // Proporcion horizontal/vertical. En este caso la imagen es cuadrada
            ratio = 1;
            if (screenWidth / screenHeight > ratio) {
                $(bg).height("auto");
                $(bg).width("100%");
                //$('#s3sliderContent').css('min-height', $(bg).height()*1.2);
            } else {
                $(bg).width("auto");
                $(bg).height("100%");
                $('.li2').css('height', screenHeight);
            }
            // Si a la imagen le sobra anchura, la centramos a mano
            //if ($(bg).width() > 0) {
            //$(bg).css('left', (screenWidth - $(bg).width()) / 2);
            //}
        }
        $(document).ready(function () {
            // Actualizamos el fondo al cargar la pagina
            updateBackground();
            $("#tabs").tabs();
            $(window).bind("resize", function () {
                // Y tambien cada vez que se redimensione el navegador
                updateBackground();
            });
            $('#sdt_menu > li').bind('mouseenter', function () {
                var $elem = $(this);
                $elem.find('.int')
		        .stop(true)
		        .animate({ 'width': '49%', 'height': 'auto', 'left': '0px', 'top': '0px' }, 400, '', function () {
		            $elem.find('.divcssmenu')
		        .stop(true)
		        .css('display', 'block')
		        });
                //.andSelf()
                //		        setTimeout(function() { $elem.find('.divcssmenu').slideDown('slow'); }, 300);
            })
		    .bind('mouseleave', function () {
		        var $elem = $(this);
		        //		        $('.divcssmenu').hide('fast')
		        $elem.find('.divcssmenu')
		        //.stop(true)
		        .hide('fast')
		        .andSelf()
		        .find('.int')
		        .stop(true)
		        .animate({ 'width': '0px', 'height': 'auto', 'left': '0px' }, 400);
            });
            var selectedTab = $("#<%=hfTab.ClientID%>");
            var tabId = selectedTab.val() != "" ? selectedTab.val() : "home";
            $('.nav-tabs a[href="#' + tabId + '"]').tab('show');
            $("#dvTab a").click(function () {
                selectedTab.val($(this).attr("href").substring(1));
            });
        });

    </script>

    <script language="javascript" type="text/javascript">
		<!--
    function bookmarksite(title, url) {
        if (document.all)
            window.external.AddFavorite(url, title);
        else if (window.sidebar)
            alert("Presione Ctrl+D para marcar como favorito");
    }
    //-->
    </script>

    <script type="text/javascript">
        function signOut() {
            __doPostBack('CierraSession', this);
        }
    </script>

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
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
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
                        <a href="index.aspx"><i class="fas fa-home"></i> Inicio</a>
                        <a href="misdatos.aspx"><i class="fas fa-home active"></i> Mis Datos</a>
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
                        <a class="navbar-brand" href="/">
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
                                <li><a href='menuFinal.aspx' class="menu menu-inicio"><span>INICIO</span></a></li>
                                <li><a href='misdatos.aspx' class="menu menu-inicio active"><span>MIS DATOS</span></a></li>
                                <li><a href='preguntasFrecuentes.aspx' class="menu menu-inicio"><span class="pregunta">PREGUNTAS FRECUENTES</span></a></li>
                                <li><a href='contacto.aspx' class="menu menu-inicio"><span class="contacto">FORMULARIO DE CONTACTO</span></a></li>
                                <li><a href='http://inteatro.gob.ar' target="_blank" class="menu menu-inicio"><span>WEB INT</span></a></li>
                                <li><a href='logout.aspx' class="menu  menu-inicio" runat="server" id="CerrarSesion"><span>CERRAR SESIÓN</span></a></li> 
                            </ul>
                        </div>
                    </div><!-- /.navbar-collapse -->
                </div><!-- /.container-fluid -->
            </nav>

        </header>
        <div id="backgroundBody">
            <div id="main">
               <div id="tododatos" >
                    <div id="izq2" style="background-color: #dedede; display:none">
                        <!--<div id="menuizq">-->
                        <ul id="sdt_menu" class="sdt_menu">
                            <li class="li2">
                                <a class="ref" href="cambioDeClave.aspx">
                                    <img class="int" src="images/int.jpg" alt="" />
                                    <span class="sdt_active"></span>
                                    <span class="sdt_wrap">
                                        <%--<div class="menu-item">
                                            <span>CAMBIAR </span>
                                            <span> CONTRASEÑA</span>
                                            <i class="faicon fas fa-user-lock"></i>
                                        </div>--%>
                                        <%--<div class="btnContrasena">
                                            <div class="btnContenido">
                                                <span>Cambiar</span><br /><span> Contraseña</span>
                                            </div>                                            
                                        </div>--%>
                                        <div class="contenedor-bg">
                                           <img class="bg-pic" alt="Contraseña" src="images/btnContrasena2.png" /> 
                                            <span class="titulo-bg">Cambiar Contraseña</span>
                                            <i class="fas fa-key btnIcon"></i>
                                        </div>
                                        <span class="sdt_link"></span>
                                        <span class="sdt_descr"></span>
                                    </span>
                                </a>
                            </li>
                            <li class="li2">
                                <a class="ref" href="cambioDeMail.aspx">
                                    <img class="int" src="images/int.jpg" alt="" />
                                    <span class="sdt_active"></span>
                                    <span class="sdt_wrap">
                                      <div class="contenedor-bg">
                                           <img class="bg-pic" alt="Email" src="images/btnMail2.png" /> 
                                            <span class="titulo-bg">Cambiar EMail</span>
                                            <i class="fas fa-at btnIcon"></i>
                                       </div>
<%--                                        <img class="bg" alt="Impresión" src="images/btnMail.png" />--%>
                                        <span class="sdt_link"></span>
                                        <span class="sdt_descr"></span>
                                    </span>
                                </a>
                            </li>
                            <li class="li2">
                                <a class="ref" href="#">
                                    <img class="int" src="images/int.jpg" alt="" />
                                    <span class="sdt_active"></span>
                                    <span class="sdt_wrap" style="background-color: green;">
<%--                                        <img class="bg" alt="Datos Personales" src="images/btnDatos.png" style="width: 100%; height: auto" />--%>
                                      <div class="contenedor-bg">
                                           <img class="bg-pic" alt="Datos Personales" src="images/btnDatos2.png" /> 
                                            <span class="titulo-bg">Datos Personales</span>
<%--                                            <i class="fas fa-at btnIcon"></i>--%>
                                          <i class="fas fa-address-card btnIcon"></i>
                                       </div>
                                        <span class="sdt_link"></span>
                                        <span class="sdt_descr"></span>
                                    </span>
                                </a>
                                <div class='divcssmenu'>
                                    <ul class="mnu1" id="rojo">
                                        <li class="li1"><a href="ActualIndivFis.aspx" id="actual" runat="server">
                                            <i class="fas fa-database"></i><span class="spanli">Actualización de Datos</span></a>
                                        </li>
                                        <li class="li1 consub"><a href="Desvincular.aspx">
                                            <i class="fas fa-unlink"></i><span class="spanli">Desvincularme de un Registro</span></a>
                                        </li>
                                        <li class="li1 consub"><a href="AltaBeneficiario.aspx">
                                            <i class="fas fa-clipboard"></i><span class="spanli">Datos de Alta de Beneficiario</span></a>
                                        </li>
                                        <li class="li1 consub"><a href="AdjuntosLista.aspx">
                                            <i class="fas fa-paperclip"></i><span class="spanli">Documentación Adjunta</span></a>
                                        </li>


                                    </ul>
                                </div>
                            </li>
                            <li class="li2">
                                <a class="ref" href="#">
                                    <img class="int" src="images/int.jpg" alt="" />
                                    <span class="sdt_active"></span>
                                    <span class="sdt_wrap">
                                    <div class="contenedor-bg">
                                       <img class="bg-pic" alt="Registros" src="images/btnContrasena2.png" /> 
                                       <span class="titulo-bg">Registros</span>
                                       <i class="fas fa-folder-open btnIcon"></i>
                                      </div>
<%--                                        <img class="bg" alt="Registros" src="images/btnRegistors.png" />
                                        <i class="fas fa-folder-open btnIcon"></i>--%>
                                        <span class="sdt_link"></span>
                                        <span class="sdt_descr"></span>
                                    </span>
                                </a>
                                <div class='divcssmenu'>
                                    <ul class="mnu1">
                                        <li class="li1"><a class="ref">
                                            <i class="fas fa-file-medical"></i><span class="spanli">Nuevo Registro <span id="flecha">&#10140;</span></span></a>
                                            <ul class="tabla">
                                                <li class='fila'>
                                                    <a href="registroTeatro.aspx?accion=A&codigo=0">
                                                        <span class="spanli"><i class="fas fa-theater-masks"></i>Sala Teatro Independiente</span>
                                                    </a>
                                                </li>
                                                <li class="fila">
                                                    <a href="registroGrupo.aspx?accion=A&codigo=0&S=2">
                                                        <span class="spanli"><i class="fas fa-theater-masks"></i>Grupo Teatro Independiente</span>
                                                    </a>
                                                </li>
                                                <li class='fila'><a href="registroGrupo.aspx?accion=A&codigo=0&S=3"><span class="spanli"><i class="fas fa-theater-masks"></i>Grupo Comunitario</span></a></li>
                                                <li class='fila'><a href="registroGrupo.aspx?accion=A&codigo=0&S=4"><span class="spanli"><i class="fas fa-theater-masks"></i>Grupo Vocacional</span></a></li>
                                                <li class='fila'><a href="registroAsistenteTecnico.aspx?accion=A&codigo=0"><span class="spanli"><i class="fas fa-theater-masks"></i>Capacitador Técnico</span></a></li>
                                                <li class='fila'><a href="registroEspectaculo.aspx?accion=A&codigo=0"><span class="spanli"><i class="fas fa-theater-masks"></i>Espectáculo Concertado</span></a></li>
                                                <li class='fila'><a href="registroPublicacion.aspx?accion=A&codigo=0"><span class="spanli"><i class="fas fa-theater-masks"></i>Publicación</span></a></li>
                                                <li runat="server" id="ong" class='fila'><a href="registroONG.aspx?accion=A&codigo=0"><span class="spanli"><i class="fas fa-theater-masks"></i>Entidad/Sociedad</span></a></li>
                                                <li class='fila'><a href="registroEvento.aspx?accion=A&codigo=0"><span class="spanli"><i class="fas fa-theater-masks"></i>Evento</span></a></li>
                                            </ul>
                                        </li>
                                        <li class="li1"><a href="registroLista.aspx">
                                            <i class="fas fa-edit"></i><span class="spanli">Actualización de Registro</span></a></li>
                                        <li class="li1"><a href="CambioResponsable.aspx">
                                            <i class="fas fa-user-check"></i><span class="spanli">Cambio de Responsable</span></a></li>
                                    </ul>
                                </div>
                            </li>
                            <li class="li2">
                                <a class="ref" href="registroImpresion.aspx">
                                    <img class="int" src="images/int.jpg" alt="" />
                                    <span class="sdt_active"></span>
                                    <span class="sdt_wrap">
<%--                                        <img class="bg" alt="Impresión" src="images/btnPrint.png" />--%>
                                      <div class="contenedor-bg">
                                           <img class="bg-pic" alt="Impresión" src="images/btnMail2.png" /> 
                                            <span class="titulo-bg">Imprimir Constancias</span>
                                            <i class="fas fa-print btnIcon"></i>
                                       </div>
                                        <span class="sdt_link"></span>
                                        <span class="sdt_descr"></span>
                                    </span>
                                </a>
                            </li>
                            <li class="li2">
                                <a class="ref" href="convocatorias.aspx">
                                    <img class="int" src="images/int.jpg" alt="" />
                                    <span class="sdt_active"></span>
                                    <span class="sdt_wrap">
                                     <%--   <img class="bg" alt="Impresión" src="images/btnConvocatoria.png" />--%>
                                      <div class="contenedor-bg">
                                           <img class="bg-pic" alt="Convocatorias" src="images/btnDatos2.png" /> 
                                            <span class="titulo-bg">Convocatorias Extraordinarias</span>
                                            <i class="fas fa-bullhorn btnIcon"></i>
                                         
                                       </div>
                                        <span class="sdt_link"></span>
                                        <span class="sdt_descr"></span>
                                    </span>
                                </a>
                            </li>
                        </ul>
                        <!--  <img src="images/logoINT.jpg" style="position:fixed;" />
				</div>-->
                    </div>

                    <div id="centro2" class="centro">
                        <div id="upanddown">
                            <div id="MisDatosHeader">
                                <asp:Label ID="lblNombre" class="title" runat="server" Text="Label"></asp:Label>
                                <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold volver">Volver</asp:HyperLink>
                            </div>
                            <div id="DivUp">
                                <div id="divcodigo" class="divdato"><%=getCodigo()%></div>
                                <div id="divpersona" class="divdato"><%=getPersona()%></div>
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
                    </div>
                </div>
            </div>
        </div>
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
    </form>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/js/all.min.js" integrity="sha512-YSdqvJoZr83hj76AIVdOcvLWYMWzy6sJyIMic2aQz5kh2bPTd9dzY3NtdeEAzPp/PhgZqr4aJObB3ym/vsItMg==" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://kit.fontawesome.com/900cb600a9.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="menu.js"></script>/script>

</body>
</html>
