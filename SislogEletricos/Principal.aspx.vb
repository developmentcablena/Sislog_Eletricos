

Public Class Principal
        Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Dim targetControl As String = Request("__EVENTTARGET")
            ' Aqui você coloca a parte final do ID do controle dentro do ascx
            If Not String.IsNullOrEmpty(targetControl) AndAlso targetControl.Contains("txtCodigoRecusado") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AbrirModal", "abrirModalRecusados(); abrirModalEmbarque2Recusado();", True)

            ElseIf Not String.IsNullOrEmpty(targetControl) AndAlso targetControl.Contains("txtCodigo") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AbrirModal", "abrirModalEmbarque();", True)
            End If
        End If



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
            btnRelatorio.Enabled = False

            Select Case funcao
                Case "Adiministrador"
                    btnRecebimento.Enabled = True
                    btnAutorizar.Enabled = True
                    btnEmbarque.Enabled = True
                    btnHistorico.Enabled = True
                    btnLiberar.Enabled = True
                    btnUsuario.Enabled = True
                    btnRecusados.Visible = True
                    btnRelatorio.Enabled = True


                Case "Liberador"
                    btnRecebimento.Enabled = True
                    btnAutorizar.Enabled = True
                    btnEmbarque.Enabled = True
                    btnHistorico.Enabled = True
                    btnUsuario.Enabled = True
                    btnRelatorio.Enabled = True

                Case "Cadastrar"
                    btnRecebimento.Enabled = True
                    btnEmbarque.Enabled = True
                    btnRecusados.Visible = True
                    btnRelatorio.Enabled = True

                Case "Portaria"
                    btnLiberar.Enabled = True
            End Select
        End If

    End Sub



    Protected Sub Sair(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirmodal", "abrirModalSair();", True)
    End Sub

    Protected Sub btnRelatorio_Click(sender As Object, e As EventArgs)
        Response.Redirect("Relatorio.aspx")
    End Sub
End Class
