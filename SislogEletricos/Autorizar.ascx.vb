Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics

Partial Public Class Autorizar
    Inherits System.Web.UI.UserControl

    Private Shared connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
    Private ReadOnly conexao As String = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarDados()
        End If

        If Session("FuncaoUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
    End Sub

    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        CarregarDados()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalAutorizacao();", True)
    End Sub

    Private Sub CarregarDados()
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
                Dim query As String = "SELECT CadastroID, TipoCadastro, FornecedorCliente, Transportadora, Placa, " &
                                  "CASE WHEN Status = 1 THEN 'Pendente' ELSE '' END AS Status " &
                                  "FROM tb_Cadastro WHERE Status = 1"
                Dim cmd As New SqlCommand(query, conn)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()

                conn.Open()
                da.Fill(dt)
                conn.Close()

                ' Se houver dados, exibe no GridView
                If dt.Rows.Count > 0 Then
                    gvCadastros.DataSource = dt
                    gvCadastros.DataBind()
                Else
                    gvCadastros.DataSource = Nothing
                    gvCadastros.DataBind()
                End If
            End Using
        Catch ex As Exception
            Response.Write("<script>alert('Erro ao carregar dados: " & ex.Message & "');</script>")
        End Try
    End Sub
    Protected Sub gvCadastros_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim commandName As String = e.CommandName
        Dim cadastroID As Integer = Convert.ToInt32(e.CommandArgument)
        Select Case commandName

            Case "Autorizar"
                AutorizarCadastro(cadastroID)

            Case "Rejeitar"
                RejeitarCadastro(cadastroID)

            Case "excluir"
                ExcluirCadastro(cadastroID)

            Case "abrirModal"
                Dim btn As Button = CType(e.CommandSource, Button)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim tipoCadastro As String = row.Cells(1).Text.Trim() ' Ajuste o índice conforme necessário
                AbrirModal(cadastroID, tipoCadastro)
                Debug.WriteLine("valores " & cadastroID & tipoCadastro)
        End Select
    End Sub
    Private Sub AutorizarCadastro(ByVal cadastroID As Integer)
        Try
            Using conn As New SqlConnection(connectionString)
                Dim sql As String = "UPDATE tb_Cadastro SET Status = 2, StatusHorario = 1 WHERE CadastroID = @id"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", cadastroID)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                End Using
            End Using

            ' Mensagem de sucesso
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", $"alert('Cadastro Nº {cadastroID} autorizado com sucesso!'); abrirModalAutorizacao();", True)

            ' Atualiza os dados na interface
            CarregarDados()

        Catch ex As Exception
            ' Mensagem de erro tratada
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Erro ao cadastrar: " & ex.Message.Replace("'", "\'") & "')", True)

        End Try
    End Sub

    Private Sub ExcluirCadastro(ByVal cadastroID As Integer)
        Using conn As New SqlConnection(connectionString)
            Dim sql As String = "UPDATE tb_Cadastro SET Status= 5 WHERE CadastroID= @id"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@id", cadastroID)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", $"alert('Cadastro Nº {cadastroID} excluido!'), abrirModalAutorizacao();", True)
                    CarregarDados()
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Erro ao cadastrar: " & ex.Message.Replace("'", "\'") & "')", True)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub RejeitarCadastro(ByVal cadastroID As Integer)
        HttpContext.Current.Session("vCadastroID") = cadastroID
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalAutorizacao(); abrirModalMotivo();", True)
    End Sub

    Private Sub AbrirModal(ByVal cadastroID As Integer, ByVal tipoCadastro As String)
        CarregarDadosModal(cadastroID, tipoCadastro)
        Debug.WriteLine("valores222 " & cadastroID & tipoCadastro)

        If tipoCadastro = "EMBARQUE" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail2", "abrirModalAutorizacao(); abrirModalEmbarque2();", True)

        ElseIf tipoCadastro = "RECEBIMENTO" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail2", "abrirModalAutorizacao(); abrirModal2Autorizar();", True)
        End If
    End Sub

    Private Sub CarregarDadosModal(ByVal cadastroID As Integer, ByVal tipoCadastro As String)
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim comando As SqlCommand
        Debug.WriteLine("" & cadastroID)
        If tipoCadastro = "EMBARQUE" Then
            comando = New SqlCommand("SELECT * FROM tb_Cadastro WHERE CadastroID = @ID", conexao)
        ElseIf tipoCadastro = "RECEBIMENTO" Then
            comando = New SqlCommand("SELECT *  FROM tb_Cadastro WHERE CadastroID = @ID", conexao)
        Else
            Debug.WriteLine("não achou cadastro")
            Exit Sub ' Sai se o tipo de cadastro não for reconhecido
        End If
        comando.Parameters.AddWithValue("@ID", cadastroID)
        Try
            If Trim(tipoCadastro) = "EMBARQUE" Then
                conexao.Open()
                Dim leitor As SqlDataReader = comando.ExecuteReader()
                If leitor.Read() Then
                    ' Armazena os dados na Session
                    Dim valorColuna1 As String = leitor(1).ToString()
                    If Not leitor.IsDBNull(1) Then valorColuna1 = leitor(1).ToString()
                    Dim valorColuna2 As String = leitor(2).ToString()
                    Dim valorColuna3 As String = leitor(3).ToString()
                    Dim valorColuna4 As String = leitor(4).ToString()
                    Dim valorColuna5 As String = leitor(5).ToString()
                    Dim valorColuna6 As String = leitor(6).ToString()
                    Dim valorColuna7 As String = leitor(7).ToString()
                    Dim valorColuna8 As String = leitor(8).ToString()
                    Dim valorColuna9 As String = leitor(9).ToString()
                    Dim valorColuna10 As String = leitor(10).ToString()
                    Dim valorColuna11 As String = leitor(11).ToString()
                    Dim valorColuna12 As String = leitor(12).ToString()
                    Dim valorColuna13 As String = leitor(13).ToString()
                    Dim valorColuna14 As String = leitor(14).ToString()
                    Dim valorColuna15 As String = leitor(15).ToString()
                    Dim valorColuna16 As String = leitor(16).ToString()
                    Dim valorColuna17 As String = leitor(17).ToString()
                    Dim valorColuna24 As String = leitor(24).ToString()
                    Dim valorCodigo As Integer = leitor("CodigoCliente").ToString()
                    Dim vTempo As String = leitor("TempoPadrao").ToString()


                    txtNotaFiscal.Text = valorColuna1
                    txtNotaFiscal.Enabled = False

                    txtCliente.Text = valorColuna2
                    txtCliente.Enabled = False

                    txtTempo.Text = vTempo
                    txtTempo.Enabled = False

                    txtCidade.Text = valorColuna3
                    txtCidade.Enabled = False

                    txtUF.Text = valorColuna4
                    txtUF.Enabled = False

                    txtTransportadora.Text = valorColuna5
                    txtTransportadora.Enabled = False

                    ddlFrete.Text = valorColuna6
                    ddlFrete.Enabled = False

                    txtMotorista.Text = valorColuna7
                    txtMotorista.Enabled = False

                    txtRG.Text = valorColuna8
                    txtRG.Enabled = False

                    txtPlaca.Text = valorColuna9
                    txtPlaca.Enabled = False

                    txtMaterial.Text = valorColuna10
                    txtMaterial.Enabled = False

                    txtVolumes.Text = valorColuna11
                    txtVolumes.Enabled = False

                    txtPaletasbobinas.Text = valorColuna12
                    txtPaletasbobinas.Enabled = False

                    txtPeso.Text = valorColuna13
                    txtPeso.Enabled = False

                    ddlVeiculo.Text = valorColuna14
                    ddlVeiculo.Enabled = False

                    txtCarga.Text = valorColuna15
                    txtCarga.Enabled = False

                    Dim dataBanco As String = valorColuna16
                    Dim dataConvertida As DateTime
                    If DateTime.TryParse(dataBanco, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        txtData.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        txtData.Enabled = False
                    Else
                        Debug.WriteLine("data: errada")
                    End If

                    Dim datajanela As String = valorColuna24
                    If DateTime.TryParse(datajanela, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        dataJanelaEmbarque.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        dataJanelaEmbarque.Enabled = False
                    Else
                        Debug.WriteLine("data: errada")
                    End If

                    txtObservacao.Text = valorColuna17
                    txtObservacao.Enabled = False

                    btnCadastrar.Enabled = False

                    txtCodigo.Text = valorCodigo
                    txtCodigo.Enabled = False
                End If

            ElseIf Trim(tipoCadastro) = "RECEBIMENTO" Then
                conexao.Open()
                Dim leitor As SqlDataReader = comando.ExecuteReader()
                If leitor.Read() Then
                    Dim valorcoluna1 As String = leitor(1).ToString()
                    Dim valorcoluna2 As String = leitor("FornecedorCliente").ToString()
                    Dim valorcoluna3 As String = leitor("Cidade").ToString()
                    Dim valorcoluna4 As String = leitor(4).ToString()
                    Dim valorcoluna5 As String = leitor(5).ToString()
                    Dim valorcoluna6 As String = leitor(6).ToString()
                    Dim valorcoluna7 As String = leitor(7).ToString()
                    Dim valorcoluna8 As String = leitor(8).ToString()
                    Dim valorcoluna9 As String = leitor(9).ToString()
                    Dim valorcoluna10 As String = leitor(10).ToString()
                    Dim valorcoluna11 As String = leitor(11).ToString() 'volumes
                    Dim valorcoluna16 As String = leitor(16).ToString()
                    Dim valorcoluna13 As String = leitor(25).ToString() 'obs 17
                    Dim valorcoluna24 As String = leitor(24).ToString()

                    Debug.WriteLine("Campos disponíveis: " & valorcoluna1)

                    txtNotaFiscal22.Text = valorcoluna1
                    txtNotaFiscal22.Enabled = False

                    txtFornecedor22.Text = valorcoluna2
                    txtFornecedor22.Enabled = False

                    txtCidade22.Text = valorcoluna3
                    txtCidade22.Enabled = False

                    txtUF22.Text = valorcoluna4
                    txtUF22.Enabled = False

                    txtTransportadora22.Text = valorcoluna5
                    txtTransportadora22.Enabled = False

                    ddlFrete22.Text = valorcoluna6
                    ddlFrete22.Enabled = False

                    txtMotorista22.Text = valorcoluna7
                    txtMotorista22.Enabled = False

                    txtRG22.Text = valorcoluna8
                    txtRG22.Enabled = False

                    txtPlaca22.Text = valorcoluna9
                    txtPlaca22.Enabled = False

                    txtMaterial22.Text = valorcoluna10
                    txtMaterial22.Enabled = False

                    txtVolumes22.Text = valorcoluna11
                    txtVolumes22.Enabled = False

                    Dim dataBanco As String = valorcoluna16
                    Dim dataConvertida As DateTime
                    If DateTime.TryParse(dataBanco, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        txtData22.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        txtData22.Enabled = False
                    Else
                        Debug.WriteLine("data: errada")
                    End If

                    Dim datajanela2 As String = valorcoluna24
                    If DateTime.TryParse(datajanela2, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        dataJanelaRecebimento2.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        dataJanelaRecebimento2.Enabled = False
                    Else
                        Debug.WriteLine("data: errada")
                    End If

                    txtOBS22.Text = valorcoluna13
                    txtOBS22.Enabled = False

                    btn2.Enabled = False
                    Debug.WriteLine("Query SQL: " & comando.CommandText)
                    Debug.WriteLine("Parâmetro @ID: " & cadastroID)

                End If
            Else
                Debug.WriteLine("Nenhum registro encontrado para o ID informado.")
                Debug.WriteLine(" deu erro em recebimento") ' Log para testar
            End If
        Catch ex As Exception
            Debug.WriteLine("Erro: " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erroDB", "alert('Erro ao carregar os dados!');", True)
        Finally
            conexao.Close()
        End Try
    End Sub

End Class
