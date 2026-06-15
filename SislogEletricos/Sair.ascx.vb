Partial Public Class Sair
    Inherits System.Web.UI.UserControl
    Private vConexao As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        vConexao = Session("vConexao")


        If Not IsPostBack Then
            Dim versao As Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            lbl_versao.Text = "Versão: " & versao.ToString()


            If vConexao = "ConectarBD" Then
                lbl_descricao_emperesa.Text = "Cablena do Brasil LTDA (ELÉTRICOS)"
            ElseIf vConexao = "ConectarBD_Telecom" Then
                lbl_descricao_emperesa.Text = "Cablena do Brasil LTDA (TELECOM)"
            End If

        End If
    End Sub

    Protected Sub Btn_close_Click()
        Session("FuncaoUsuario") = ""
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manual.aspx")
    End Sub
End Class