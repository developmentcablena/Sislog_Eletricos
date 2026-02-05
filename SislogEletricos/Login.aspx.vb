Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Configuration
Imports System.Data

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim versao As Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            lbl_versao.Text = "Versão: " & versao.ToString()
        End If




    End Sub


    Private Function valorUsuarioBanco(userId As String) As String
        Dim vConexaoEmpresa As String = ddl_Empresa.Text
        Dim conexao As String = String.Empty

        If vConexaoEmpresa = "Cablena do Brasil LTDA (ELÉTRICOS)" Then
            conexao = "ConectarBD"
        ElseIf vConexaoEmpresa = "Cablena do Brasil LTDA (TELECOM)" Then
            conexao = "ConectarBD_Telecom"
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro na conexao", "alert('Erro na conexão!!')", True)
        End If

        HttpContext.Current.Session("vConexao") = conexao
        Debug.WriteLine("" & Session("vConexao"))

        ' Obtém a string de conexão do Web.config
        Dim connectionString As String = ConfigurationManager.ConnectionStrings($"{conexao}").ConnectionString
        Dim query As String = "SELECT Usuario, Funcao, Nome, Empresa, ID_USER FROM tb_Usuarios WHERE Usuario = @usuario"
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
                            Dim valorEmpresa As Integer = reader("Empresa").ToString()
                            Dim valorID_User As Integer = reader("ID_USER").ToString

                            ' Salva os valores na Sessão
                            HttpContext.Current.Session("FuncaoUsuario") = funcaoBD
                            HttpContext.Current.Session("Usuario") = usuarioV
                            HttpContext.Current.Session("Nome") = nomeV
                            HttpContext.Current.Session("Empresa") = valorEmpresa
                            HttpContext.Current.Session("ID_USER") = valorID_User
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

    'Private Function AutenticarUsuario(dominio As String, usuario As String, senha As String) As Boolean
    '    Try
    '        ' Concatena o domínio com o usuário (exemplo: empresa\usuario)
    '        Dim caminhoLDAP As String = "LDAP://" & dominio
    '        Dim entry As New DirectoryEntry(caminhoLDAP, usuario, senha)

    '        ' Força autenticação no AD
    '        Dim obj As Object = entry.NativeObject
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Private Function AutenticarUsuario(dominio As String, usuario As String, senha As String) As Boolean
        ' Concatena o nome do usuário com o domínio para autenticação
        Dim usuarioAD As String = dominio & "\" & usuario  ' Exemplo: cablenabr\usuario
        Dim caminhoLDAP As String             '= "LDAP://telsrv005/DC=cablenabr,DC=local"
        Dim conexao As String = Session("vConexao")

        If conexao = "ConectarBD" Then
            caminhoLDAP = "LDAP://elesrv027/DC=cablenabr,DC=local"
            Dim entry As New DirectoryEntry(caminhoLDAP, usuarioAD, senha)   ' Tenta conectar ao Active Directory
            Dim obj As Object = entry.NativeObject ' Força autenticação no AD
            Return True

        ElseIf conexao = "ConectarBD_Telecom" Then
            caminhoLDAP = "LDAP://telsrv005/DC=cablenabr,DC=local"
            Dim entry As New DirectoryEntry(caminhoLDAP, usuarioAD, senha)   ' Tenta conectar ao Active Directory
            Dim obj As Object = entry.NativeObject ' Força autenticação no AD
            Return True
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('ERRO na hora de autenticar usuário!')", True)
            Return False
        End If
    End Function


    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        Dim usuario As String = txtUsuario.Text.Trim()
        Dim senha As String = txtSenha.Text.Trim()
        Dim dominio As String = "cablenabr.local"
        Dim vConexao As String = Session("vConexao")

        If String.IsNullOrEmpty(Trim(ddl_Empresa.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor selecione a Empresa!')", True)
            ddl_Empresa.Focus()
            Exit Sub
        End If

        If usuario = valorUsuarioBanco(usuario) Then
            If Trim(ddl_Empresa.Text) = "Cablena do Brasil LTDA (ELÉTRICOS)" And Session("Empresa") = 1 Then
                '
            ElseIf Trim(ddl_Empresa.Text) = "Cablena do Brasil LTDA (ELÉTRICOS)" And Session("Empresa") = 2 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Usuário não tem permissão para acessar a Empresa TELECOM!')", True)
                Exit Sub
            ElseIf Trim(ddl_Empresa.Text) = "Cablena do Brasil LTDA (TELECOM)" And Session("Empresa") = 1 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Usuário não tem permissão para acessar a Empresa TELECOM!')", True)
                Exit Sub
            ElseIf Trim(ddl_Empresa.Text) = "Cablena do Brasil LTDA (TELECOM)" And Session("Empresa") = 2 Then
                '
            Else

            End If

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