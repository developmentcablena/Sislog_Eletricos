Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics
Imports System.Net.Mail

Partial Public Class Recusados
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarDados()
        End If
    End Sub

    Private Sub CarregarDados()
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
                Dim query As String = "SELECT CadastroID, TipoCadastro, FornecedorCliente, Transportadora, Placa, " &
                                  "CASE WHEN Status = 3 THEN 'Recusado' ELSE '' END AS Status " &
                                  "FROM tb_Cadastro WHERE Status = 3"
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

    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        CarregarDados()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalRecusados();", True)
    End Sub

    Protected Sub btn_enviar_embarque(sender As Object, e As EventArgs)
        Dim connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim vNotaFiscal As String = txtNotaFiscal.Text
        Dim vFornecedor As String = txtCliente.Text
        Dim vCidade As String = txtCidade.Text
        Dim vUF As String = txtUF.Text
        Dim vTransportadora As String = txtTransportadora.Text
        Dim vFrete As String = ddlFrete.Text
        Dim vMotorista As String = txtMotorista.Text
        Dim vRG As String = txtRG.Text
        Dim vPlaca As String = txtPlaca.Text
        Dim vMaterial As String = txtMaterial.Text
        Dim vVolumes As String = txtVolumes.Text
        Dim vData As DateTime = DateTime.Parse(txtData.Text)
        Dim vObs As String = txtObservacao.Text
        Dim vPaletaBonina As String = txtPaletasbobinas.Text
        Dim vPeso As String = txtPeso.Text
        Dim vVeiculo As String = ddlVeiculo.Text
        Dim vCarga As String = txtCarga.Text
        Dim id As String = Session("ValorID")

        If String.IsNullOrEmpty(Trim(txtCliente.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir o Fornecedor'); abrirModalEmbarque2();", True)
            txtCliente.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtCidade.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a Cidade'); abrirModalEmbarque2();", True)
            txtCidade.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtUF.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor colocar o UF'); abrirModalEmbarque2();", True)
            txtUF.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtTransportadora.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a transportadora'); abrirModalEmbarque2();", True)
            txtTransportadora.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(ddlFrete.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o frete'); abrirModalEmbarque2();", True)
            ddlFrete.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMotorista.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do motorista'); abrirModalEmbarque2();", True)
            txtMotorista.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtRG.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o RG'); abrirModalEmbarque2();", True)
            txtRG.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPlaca.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir a placa'); abrirModalEmbarque2();", True)
            txtPlaca.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMaterial.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do material'); abrirModalEmbarque2();", True)
            txtMaterial.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtVolumes.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar a quantidade de volumes'); abrirModalEmbarque2();", True)
            txtVolumes.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPaletasbobinas.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor selecionar Paletas/Bobinas'); abrirModalEmbarque2();", True)
            Me.txtPaletasbobinas.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPeso.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o peso(Kg)'); abrirModalEmbarque2();", True)
            Me.txtPeso.Focus()
            Exit Sub
        End If
        If Trim(ddlVeiculo.Text) = "Selecione" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o tipo de veiculo '); abrirModalEmbarque2();", True)
            Me.ddlVeiculo.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtCarga.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar a capacidade da carga! '); abrirModalEmbarque2();", True)
            Me.txtCarga.Focus()
            Exit Sub
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim sql As String = "UPDATE tb_Cadastro SET NotaFiscal = @notafiscal, FornecedorCliente = @fornecedorCliente, Cidade = @cidade, UF = @uf, Transportadora = @transportadora, Frete = @frete, Motorista = @motorista, RG = @rg, Placa = @placa, Material = @material, Volumes = @volumes, Observacao = @obs, PaletesBobina = @paletesBobina, Peso = @peso, TipoVeiculo = @veiculo, CapacidadeCarga = @carga, Status = @status WHERE CadastroID = @id"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@notafiscal", vNotaFiscal)
                    cmd.Parameters.AddWithValue("@fornecedorCliente", vFornecedor)
                    cmd.Parameters.AddWithValue("@cidade", vCidade)
                    cmd.Parameters.AddWithValue("@uf", vUF)
                    cmd.Parameters.AddWithValue("@transportadora", vTransportadora)
                    cmd.Parameters.AddWithValue("@frete", vFrete)
                    cmd.Parameters.AddWithValue("@motorista", vMotorista)
                    cmd.Parameters.AddWithValue("@rg", vRG)
                    cmd.Parameters.AddWithValue("@placa", vPlaca)
                    cmd.Parameters.AddWithValue("@material", vMaterial)
                    cmd.Parameters.AddWithValue("@volumes", vVolumes)
                    cmd.Parameters.AddWithValue("@obs", vObs)
                    cmd.Parameters.AddWithValue("@paletesBobina", vPaletaBonina)
                    cmd.Parameters.AddWithValue("@peso", vPeso)
                    cmd.Parameters.AddWithValue("@veiculo", vVeiculo)
                    cmd.Parameters.AddWithValue("@carga", vCarga)
                    cmd.Parameters.AddWithValue("@status", 1)
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.ExecuteNonQuery()
                End Using
                FnEnviarEmail(id)
                CarregarDados()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Dados atualizados com sucesso.'); abrirModalRecusados();", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('ERRO ao atualizar os dados!!'); abrirModalRecusados();", True)
            End Try
        End Using
    End Sub

    Protected Sub btn_enviar_recebimento(sender As Object, e As EventArgs)
        Dim connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim vNotaFiscal As String = txtNotaFiscal2.Text
        Dim vFornecedor As String = txtFornecedor2.Text
        Dim vCidade As String = txtCidade2.Text
        Dim vUF As String = txtUF2.Text
        Dim vTransportadora As String = txtTransportadora2.Text
        Dim vFrete As String = ddlFrete2.Text
        Dim vMotorista As String = txtMotorista2.Text
        Dim vRG As String = txtRG2.Text
        Dim vPlaca As String = txtPlaca2.Text
        Dim vMaterial As String = txtMaterial2.Text
        Dim vVolumes As String = txtVolumes2.Text
        Dim vData As DateTime = DateTime.Parse(txtData2.Text)
        Dim vObs As String = txtOBS2.Text
        Dim id As String = Session("ValorID")

        If String.IsNullOrEmpty(Trim(txtFornecedor2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir o Fornecedor'); abrirModal2();", True)
            txtFornecedor2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtCidade2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a Cidade'); abrirModal2();", True)
            txtCidade2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtUF2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor colocar o UF'); abrirModal2();", True)
            txtUF2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtTransportadora2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a transportadora'); abrirModal2();", True)
            txtTransportadora2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(ddlFrete2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o frete'); abrirModal2();", True)
            ddlFrete2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMotorista2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do motorista'); abrirModal2();", True)
            txtMotorista2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtRG2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o RG'); abrirModal2();", True)
            txtRG2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPlaca2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir a placa'); abrirModal2();", True)
            txtPlaca2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMaterial2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do material'); abrirModal2();", True)
            txtMaterial2.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtVolumes2.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar a quantidade de volumes'); abrirModal2();", True)
            txtVolumes2.Focus()
            Exit Sub
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim sql As String = "UPDATE tb_Cadastro SET NotaFiscal = @notafiscal, FornecedorCliente = @fornecedorCliente, Cidade = @cidade, UF = @uf, Transportadora = @transportadora, Frete = @frete, Motorista = @motorista, RG = @rg, Placa = @placa, Material = @material, Volumes = @volumes, Observacao = @obs, Status = @status WHERE CadastroID = @id"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@notafiscal", vNotaFiscal)
                    cmd.Parameters.AddWithValue("@fornecedorCliente", vFornecedor)
                    cmd.Parameters.AddWithValue("@cidade", vCidade)
                    cmd.Parameters.AddWithValue("@uf", vUF)
                    cmd.Parameters.AddWithValue("@transportadora", vTransportadora)
                    cmd.Parameters.AddWithValue("@frete", vFrete)
                    cmd.Parameters.AddWithValue("@motorista", vMotorista)
                    cmd.Parameters.AddWithValue("@rg", vRG)
                    cmd.Parameters.AddWithValue("@placa", vPlaca)
                    cmd.Parameters.AddWithValue("@material", vMaterial)
                    cmd.Parameters.AddWithValue("@volumes", vVolumes)
                    cmd.Parameters.AddWithValue("@obs", vObs)
                    cmd.Parameters.AddWithValue("@status", 1)
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.ExecuteNonQuery()
                End Using
                FnEnviarEmail(id)
                CarregarDados()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Dados atualizados com sucesso.'); abrirModalRecusados();", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('ERRO ao atualizar os dados!!'); abrirModalRecusados();", True)
            End Try
        End Using

    End Sub

    Protected Sub gvCadastros_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim commandName As String = e.CommandName
        Dim cadastroID As Integer = Convert.ToInt32(e.CommandArgument)
        Select Case commandName

            Case "Editar"
                Dim btn As Button = CType(e.CommandSource, Button)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim tipoCadastro As String = row.Cells(1).Text.Trim() ' Ajuste o índice conforme necessário
                EditarCadastro(cadastroID, tipoCadastro)
                HttpContext.Current.Session("ValorID") = cadastroID

            Case "Visualizar"
                Dim btn As Button = CType(e.CommandSource, Button)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim tipoCadastro As String = row.Cells(1).Text.Trim() ' Ajuste o índice conforme necessário
                VisualizarMotivo(cadastroID, tipoCadastro)


        End Select
    End Sub

    Private Sub VisualizarMotivo(ByVal cadastroID As Integer, ByVal tipoCadastro As String)
        CarregarDadosMotivo(cadastroID, tipoCadastro)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalRecusados(); abrirModalMotivo2();", True)
    End Sub

    Private Sub CarregarDadosMotivo(ByVal cadastroID As Integer, ByVal tipoCadastro As String)
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim comando As SqlCommand

        comando = New SqlCommand("SELECT * FROM tb_MotivoRecusado WHERE CadastroID = @ID", conexao)

        comando.Parameters.AddWithValue("@ID", cadastroID)
        Try
            conexao.Open()
            Dim leitor As SqlDataReader = comando.ExecuteReader()
            If leitor.Read() Then
                Dim motivo As String = leitor(1).ToString()
                Debug.WriteLine("motivo leitor " & motivo)
                txtMotivo2.Text = motivo
                txtMotivo2.Enabled = False
                btnEnviarMotivo2.Enabled = False
            End If

            btnEnviarMotivo2.Enabled = False

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erroDB", "alert('Erro ao carregar os dados!');", True)
        Finally
            conexao.Close()
        End Try
    End Sub

    Private Sub EditarCadastro(ByVal cadastroID As Integer, ByVal tipoCadastro As String)
        CarregarDadosModal(cadastroID, tipoCadastro)

        If tipoCadastro = "EMBARQUE" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirEmbarque", "abrirModalRecusados(); abrirModalEmbarque2();", True)

        ElseIf tipoCadastro = "RECEBIMENTO" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "AbrirRecebimento", "abrirModalRecusados(); abrirModal2222();", True)
        End If
    End Sub

    Private Sub CarregarDadosModal(ByVal cadastroID As Integer, ByVal tipoCadastro As String)
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim comando As SqlCommand

        If tipoCadastro = "EMBARQUE" Then
            comando = New SqlCommand("SELECT * FROM tb_Cadastro WHERE CadastroID = @ID", conexao)
        ElseIf tipoCadastro = "RECEBIMENTO" Then
            comando = New SqlCommand("SELECT * FROM tb_Cadastro WHERE CadastroID = @ID", conexao)
        Else
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
                End If

            ElseIf Trim(tipoCadastro) = "RECEBIMENTO" Then
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
                    Dim valorColuna13 As String = leitor(17).ToString()
                    Dim valorColuna24 As String = leitor(24).ToString()

                    txtNotaFiscal2.Text = valorColuna1
                    txtNotaFiscal2.Enabled = True

                    txtFornecedor2.Text = valorColuna2
                    txtFornecedor2.Enabled = True

                    txtCidade2.Text = valorColuna3
                    txtCidade2.Enabled = True

                    txtUF2.Text = valorColuna4
                    txtUF2.Enabled = True

                    txtTransportadora2.Text = valorColuna5
                    txtTransportadora2.Enabled = True

                    ddlFrete2.Text = valorColuna6
                    ddlFrete2.Enabled = True

                    txtMotorista2.Text = valorColuna7
                    txtMotorista2.Enabled = True

                    txtRG2.Text = valorColuna8
                    txtRG2.Enabled = True

                    txtPlaca2.Text = valorColuna9
                    txtPlaca2.Enabled = True

                    txtMaterial2.Text = valorColuna10
                    txtMaterial2.Enabled = True

                    txtVolumes2.Text = valorColuna11
                    txtVolumes2.Enabled = True

                    Dim dataBanco As String = valorColuna16
                    Dim dataConvertida As DateTime
                    If DateTime.TryParse(dataBanco, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        txtData2.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        txtData2.Enabled = False
                    Else
                        Debug.WriteLine("data: errada")
                    End If

                    Dim datajanela2 As String = valorColuna24
                    If DateTime.TryParse(datajanela2, dataConvertida) Then
                        ' Se a conversão for bem-sucedida, formate a data e atribua ao txtData.Text
                        dataJanelaRecebimento.Text = dataConvertida.ToString("yyyy-MM-ddTHH:mm")
                        dataJanelaRecebimento.Enabled = False
                    Else
                        Debug.WriteLine("data: errada")
                    End If

                    txtOBS2.Text = valorColuna13
                    txtOBS2.Enabled = True

                    btn2.Enabled = True
                End If
            Else

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erroDB", "alert('Erro ao carregar os dados!');", True)
        Finally
            conexao.Close()
        End Try
    End Sub


    Private Function FnEnviarEmail(ByVal vID As Long) As Boolean
        Try
            Dim mail As New MailMessage()

            mail.From = New MailAddress(FnContaSMTP())
            mail.To.Add("mpaixao@cablena.com.br")
            mail.Subject = "SisLOG Elétricos - CADASTRO ATUALIZADO " & Format(vID, "0000")
            Dim corpoEmail As String = suRelatorio(vID)
            mail.Body = corpoEmail
            mail.IsBodyHtml = False

            Dim smtp As New SmtpClient("email-ssl.com.br")
            smtp.Port = 587
            smtp.Credentials = New System.Net.NetworkCredential(FnContaSMTP(), FnSenhaSMTP())
            smtp.EnableSsl = True

            smtp.Send(mail)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail2", $"alert('Email enviado com sucesso!'); fecharModalEmbarque();", True)
            Return True
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Erro", $"alert('Erro ao enviar email: {ex.Message}'); abrirModalEmbarque();", True)
            Return False
        End Try
    End Function

    Private Function suRelatorio(ByVal vID As Long)
        Dim str As String = New String("="c, 120) & vbCrLf & vbCrLf
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim query As String = "SELECT * FROM tb_Cadastro WHERE CadastroID = @CadastroID"
        Dim cmd As New SqlCommand(query, con)

        cmd.Parameters.AddWithValue("@CadastroID", vID)
        Dim strRelatorio As String = ""
        Try
            con.Open()

            Dim rs As SqlDataReader = cmd.ExecuteReader()

            If rs.Read() Then

                strRelatorio &= str
                strRelatorio &= "Nº do Cadastro: " & Format(rs!CadastroID, "0000") & vbCrLf & vbCrLf
                strRelatorio &= "Tipo Cadastro: " & rs!TipoCadastro & vbCrLf & vbCrLf
                strRelatorio &= "Cliente: " & rs!FornecedorCliente & vbCrLf & vbCrLf
                strRelatorio &= "Transportadora: " & rs!Transportadora & vbCrLf & vbCrLf
                strRelatorio &= "Placa: " & rs!Placa & vbCrLf & vbCrLf
                strRelatorio &= "Status: Pendente " & vbCrLf & vbCrLf
                strRelatorio &= "Data Cadastro: " & rs!DataCadastro & vbCrLf & vbCrLf
                strRelatorio &= str
            End If
            rs.Close()

        Catch ex As Exception
            strRelatorio = "Erro ao gerar o relatório: " & ex.Message
        Finally
            con.Close()

        End Try
        Return strRelatorio
    End Function

    Private Function FnContaSMTP() As String
        Dim strSQL As String = "SELECT * FROM tb_Email WHERE ID = 1"
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
            Using cmd As New SqlCommand(strSQL, cn)
                Try
                    cn.Open()
                    Using rs As SqlDataReader = cmd.ExecuteReader()
                        If rs.HasRows Then
                            rs.Read()
                            Return rs("Valor").ToString().Trim()
                        Else
                            Return ""
                        End If
                    End Using
                Catch ex As Exception
                    Return ""
                Finally
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End Try
            End Using
        End Using
    End Function

    Private Function FnSenhaSMTP() As String
        Dim strSQL As String = "SELECT * FROM tb_Email WHERE ID = 2"
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
            Using cmd As New SqlCommand(strSQL, cn)
                Try
                    cn.Open()
                    Using rs As SqlDataReader = cmd.ExecuteReader()
                        If rs.HasRows Then
                            rs.Read()
                            Return rs("Valor").ToString().Trim()
                        Else
                            Return ""
                        End If
                    End Using
                Catch ex As Exception
                    Return ""
                Finally
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End Try
            End Using
        End Using
    End Function


End Class