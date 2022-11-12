Public Class desvinculado

    Private _IDIntegrante As Integer
    Public Property IDIntegrante() As Integer
        Get
            Return _IDIntegrante
        End Get
        Set(ByVal value As Integer)
            _IDIntegrante = value
        End Set
    End Property


    Private _IDRegistro As Integer
    Public Property IDRegistro() As Integer
        Get
            Return _IDRegistro
        End Get
        Set(ByVal value As Integer)
            _IDRegistro = value
        End Set
    End Property


    Private _Apellido As String
    Public Property Apellido() As String
        Get
            Return _Apellido
        End Get
        Set(ByVal value As String)
            _Apellido = value
        End Set
    End Property


    Private _Nombre As String
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property


    Private _CUIL As String
    Public Property CUIL() As String
        Get
            Return _CUIL
        End Get
        Set(ByVal value As String)
            _CUIL = value
        End Set
    End Property



    Private _TipoRegistro As String
    Public Property TipoRegistro() As String
        Get
            Return _TipoRegistro
        End Get
        Set(ByVal value As String)
            _TipoRegistro = value
        End Set
    End Property


    Private _NombreRegistro As String
    Public Property NombreRegistro() As String
        Get
            Return _NombreRegistro
        End Get
        Set(ByVal value As String)
            _NombreRegistro = value
        End Set
    End Property


    Private _correoIntegrante As String
    Public Property correoIntegrante() As String
        Get
            Return _correoIntegrante
        End Get
        Set(ByVal value As String)
            _correoIntegrante = value
        End Set
    End Property


    Private _correoResponsable As String
    Public Property correoResponsable() As String
        Get
            Return _correoResponsable
        End Get
        Set(ByVal value As String)
            _correoResponsable = value
        End Set
    End Property


    Private _correoProvincia As String
    Public Property correoProvincia() As String
        Get
            Return _correoProvincia
        End Get
        Set(ByVal value As String)
            _correoProvincia = value
        End Set
    End Property


    Private _datoIntegrante As String
    Public ReadOnly Property datoIntegrante() As String
        Get
            Return _datoIntegrante
        End Get

    End Property



    Private _datoRegistro As String
    Public ReadOnly Property datoRegistro() As String
        Get
            Return _datoRegistro
        End Get

    End Property


    Private Sub GetDatoIntegrante()
        _datoIntegrante = Me._Apellido + " " + Me._Nombre + ", CUIL N° " + Me._CUIL
    End Sub

    Private Sub GetDatoRegistro()
        _datoRegistro = Me._TipoRegistro + " " + Me._NombreRegistro
    End Sub

    Public Sub New(ByVal id As Integer)
        Me._IDIntegrante = id
        Dim desv As New DesvincularDataContext()
        Dim data = From d In desv.Integrantes Where d.idIntegrante = id Select New With {
            .IDReg = d.REGISTRO.CODIGO,
            .Reg = d.REGISTRO.DENOMINACION,
            .tipoReg = d.REGISTRO.SECTORES.DESCRIP,
            .apellido = d.REGISDIG.APELLIDO,
            .Nombre = d.REGISDIG.NOMBRE,
            .CUIL = d.REGISDIG.CUIL.ToString,
            .Responsable = d.REGISTRO.REGISDIG.APELLIDO & " " & d.REGISTRO.REGISDIG.NOMBRE,
            .correoInt = d.REGISDIG.EMAIL,
            .correoResp = d.REGISTRO.REGISDIG.EMAIL,
            .correoProv = d.REGISTRO.REGISDIG.ProvinciasMail.mail}

        Me._Apellido = data.First.apellido.Trim()
        Me._Nombre = data.First.Nombre.Trim()
        Me._IDRegistro = data.First.IDReg
        Me._NombreRegistro = data.First.Reg.Trim()
        Me._TipoRegistro = data.First.tipoReg.Trim()
        Me._CUIL = data.First.CUIL
        Me._correoIntegrante = data.First.correoInt.Trim()
        Me._correoResponsable = data.First.correoResp.Trim()
        Me._correoProvincia = data.First.correoProv.Trim()

        GetDatoIntegrante()
        GetDatoRegistro()
    End Sub
End Class
