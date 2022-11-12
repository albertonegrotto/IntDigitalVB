<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="INTeatroDig.index" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="prueba.css" />
    <link rel="stylesheet" href="estilo.css" />
    <link rel="icon" href="favicon.ico" type="image/x-icon" />
    <script src="js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <link href="bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
    <link href='https://fonts.googleapis.com/css?family=Lato:400,300,700,400italic,700italic,300italic|PT+Serif:400,700,400italic' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
    <link href="fonts/muller-font.css" rel="stylesheet" />
    <script src="js/easyslider1.7/easyslider1.7/js/easySlider1.7.js" type="text/javascript"></script>
    <link href="menu_assets/styles.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css' />
    <title>INTeatroDigital</title>
    <script type="text/javascript">
        function validaUsuario() {
            var red = getUrlVars()["ReturnURL"];
            conf = false;
            if (typeof red == 'undefined') {
                var conf = false;
            }
            else {
                if (red.substring(0, 21) == 'confirmarRegistracion') {
                    conf = true;
                }
            }
            PageMethods.ValidaUsuario(document.getElementById("inusuario").value, document.getElementById("pwd").value, conf, onSuccess, onFailure);
        }

        function validaCUIT() {
            conf = false;
            var usuario = document.getElementById("inusuario").value;
            //PageMethods.ValidaCUIT(usuario, conf, onSuccess, onFailure);
            __doPostBack('ValidaCUIT', document.getElementById("inusuario").value + ',0');
        }

        function onSuccess(result) {
            if (!result) {
                $('#inusuario').addClass('error');
                $('#mje').html('Revisa el CUIT y la Clave Ingresada');
                $('#helpUsuario2').addClass('helperror');
                $('#helpUsuario2').slideDown('fast');
            }
            else {
                $('#inusuario').removeClass('error');
                $('#helpUsuario2').removeClass('helperror');
                $('#helpUsuario2').slideUp('fast');
                __doPostBack('IniciaSession', document.getElementById("inusuario").value + ',' + document.getElementById("pwd").value);

            }
        }

        function onFailure(result) {
            $('#inusuario').addClass('error');
            $('#mje').html('Revisa el CUIT y la Clave Ingresada');
            $('#helpUsuario2').addClass('helperror');
            $('#helpUsuario2').slideDown('fast');

        }
    </script>
    <script type="text/javascript">
        function tamaños() {
            $('#slider').css('width', '100%');
            var fotos = $('.item');
            var cantidad = fotos.length;
            ancho = jQuery("#slider").width();
            $('#slider').css('width', ancho);
            //var ancho = $('.item').width();
            //alert(screenWidth);
            $('.item').css('width', ancho);
            $('.lista').css('width', ancho * cantidad);
            //alert('slider:' + $('#slider').width() + '; item:' + $('.item').width() + '; lista:' + $('.lista').width());
        }

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        $(document).ready(function () {
            var antes = $('#slider').html();
            tamaños();
            var ancho = $('#slider').width();
            $('#slider').easySlider({
                prevText: '<<',
                nextText: '>>',
                firstShow: false,
                lastShow: false,
                continuous: true,
                numeric: true,
                auto: true,
                controlsShow: false,
                incremento: ancho,
                pause: 3000

            });
            $('#helpUsuario2').slideUp('fast');
            //$(window).bind("resize", function () {
            //    $('#slider').remove();
            //    var slr = '<div id="slider">' + antes + '</div>';
            //    $('.centro').html(slr + $('.centro').html());
            //    tamaños();
            //    var ancho = $('#slider').width();
            //    $('#slider').easySlider({
            //        prevText: '<<',
            //        nextText: '>>',
            //        firstShow: false,
            //        lastShow: false,
            //        continuous: true,
            //        numeric: true,
            //        auto: true,
            //        controlsShow: false,
            //        incremento: ancho
            //    });
            //});

            $('#inusuario').focusin(function () {
                if ($('.error').length == 0) {
                    $('#inusuario').addClass('focus');
                    $('#mje').html('Ingresá los 11 números del CUIT/CUIL (sin guiones)');
                    $('#helpUsuario2').slideDown('fast');
                }

            });
            $('#inusuario').focusout(function () {
                if ($('.error').length == 0) {
                    $('#inusuario').removeClass('focus');
                    $('#helpUsuario2').slideUp('fast');
                }
            });

        });

        function response() {
            window.location = "InicioIndiv.aspx";
        }
    </script>
    <script language="javascript" type="text/javascript">
		<!--
        function bookmarksite(title, url) {
            if (document.all)
                window.external.AddFavorite(url, title);
            else if (window.sidebar)
                alert("Presiona Ctrl+D para marcar como favorito");
        }
        //-->
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>

    </form>
    <header>
      <div class="menu" id="menu" >
        <div class="contenedor contenedor-botones-menu">
            <button id="btn-menu-barras" class="btn-menu-barras"><i class="fas fa-bars"></i></button>
            <button id="btn-menu-cerrar" class="btn-menu-cerrar"><i class="fas fa-times"></i></button>
        </div>
        <div class="contenedor contenedor-enlaces-nav">
            <div class="enlaces">
                <a href="index.aspx"><i class="fas fa-home"></i> Inicio</a>
                <a href="preguntasFrecuentes.aspx"><i class="far fa-question-circle pregunta"></i> Preguntas Frecuentes</a>
                <a href="contacto.aspx"><i class="fas fa-file-signature"></i> Formulario de contacto</a>
                <a href="http://inteatro.gob.ar"><i class="fas fa-globe"></i> Web INT</a>
            </div>
        </div>
       </div>
        <nav class="navbar navbar-default menu_desktop" role="navigation">
            <div class="container main-container">
              <div class="navbar-header">
                    <%--<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#header-navbar">
                        <span class="sr-only">Menú</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>--%>
                    <%--<a id="Home" href="javascript:void(0)"></a>--%>
                    <a id="Home" href="index.aspx"></a>
                    <a class="navbar-brand" href="index.aspx">
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
                            <li><a href='index.aspx' class="menu  menu-inicio active"><span>INICIO</span></a></li>
                            <li><a href='preguntasFrecuentes.aspx' target="_blank" class="menu menu-inicio"><span class="pregunta">PREGUNTAS FRECUENTES</span></a></li>
                            <li><a href='contacto.aspx' target="_blank" class="menu menu-inicio"><span class="contacto">FORMULARIO DE CONTACTO</span></a></li>
                            <li><a href='http://inteatro.gob.ar' target="_blank" class="menu  menu-inicio"><span>WEB INT</span></a></li>
                            <li><a href='logout.aspx' class="menu menu-inicio" runat="server" id="CerrarSesion"><span>CERRAR SESIÓN</span></a></li>
                        </ul>
                    </div>
                </div><!-- /.navbar-collapse -->
            </div><!-- /.container-fluid -->
        </nav>
    </header>
    <div id="backgroundBody">
        <div id="main">
            
            <div id="izq">
                <div id="login">
                    <div id="titulo">INICIAR SESIÓN</div>
                    <div id="loginCont">
                        <div class="login-wrap">
                            <span class="label">CUIT/CUIL:</span>
                            <div id="divUsuario" class="divInput" runat="server">
                                <input id="inusuario" class="login" type="text" runat="server" />
                                <div id="helpUsuario2" class="helpnormal">
                                    <p id="mje">Ingresá los 11 números del CUIT/CUIL (sin guiones)</p>
                                </div>
                                <input id="ingresa" type="button" runat="server" value="Ingresar" class="btn btn-warning" style="display: block; margin: auto; margin-bottom: 10px;" onclick="validaCUIT();" />
                            </div>
                            <br />
                            <div id="divPwd" class="divInput" runat="server">
                                <span>Bienvenida/o</span>
                                <br />
                                <span id="Bienvenida" runat="server">Usuario de INTDigital</span>
                                <br />
                                <br />
                                <span class="label">Contraseña:</span>
                                <input id="pwd" class="login" type="password" runat="server" />
                            </div>
                        </div>
                        <div id="DivContra" runat="server">
                           <a id="forgot" href="RecupContra.aspx">Olvidé mi contraseña</a>
                           <input type="button" value="Iniciar Sesión" class="btn btn-warning" style="display: block; margin: auto; margin-bottom: 10px;" onclick="validaUsuario();" />
                        </div>
                        <div id="DivAlta" runat="server">
                           <span id="todavia">Todavía no realizaste tu Alta Individual?<br />Hacelo aquí</span>
                           <input type="button" value="Realizar Alta Individual" class="btn btn-primary btn-xs" style="display: block; margin: auto; margin-top: 10px;" onclick="response();" />
                        </div>
                    </div>
                </div>
                <div style="position: relative; width: 100%; margin-top: 20px">
                    <img id="imagen" src="http://inteatro.gob.ar/content/images/logo.png" />
                </div>
            </div>
            <div id="centro" class="centro" >
<%--                <div id="slider" >
                    <ul class="lista">
                        <li class="item">
                            <img src="images/wellcome.png" alt="" class="bg"></li>
                        <li class="item">
                            <img src="images/diseño_remozado.png" alt="" class="bg"></li>
                        <li class="item">
                            <img src="images/sesion.png" alt="" class="bg"></li>
                        <li class="item">
                            <img src="images/integrado.png" alt="" class="bg"></li>
                        <li class="item">
                            <img src="images/faq2.png" alt="" class="bg"></li>
                    </ul>
                </div>--%>
                <div id="imgTitulo">
                    <img src="images/plataforma.jpg" alt="inteatrodigital" />
                </div>
                <div id="texto">
                    <span class="obj" style="color: blue"><strong><u>Importante:</u></strong></span><br />
                    <ul>
                        <li class="obj" style="color: blue">Dada la imposibilidad de enviar al INT documentación por correo postal,</li>
                        <li class="obj" style="color: blue">provisoriamente se aceptará que el Responsable, luego de realizar un </li>
                        <li class="obj" style="color: blue"> REGISTRO o una ACTUALIZACION, envíe POR MAIL a su correspondiente  </li>
                        <li class="obj" style="color: blue"> Representación Provincial la <strong>"Constancia de Registro"</strong></li>
                    </ul>
                    <span class="obj" style="color: #ed1b24"><strong><u>Si su proveedor de correo es "hotmail":</u></strong></span><br />
                    <ul>
                        <li class="obj" style="color: #ed1b24">Tenga la previsión de incorporar a la libreta de contactos (o a la lista segura </li>
                        <li class="obj" style="color: #ed1b24"> de remitentes), las direcciones <b>intdigital@inteatro.gob.ar</b> e </li>
                        <li class="obj" style="color: #ed1b24"> <b>info.intdigital@inteatro.gob.ar</b> para que ningún mensaje proveniente </li>
                        <li class="obj" style="color: #ed1b24">  de esta plataforma sea considerado como “spam” por su proveedor de correo.</li>
                    </ul>
                    <span class="obj">Nuestros objetivos en la implementación de esta plataforma son:</span><br />
                    <ul>
                        <li class="obj"><strong>Mejor Servicio al ciudadano:</strong> mejorando la calidad y eficiencia mediante el uso de información, procesos y tecnología.</li>
                        <li class="obj"><strong>Mejor gestión pública:</strong> aumentando la calidad de funcionamiento de este Organismo, contribuyendo a la creación de una administración pública más eficaz en sus logros, más eficiente en el uso de sus recursos y más transparente en sus métodos.</li>
                        <li class="obj"><strong>Reducción de costos:</strong> disminuyendo los costos operativos del estado y de los ciudadanos en su relación con él.</li>
                        <li class="obj"><strong>Transparencia e Integración:</strong> utilizando todo el poder de las Tecnologías de la Información y la Comunicación para facilitar el acceso a ella y simplificar la relación entre el ciudadano y el Estado.</li>
                        <li class="obj"><strong>Participación:</strong> generando sistemas de intercambio de información entre el Estado y los ciudadanos.</li>
                    </ul>
                </div>
                <br />
                <div id="cartel">
                    Estar inscripto en el Registro Nacional del Teatro Independiente es requisito indispensable para poder acceder a cualquiera de los beneficios del I.N.T. <br />(o a los beneficios derivados de los Convenios con otras entidades). <br />Para poder realizar el trámite de "Registro" cada integrante deberá haber realizado previamente su Alta Individual desde esta misma plataforma. 
                </div>
                <br />
                <a class="lnk" href="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" target="_blank">Ver Reglamentación del Registro Nacional del Teatro Independiente</a>
                <div>&nbsp;</div>
            </div>
        </div>
    </div>
    <footer style="margin-top: 30px; padding-top:20px">
        <div class="container" style="height:50px">
            <div class="row">
                <div class="col-md-3">
                    <div class="footer-logo">
                        <img src="//inteatro.gob.ar/content/images/logo-blanco.png" />
                    </div>
                    <div class="footer-text" style="width:300px">
                        <strong>Av. Santa Fe 1243 - 7º Piso<br />+54 (11) 48.15.66.61 (203 y 204)</strong><br />
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
                    <p style="clear: both;text-align: right;font-size: 12px;">Versión 2.7.0 11-11-2022</p>                    
                </div>
            </div>
        </div>
    </footer>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/js/all.min.js" integrity="sha512-YSdqvJoZr83hj76AIVdOcvLWYMWzy6sJyIMic2aQz5kh2bPTd9dzY3NtdeEAzPp/PhgZqr4aJObB3ym/vsItMg==" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://kit.fontawesome.com/900cb600a9.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="menuIndex.js"></script>
</body>
</html>
