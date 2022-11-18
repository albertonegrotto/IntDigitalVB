Public Partial Class inhibido
    Inherits System.Web.UI.Page

    Dim quien As usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            quien = CType(Session("usuario"), usuario)
            lblNombre.Text = quien.Nombre
            Session("codigo") = quien.Codigo
            SqlDataSource1.DataBind()
            SqlDataSource2.DataBind()
            GridView1.DataBind()
            grillamisregistros.DataBind()
        End If
        Dim controla As String
        controla = Request.Params("__EVENTTARGET")
        If controla = "CierraSession" Then
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("index.aspx")
        End If
    End Sub

    Function getpersona() As String
        Return "<strong>Persona: </strong><span class='datolight'>" + quien.Persona + "</span>"
    End Function


    Function getCodigo() As String
        Return "<strong>Codigo: </strong><span class='datolight'>" + quien.Codigo.ToString + "</span>"
    End Function

    Function getCuit() As String
        Return "<strong>CUIT/CUIL: </strong><span class='datolight'>" + quien.Usuario.ToString + "</span>"

    End Function
    Function getSexo() As String
        If quien.Persona = "HUMANA" Then
            Return "<strong>Sexo: </strong><span class='datolight'>" + quien.Sexo.ToString + "</span>"
        Else
            Return "<strong>Personería: </strong><span class='datolight'>" + quien.personeria + "</span>"

        End If

    End Function
    Function getDomicilio() As String
        Return "<strong>Domicilio: </strong><span class='datolight'>" + quien.Direccion.ToString + "</span>"

    End Function
    Function getLocalidad() As String
        Return "<strong>Localidad: </strong><span class='datolight'>" + quien.localidad.ToString + "</span>"

    End Function
    Function getProvincia() As String
        Return "<strong>Provincia: </strong><span class='datolight'>" + quien.Provincia.ToString + "</span>"

    End Function
    Function getEmail() As String
        Return "<strong>Email: </strong><span class='datolight'>" + quien.Email.ToString + "</span>"

    End Function

    Function getTel() As String
        Return "<strong>Teléfono Particular: </strong><span class='datolight'>" + quien.TelefonoParticular + "</span>"

    End Function

    Function getCelu() As String
        If quien.Persona = "HUMANA" Then
            Return "<strong>Teléfono Celular: </strong><span class='datolight'>" + quien.TelefonoParticular + "</span>"
        Else
            Return ""
        End If

    End Function

End Class