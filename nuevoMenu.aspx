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
		<link rel="shortcut icon" href="../favicon.ico"> 
		<link rel="stylesheet" type="text/css" href="css/default.css" />
		<link rel="stylesheet" type="text/css" href="css/component.css" />
		<link rel="stylesheet" type="text/css" href="css/icomoon/style.css" />
		<link rel="stylesheet" type="text/css" href="css/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/style2.css" />
        <link href='http://fonts.googleapis.com/css?family=Terminal+Dosis' rel='stylesheet' type='text/css' />
		<script src="js/modernizr.custom.js"></script>
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
		<div class="container" >	

			<header>
				
				
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
				
                							
			</header>
			<div class="main clearfix">

				<nav id="menu" class="nav">					
					<ul class="menuContainer">
						<li id="Uno" class="beHere" onclick="clickOnMe(this)">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-profile"></i>
								</span>
								<span class="text">Datos Personales</span>
							</a>
						</li>
						<li id="Dos" class="beHere" onclick="clickOnMe(this)">
							<a href="#">
								<span class="icon"> 
									<i aria-hidden="true" class="icon-services"></i>
								</span>
								<span class="text">Configuración</span>
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
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-folder-open-o"></i>
								</span>
								<span class="text">Mis Trámites</span>
							</a>
						</li>
						<li id="Seis" class="beHere">
							<a href="#">
								<span class="icon">
									<i aria-hidden="true" class="icon-stack"></i>
								</span>
								<span class="text">Mis Rendiciones</span>
							</a>
						</li>						
						<li id="Siete" class="beHere">
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
						</li>
					</ul>

				</nav>
				<ul class="ca-menu" style="margin:0">
					<div class="datosPersonales">
						<li>
							<a href="ActualIndivFis.aspx">
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
							<a href="#">
								<span class="ca-icon">x</span>
								<div class="ca-content">
									<h2 class="ca-main">Cambiar Contraseña</h2>
					
								</div>
							</a>
						</li>
						<li>
							<a href="#">
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
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Sala Teatro Independiente</h2>

									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Grupo Teatro Independiente</h2>

									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Grupo Comunitario</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Grupo Vocacional</h2>

									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Capacitador Técnino</h2>

									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Espectáculo Concertado</h2>

									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Publicación</h2>
									</div>
								</a>
							</li>
							<li >
								<a href="#">
									<span class="ca-icon">,</span>
									<div class="ca-content">
										<h2 class="ca-main">Evento</h2>

									</div>
								</a>
							</li>                        
						

					</div>
					<div class="registros">
						<li >
							<a href="#">
								<span class="ca-icon">*</span>
								<div class="ca-content">
									<h2 class="ca-main">Actualización de Registro</h2>
								</div>
							</a>
						</li>
						<li >
							<a href="#">
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

		<script>
            function nuevoR() {
                $('.subCategorias').toggle(1000);
            }

            function clickOnMe(li) {
                for (let i = 0; i < 8; i++) {
                    let current = $('.beHere')[i];
                    if (current.id !== li.id) {
                        $(current).toggle(1000);
                    }
                    else {
                        switch (current.id) {
                            case 'Uno': {
                                $('.datosPersonales').toggle(1000);
                                break;
                            }
                            case 'Dos': {
                                $('.config').toggle(1000);
                                break;
                            }
                            case 'Tres': {
                                $('.nuevoRegistro').toggle(1000);
                                break;
                            }
                            case 'Cuatro': {
                                $('.registros').toggle(1000);
                                break;
                            }
                        }
                    }

                }

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
            };

            //  Creating our button in JS for smaller screens
            var menuElements = document.getElementById('menu');
            menuElements.insertAdjacentHTML('afterBegin', '<button type="button" id="menutoggle" class="navtoogle" aria-hidden="true"><i aria-hidden="true" class="icon-menu"> </i> Menu</button>');

            //  Toggle the class on click to show / hide the menu
            document.getElementById('menutoggle').onclick = function () {
                changeClass(this, 'navtoogle active', 'navtoogle');
            }

            // http://tympanus.net/codrops/2013/05/08/responsive-retina-ready-menu/comment-page-2/#comment-438918
            document.onclick = function (e) {
                var mobileButton = document.getElementById('menutoggle'),
                    buttonStyle = mobileButton.currentStyle ? mobileButton.currentStyle.display : getComputedStyle(mobileButton, null).display;

                if (buttonStyle === 'block' && e.target !== mobileButton && new RegExp(' ' + 'active' + ' ').test(' ' + mobileButton.className + ' ')) {
                    changeClass(mobileButton, 'navtoogle active', 'navtoogle');
                }
            }
        </script>
		<!-- <script src="//tympanus.net/codrops/adpacks/demoad.js"></script> -->
		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>

	</body>
</html>