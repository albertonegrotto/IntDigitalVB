Module Constantes

    'Mail  

    'Producción
    Public MAIL_SMTP_SERVER As String = "mail.inteatro.gob.ar"
    'Public MAIL_WEB_SERVER As String = "http://serviciosweb.inteatro.gob.ar:8081/intdig/"
    Public MAIL_WEB_SERVER As String = "http://serviciosweb.inteatro.gob.ar:8081/intdig_prueba/"

    Public MAIL_LOGIN_PAGE As String = "default.aspx"
    'Mail de la registración
    Public MAIL_SENDER As String = "intdigital@inteatro.gob.ar"
    Public MAIL_CREDENTIALS_USER As String = "intdigital@inteatro.gob.ar"
    Public MAIL_CREDENTIALS_PASSWORD As String = "INTdigit2011"

    'Mail del formulario de contacto
    Public MAIL_SENDER_FORM As String = "info.intdigital@inteatro.gob.ar"
    Public MAIL_CREDENTIALS_USER_FORM As String = "info.intdigital@inteatro.gob.ar"
    Public MAIL_CREDENTIALS_PASSWORD_FORM As String = "infoINTdigit2011"
    Public MAIL_ENABLE_SSL As Boolean = False

    'Mail de Control
    'Public MAIL_CONTROL As String = "controlintdigital@inteatro.gob.ar"
    Public MAIL_CONTROL As String = "testing.intdig@inteatro.gob.ar"
    'Public MAIL_CONTROL As String = "anegrotto@fibertel.com.ar"

    'End of Mail

    'RegisDigPalabras
    Public Const RD_F_CODIGO_REGISDIG As Integer = 0
    Public Const RD_F_TELEFONOS As Integer = 1
    Public Const RD_F_DIRECCION_DE_CORREO_ELECTRÓNICO As Integer = 2

    'RegistroPalabras
    Public Const F_CODIGO_REGISTRO As Integer = 0
    Public Const F_APELLIDO_Y_NOMBRE_DEL_RESPONSABLE As Integer = 1
    Public Const F_CANTIDAD_DE_INTEGRANTES As Integer = 2
    Public Const F_CANTIDAD_DE_LOCALIDADES As Integer = 3
    Public Const F_CARACTERISTICAS_DEL_ESPACIO_ESCENICO As Integer = 4
    Public Const F_CURRICULUM As Integer = 5
    Public Const F_DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL As Integer = 6
    Public Const F_DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL_DEL_GRUPO As Integer = 7
    Public Const F_DIRECCION_DE_CORREO_ELECTRÓNICO As Integer = 8
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_ONG As Integer = 9
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_PUBLICACION As Integer = 10
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_SALA As Integer = 11
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO As Integer = 12
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO As Integer = 13
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_GRUPO As Integer = 14
    Public Const F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE As Integer = 15
    Public Const F_DOMICILIO_DE_LA_SALA As Integer = 16
    Public Const F_PAGINA_WEB_DE_LA_ONG As Integer = 17
    Public Const F_PAGINA_WEB_DE_LA_PUBLICACION As Integer = 18
    Public Const F_PAGINA_WEB_DE_LA_SALA As Integer = 19
    Public Const F_PAGINA_WEB_DEL_ESPECTACULO As Integer = 20
    Public Const F_PAGINA_WEB_DEL_EVENTO As Integer = 21
    Public Const F_PAGINA_WEB_DEL_GRUPO As Integer = 22
    Public Const F_TELEFONO_DE_LA_ONG As Integer = 23
    Public Const F_TELEFONO_DE_LA_REDACCION As Integer = 24
    Public Const F_TELEFONO_DE_LA_SALA As Integer = 25
    Public Const F_TELEFONOS As Integer = 26
    Public Const F_TELEFONOS_DEL_RESPONSABLE As Integer = 27
    Public Const F_TIPO_DE_EVENTO As Integer = 28


End Module
