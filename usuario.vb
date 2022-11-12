<Serializable()> Public Class usuario
    Public Sub New()

    End Sub


#Region "Properties"


    Private _usuario As String
    Public Property Usuario() As String
        Get
            Return _usuario
        End Get
        Set(ByVal value As String)
            _usuario = value
        End Set
    End Property


    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property


    Private _direccion As String
    Public Property Direccion() As String
        Get
            Return _direccion
        End Get
        Set(ByVal value As String)
            _direccion = value
        End Set
    End Property


    Private _cpostal As String
    Public Property CPostal() As String
        Get
            Return _cpostal
        End Get
        Set(ByVal value As String)
            _cpostal = value
        End Set
    End Property



    Private _provincia As String
    Public Property Provincia() As String
        Get
            Return _provincia
        End Get
        Set(ByVal value As String)
            _provincia = value
        End Set
    End Property


    Private _persona As String
    Public Property Persona() As String
        Get
            Return _persona
        End Get
        Set(ByVal value As String)
            _persona = value
        End Set
    End Property


    Private _codigo As Integer
    Public Property Codigo() As Integer
        Get
            Return _codigo
        End Get
        Set(ByVal value As Integer)
            _codigo = value
        End Set
    End Property


    Private _sexo As String
    Public Property Sexo() As String
        Get
            Return _sexo
        End Get
        Set(ByVal value As String)
            _sexo = value
        End Set
    End Property



    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property


    Private _denominacion As String
    Public Property Denominacion() As String
        Get
            Return _denominacion
        End Get
        Set(ByVal value As String)
            _denominacion = value
        End Set
    End Property


    Private _domicilio As String
    Public Property Domicilio() As String
        Get
            Return _domicilio
        End Get
        Set(ByVal value As String)
            _domicilio = value
        End Set
    End Property



    Private _telefonoParticular As String
    Public Property TelefonoParticular() As String
        Get
            Return _telefonoParticular
        End Get
        Set(ByVal value As String)
            _telefonoParticular = value
        End Set
    End Property


    Private _telefonocelular As String
    Public Property TelefonoCelular() As String
        Get
            Return _telefonocelular
        End Get
        Set(ByVal value As String)
            _telefonocelular = value
        End Set
    End Property

    Private _codprovin As Int32
    Public Property codprovin() As Int32
        Get
            Return _codprovin
        End Get
        Set(ByVal value As Int32)
            _codprovin = value
        End Set
    End Property



    Private _personeria As String
    Public Property personeria() As String
        Get
            Return _personeria
        End Get
        Set(ByVal value As String)
            _personeria = value
        End Set
    End Property


    Private _localidad As String
    Public Property localidad() As String
        Get
            Return _localidad
        End Get
        Set(ByVal value As String)
            _localidad = value
        End Set
    End Property


    Private _cambclave As Integer
    Public Property cambclave() As Integer
        Get
            Return _cambclave
        End Get
        Set(ByVal value As Integer)
            _cambclave = value
        End Set
    End Property


    Private _inhibido As Boolean
    Public Property inhibido() As Boolean
        Get
            Return _inhibido
        End Get
        Set(ByVal value As Boolean)
            _inhibido = value
        End Set
    End Property


#End Region




End Class
