
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
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
                    ' 1 - Buscar na API
                    RazaoSocial = BuscarRazaoSocialPorCNPJ(cnpj)

                    If String.IsNullOrEmpty(RazaoSocial) Then
                        Throw New Exception("CNPJ NÃO ENCONTRADO EM NENHUMA BASE")
                    End If

                    ' 2 - Inserir fornecedor automaticamente
                    Using cmdInsertFornecedor As New SqlCommand(
                        "INSERT INTO Fornecedores (CNPJ, RAZAO_SOCIAL) VALUES (@cnpj, @razao)", conn)

                        cmdInsertFornecedor.Parameters.Add("@cnpj", SqlDbType.VarChar, 14).Value = cnpj
                        cmdInsertFornecedor.Parameters.Add("@razao", SqlDbType.VarChar, 200).Value = RazaoSocial

                        cmdInsertFornecedor.ExecuteNonQuery()
                    End Using
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


    Private Function BuscarRazaoSocialPorCNPJ(cnpj As String) As String
        Try
            ' 🔥 ESSENCIAL
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim url As String = "https://www.receitaws.com.br/v1/cnpj/" & cnpj

            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"
            request.UserAgent = "Mozilla/5.0"
            request.Timeout = 10000

            Dim response As HttpWebResponse = request.GetResponse()
            Dim reader As New StreamReader(response.GetResponseStream())

            Dim json As String = reader.ReadToEnd()

            Dim serializer As New JavaScriptSerializer()
            Dim dados = serializer.Deserialize(Of Dictionary(Of String, Object))(json)

            If dados.ContainsKey("status") AndAlso dados("status").ToString() = "OK" Then
                Return dados("nome").ToString()
            End If

        Catch ex As Exception
            Throw New Exception("Erro ao consultar API: " & ex.Message)
        End Try

        Return Nothing
    End Function




End Class