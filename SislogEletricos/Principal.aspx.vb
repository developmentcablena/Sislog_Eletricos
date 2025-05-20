

Public Class Principal
        Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("FuncaoUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            Dim funcao As String = Session("FuncaoUsuario")

            btnRecebimento.Enabled = False
            btnAutorizar.Enabled = False
            btnEmbarque.Enabled = False
            btnHistorico.Enabled = False
            btnLiberar.Enabled = False
            btnUsuario.Enabled = False
            btnRecusados.Visible = False

            Select Case funcao
                Case "Adiministrador"
                    btnRecebimento.Enabled = True
                    btnAutorizar.Enabled = True
                    btnEmbarque.Enabled = True
                    btnHistorico.Enabled = True
                    btnLiberar.Enabled = True
                    btnUsuario.Enabled = True
                    btnRecusados.Visible = True

                Case "Liberador"
                    btnRecebimento.Enabled = True
                    btnAutorizar.Enabled = True
                    btnEmbarque.Enabled = True
                    btnHistorico.Enabled = True
                    btnUsuario.Enabled = True

                Case "Cadastrar"
                    btnRecebimento.Enabled = True
                    btnEmbarque.Enabled = True
                    btnRecusados.Visible = True

                Case "Portaria"
                    btnLiberar.Enabled = True
            End Select
        End If

    End Sub
End Class
