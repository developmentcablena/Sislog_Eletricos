Partial Public Class Sair
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Btn_close_Click()
        Session("FuncaoUsuario") = ""
        Response.Redirect("Login.aspx")
    End Sub
End Class