Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class Historico
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarNotasFiscais()
        End If

    End Sub

    Private Sub CarregarNotasFiscais()
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
                Dim query As String = "SELECT CadastroID, TipoCadastro, FornecedorCliente, Transportadora, Placa, " &
                          "FORMAT(HorarioChegada, 'HH:mm ') AS HorarioChegada, " &
                          "FORMAT(HorarioEntrada, 'HH:mm') AS HorarioEntrada, " &
                          "FORMAT(HorarioSaida, 'HH:mm ') AS HorarioSaida, " &
                          "CASE " &
                          "   WHEN Status = 4 THEN 'Concluido' " &
                          "   WHEN Status = 2 THEN 'Pendente' " &
                          "   ELSE '' " &
                          "END AS Status " &
                          "FROM tb_Cadastro " &
                          "WHERE Status IN (2, 4)"

                Dim cmd As New SqlCommand(query, conn)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()

                conn.Open()
                da.Fill(dt)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    gvCadastros.DataSource = dt
                    gvCadastros.DataBind()
                Else
                    gvCadastros.DataSource = Nothing
                    gvCadastros.DataBind()
                End If
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", $"alert('Erro ao carregar notas fiscais: {ex.Message}')", True)
        End Try
        'Response.Redirect(Request.RawUrl)
    End Sub

    Protected Sub gvCadastros_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim commandName As String = e.CommandName
        Dim cadastroID As Integer = Convert.ToInt32(e.CommandArgument)
        Select Case commandName
            Case "AbrirModal"
                Dim btn As Button = CType(e.CommandSource, Button)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim tipoCadastro As String = row.Cells(1).Text.Trim() ' Ajuste o índice conforme necessário

                AbrirModal(cadastroID, tipoCadastro)
        End Select
    End Sub

    Private Sub AbrirModal(ByVal vID As Integer, ByVal vTipo As String)
        CarregarDadosModal(vID, vTipo)

        If vTipo = "Embarque" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail2", "abrirModalHistorico(); abrirModalEmbarque3();", True)

        ElseIf vTipo = "Recebimento" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail2", "abrirModalHistorico(); abrirModal3();", True)
        End If
    End Sub

    Private Sub CarregarDadosModal(ByVal vID As Integer, ByVal vTipo As String)
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim comando As SqlCommand

        If vTipo = "Embarque" Then
            comando = New SqlCommand("SELECT * FROM tb_Cadastro WHERE CadastroID = @ID", conexao)
        ElseIf vTipo = "Recebimento" Then
            comando = New SqlCommand("SELECT * FROM tb_Cadastro WHERE CadastroID = @ID", conexao)
        Else
            Exit Sub ' Sai se o tipo de cadastro não for reconhecido
        End If
        comando.Parameters.AddWithValue("@ID", vID)
        Try
            If Trim(vTipo) = "Embarque" Then
                conexao.Open()
                Dim leitor As SqlDataReader = comando.ExecuteReader()
                If leitor.Read() Then
                    ' Armazena os dados na Session
                    Dim valorColuna1 As String = leitor(1).ToString()
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

                    txtNotaFiscal.Text = valorColuna1
                    txtNotaFiscal.Enabled = False

                    txtCliente.Text = valorColuna2
                    txtCliente.Enabled = False

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
                        ' Debug.WriteLine("data: errada")
                    End If

                    txtObservacao.Text = valorColuna17
                    txtObservacao.Enabled = False

                    btnCadastrar.Enabled = False
                End If

            ElseIf Trim(vTipo) = "Recebimento" Then
                conexao.Open()
                Dim leitor As SqlDataReader = comando.ExecuteReader()
                If leitor.Read() Then
                    ' Armazena os dados na Session
                    Dim valorColuna1 As String = leitor(1).ToString()
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
                    Dim valorColuna16 As String = leitor(16).ToString()
                    Dim valorColuna13 As String = leitor(13).ToString()

                    txtNotaFiscal2.Text = valorColuna1
                    txtNotaFiscal2.Enabled = False

                    txtFornecedor2.Text = valorColuna2
                    txtFornecedor2.Enabled = False

                    txtCidade2.Text = valorColuna3
                    txtCidade2.Enabled = False

                    txtUF2.Text = valorColuna4
                    txtUF2.Enabled = False

                    txtTransportadora2.Text = valorColuna5
                    txtTransportadora2.Enabled = False

                    ddlFrete2.Text = valorColuna6
                    ddlFrete2.Enabled = False

                    txtMotorista2.Text = valorColuna7
                    txtMotorista2.Enabled = False

                    txtRG2.Text = valorColuna8
                    txtRG2.Enabled = False

                    txtPlaca2.Text = valorColuna9
                    txtPlaca2.Enabled = False

                    txtMaterial2.Text = valorColuna10
                    txtMaterial2.Enabled = False

                    txtVolumes2.Text = valorColuna11
                    txtVolumes2.Enabled = False

                    Dim dataBanco As String = valorColuna16
                    Dim dataConvertida As DateTime
                    If DateTime.TryParse(dataBanco, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        txtData2.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        txtData2.Enabled = False
                    Else
                        ' Debug.WriteLine("data: errada")
                    End If

                    txtOBS2.Text = valorColuna13
                    txtOBS2.Enabled = False

                    btn2.Enabled = False
                End If
            Else
                'Debug.WriteLine(" deu erro em recebimento") ' Log para testar
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erroDB", "alert('Erro ao carregar os dados!');", True)
        Finally
            conexao.Close()
        End Try
    End Sub

    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        CarregarNotasFiscais()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalHistorico();", True)
    End Sub

End Class
