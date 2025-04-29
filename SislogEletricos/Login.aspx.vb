Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Configuration
Imports System.Data

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private Function valorUsuarioBanco(userId As String) As String
        ' Obtém a string de conexão do Web.config
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim query As String = "SELECT Usuario, Funcao, Nome FROM tb_Usuarios WHERE Usuario = @usuario"
        Dim usuarioBD As String = ""

        ' Usando bloco Using para garantir fechamento da conexão
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@usuario", userId)

                Try
                    connection.Open()

                    Using reader As SqlDataReader = command.ExecuteReader()
                        ' Verifica se encontrou algum resultado
                        If reader.HasRows Then
                            reader.Read() ' Move para a primeira linha

                            ' Obtém os valores e trata possíveis valores NULL
                            Dim funcaoBD As String = If(Not reader.IsDBNull(reader.GetOrdinal("Funcao")), reader("Funcao").ToString(), "")
                            Dim usuarioV As String = If(Not reader.IsDBNull(reader.GetOrdinal("Usuario")), reader("Usuario").ToString(), "")
                            Dim nomeV As String = If(Not reader.IsDBNull(reader.GetOrdinal("Nome")), reader("Nome").ToString(), "")

                            ' Salva os valores na Sessão
                            HttpContext.Current.Session("FuncaoUsuario") = funcaoBD
                            HttpContext.Current.Session("Usuario") = usuarioV
                            HttpContext.Current.Session("Nome") = nomeV

                            ' Define o retorno com o nome do usuário
                            usuarioBD = usuarioV
                        End If
                    End Using

                Catch ex As Exception
                    ' Log do erro (opcional: salvar em um log ou mostrar uma mensagem)
                    Throw New Exception("Erro ao buscar dados do usuário: " & ex.Message)
                End Try
            End Using
        End Using

        Return usuarioBD
    End Function




    Private Function AutenticarUsuario(dominio As String, usuario As String, senha As String) As Boolean
        Try
            ' Concatena o domínio com o usuário (exemplo: empresa\usuario)
            Dim caminhoLDAP As String = "LDAP://" & dominio
            Dim entry As New DirectoryEntry(caminhoLDAP, usuario, senha)

            ' Força autenticação no AD
            Dim obj As Object = entry.NativeObject
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        Dim usuario As String = txtUsuario.Text.Trim()
        Dim senha As String = txtSenha.Text.Trim()
        Dim dominio As String = "cablenabr.local"

        If usuario = valorUsuarioBanco(usuario) Then
            If AutenticarUsuario(dominio, usuario, senha) Then
                Response.Redirect("Principal.aspx")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Usuario ou senha Invalidos!')", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Usuario não cadastrado!')", True)
        End If

    End Sub
End Class