Public Partial Class confirmaRegistro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sResultado As String
        Dim sMensaje As String
        Dim sTipo As String
        Dim sCUILResponsable As String

        If Not Page.IsPostBack Then
            'If User.Identity.IsAuthenticated Then
            'End If
            If Not Request.QueryString("r") Is Nothing Then
                sResultado = Request.QueryString("r").ToUpper
            End If
            sMensaje = Request.QueryString("m")
            sTipo = Request.QueryString("t")

            If sTipo = "f" Then
                lblTitulo.Text = "Confirmación de Persona Física"
            ElseIf sTipo = "j" Then
                lblTitulo.Text = "Confirmación de Persona Jurídica"
            Else
                lblTitulo.Text = "Confirmación de Envío de Registro"
            End If

            If Not Session("CUIT") Is Nothing Then
                sCUILResponsable = Session("CUIT")
            Else
                sCUILResponsable = Session("CUIT_TEMP")
            End If
            lblResultado.Text = sCUILResponsable
            If sResultado = "OK" Then
                If sTipo = "f" Or sTipo = "j" Then
                    lblMensaje.Text = "Recibirá en la cuenta de correo electrónico consignada un mail conteniendo la información ingresada, y las debidas instrucciones para finalizar este trámite."
                    ' lblMensaje.Text = "Recibirá en la cuenta de correo electrónico consignada un mail conteniendo la información ingresada, y las debidas instrucciones para finalizar este trámite de &quot;Alta de Datos Individual&quot; y concluir así con la ACTIVACIÓN DE SU CUENTA DE USUARIO."
                Else
                    lblResultado.Text = "Le ha sido enviado un correo electrónico al CUIL " & sCUILResponsable
                    lblMensaje.Text = "Recibirá en la cuenta de correo electrónico consignada un mail "
                    lblMensaje.Text += "conteniendo la información ingresada, el cual deberá validar siguiendo el link de verificación "
                    lblMensaje.Text += "que estará incluído en el mismo mensaje. Luego recibirá en esa misma cuenta de correo el mail de "
                    lblMensaje.Text += "notificación de aceptación del trámite realizado, con las debidas instrucciones para el envío "
                    lblMensaje.Text += "posterior de la confirmación del trámite y demás material que debiera adjuntar."

                End If
            Else
                lblResultado.Text = "No se pudo enviar un correo electrónico de confirmación, por favor intente mas tarde"
                lblMensaje.Text = ""
                'Response.Clear()
                'Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If

    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Response.Clear()
        'Response.Redirect("registroLista.aspx")
        Response.Redirect("menuFinal.aspx")
    End Sub

End Class