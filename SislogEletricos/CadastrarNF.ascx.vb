
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class CadastrarNF
    Inherits System.Web.UI.UserControl

    Private vConexao As String
    Private RazaoSocial
    Private vUsuario As String
    Private numeroNF As Integer
    Private chave As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        vConexao = Session("vConexao")
        vUsuario = Session("Usuario")
        txtDANFE.Focus()
    End Sub

    Protected Sub Btn_close_Click()
        chave = If(txtDANFE.Text, String.Empty).Trim()

        ' Verifica vazio/branco
        If String.IsNullOrWhiteSpace(chave) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Erro", "alert('Danfe inválido!'); abrirModalCadastrar();", True)
            txtDANFE.Text = ""
            txtDANFE.Focus()
            chave = ""
            Exit Sub
        End If
        ' Valida: exatamente 44 dígitos (apenas números)
        If Not Regex.IsMatch(chave, "^\d{44}$") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Erro2", "alert('Danfe inválido! A chave deve conter exatamente 44 números.'); abrirModalCadastrar();", True)
            txtDANFE.Text = ""
            txtDANFE.Focus()
            chave = ""
            Exit Sub
        End If
        Try
            If CadastrarNota(chave) Then

                Session("FlashMsg") = $"✅ Nota {numeroNF} cadastrada com sucesso!"
                Response.Redirect("EntradaNotas.aspx?ok=1", False)
                Context.ApplicationInstance.CompleteRequest()
                Exit Sub
            Else
                Session("FlashMsg") = "⚠️ Nenhuma nota foi inserido."
                Response.Redirect("EntradaNotas.aspx?ok=1", False)
                Context.ApplicationInstance.CompleteRequest()
                Exit Sub
            End If

        Catch ex As Exception
            Session("FlashMsg") = "Fornecedor não cadastrado!"
            Response.Redirect("EntradaNotas.aspx?ok=1", False)
            Context.ApplicationInstance.CompleteRequest()
            Exit Sub
        End Try
    End Sub

    Private Function CadastrarNota(ByVal danfeNF As String) As Boolean
        Dim vData As String = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

        'Response.Write("<script>alert('ENTROU NA FUNÇÃO');</script>")

        If danfeNF.Length <> 44 Then
            Throw New Exception("TAMANHO INVALIDO: " & danfeNF.Length)
        End If

        Dim cnpj As String = danfeNF.Substring(6, 14)
        numeroNF = Integer.Parse(danfeNF.Substring(25, 9))

        'Response.Write("<script>alert('CNPJ: " & cnpj & " | NF: " & numeroNF & "');</script>")

        Dim connectionString = ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString
        Using conn As New SqlConnection(connectionString)
            ' Response.Write("<script>alert('ANTES DE ABRIR CONEXÃO');</script>")
            conn.Open()
            '.Write("<script>alert('CONEXÃO ABERTA');</script>")

            Using cmdBusca As New SqlCommand(
                "SELECT RAZAO_SOCIAL FROM Fornecedores WHERE CNPJ = @CNPJ", conn)

                cmdBusca.Parameters.Add("@CNPJ", SqlDbType.VarChar, 14).Value = cnpj

                RazaoSocial = cmdBusca.ExecuteScalar()

                If RazaoSocial Is Nothing Then


                    Throw New Exception("FORNECEDOR NÃO ENCONTRADO")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta4", "alert('FORNECEDOR NÃO ENCONTRADO.'); abrirModalCadastrar();", True)
                End If
            End Using

            'Response.Write("<script>alert('VAI INSERIR');</script>")

            Using cmdInsert As New SqlCommand(
                "INSERT INTO EntradaNotas ( CHAVE_ACESSO, DATA_CADASTRO, NOTA_FISCAL, CNPJ, RAZAO_SOCIAL, USUARIO) " &
                "VALUES (@chave, @data, @nf, @cnpj, @razaoSocial, @usuario)", conn)

                cmdInsert.Parameters.Add("@chave", SqlDbType.VarChar, 44).Value = danfeNF
                cmdInsert.Parameters.Add("@data", SqlDbType.DateTime).Value = vData
                cmdInsert.Parameters.Add("@nf", SqlDbType.Int).Value = numeroNF
                cmdInsert.Parameters.Add("@cnpj", SqlDbType.VarChar, 14).Value = cnpj
                cmdInsert.Parameters.Add("@razaoSocial", SqlDbType.VarChar, 200).Value = RazaoSocial
                cmdInsert.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = vUsuario
                Dim linhas = cmdInsert.ExecuteNonQuery()
                'Response.Write("<script>alert('LINHAS INSERIDAS: " & linhas & "');</script>")

                Return linhas > 0
            End Using
        End Using
    End Function
End Class