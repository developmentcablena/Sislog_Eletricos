<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Autorizar.ascx.vb" Inherits="SislogEletricos.Autorizar" %>

<link href="Style/Autorizar.css" rel="stylesheet" type="text/css" />
<%@ Register Src="~/Recebimento.ascx" TagName="Recebimento" TagPrefix="uc" %>
<script src="https://unpkg.com/lucide@latest/dist/umd/lucide.min.js"></script>
<style>
    .modal2 {
        display: none;
        position: fixed;
        z-index: 9999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
    }

    .modal-content2 {
        background-color: white;
        margin: 6% auto;
        padding: 20px;
        width: 400px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    .h1__titulo-modal {
        background-color: #ff7f27;
        color: white;
        text-align: left;
        padding: 20px;
        margin: 0;
        width: 100%;
        font-size: 20px;
        font-weight: bold;
        border-radius: 6px 6px 0 0;
        position: relative;
        top: -40px;
        left: -20px;
    }

    #meuModal2 .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #meuModal2 .span__close:hover {
            font-size: 20px;
            color: black;
            background-color: transparent;
        }

    .modal__body {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 15px;
        padding: 0px;
    }

    .div__group {
        display: flex;
        flex-direction: column;
    }

        .div__group span {
            font-size: 14px;
            margin-bottom: 4px;
        }

    .textBox {
        padding: 6px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 14px;
    }

    #meuModal2 .btn-cadastrar {
        grid-column: span 2;
        padding: 10px;
        background-color: #ff7f27;
        color: white;
        font-size: 16px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        text-align: center;
        margin-top: 23%;
    }

    .textBox__obs {
        display: flex;
        position: absolute;
        resize: none;
        overflow: auto;
        width: 379px;
        height: 86px;
        border-radius: 4px;
        border: 1px solid #ccc;
        margin-top: 4px;
    }

    /*Modal Embarque*/
    .css__modal-Embarque2 {
        display: none;
        position: fixed;
        z-index: 99999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
    }

    .modal__content-Embarque2 {
        background-color: white;
        margin: 6% auto;
        padding: 20px;
        width: 400px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    .h1__titulo-modal {
        background-color: #ff7f27;
        color: white;
        text-align: left;
        padding: 20px;
        margin: 0;
        width: 100%;
        font-size: 20px;
        font-weight: bold;
        border-radius: 6px 6px 0 0;
        position: relative;
        top: -40px;
        left: -20px;
    }

    #modalEmbarque .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #modalEmbarque .span__close:hover {
            font-size: 20px;
            color: black;
            background-color: transparent;
        }

    .modal__body {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 15px;
        padding: 0px;
    }

    .div__group {
        display: flex;
        flex-direction: column;
    }

        .div__group span {
            font-size: 14px;
            margin-bottom: 4px;
        }

    .textBox {
        padding: 6px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 14px;
    }

    #modalEmbarque2 .btn-cadastrar {
        grid-column: span 2;
        padding: 10px;
        background-color: #ff7f27;
        color: white;
        font-size: 16px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        text-align: center;
        margin-top: 23%;
    }


    .textBox__obs {
        display: flex;
        position: absolute;
        resize: none;
        overflow: auto;
        width: 379px;
        height: 86px;
        border-radius: 4px;
        border: 1px solid #ccc;
        margin-top: 4px;
        font-family: Arial, sans-serif;
    }
</style>

<asp:UpdatePanel ID="upModal1" runat="server">
    <ContentTemplate>
        <div id="modalAutorizacao" class="css__modal-Autorizacao">
            <div class="modal__content-Autorizacao">
                <h1 class="h1__titulo-modal">Autorização</h1>
                <asp:Button CssClass="btnAtualizar" Text="🔄" runat="server" OnClick="Unnamed_Click" />
                <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalAutorizacao();">x</span>
                <div>
                    <div class="rolagem">
                        <div>
                            <asp:GridView runat="server" ID="gvCadastros" AutoGenerateColumns="false" GridLines="None" CssClass="table" OnRowCommand="gvCadastros_RowCommand">
                                <HeaderStyle CssClass="Header-fixo" />
                                <Columns>
                                    <asp:BoundField DataField="CadastroID" HeaderText="Nº Cadastro" />
                                    <asp:BoundField DataField="TipoCadastro" HeaderText="Tipo cadastro" />
                                    <asp:BoundField DataField="FornecedorCliente" HeaderText="Fornecedor/Cliente" />
                                    <asp:BoundField DataField="Transportadora" HeaderText="Fornecedor/Cliente" />
                                    <asp:BoundField DataField="Placa" HeaderText="Placa" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Button ID="btnAutorizar" runat="server" CssClass="btn_ok" CommandName="Autorizar"
                                                CommandArgument='<%# Eval("CadastroID")%>' Text="✔" />
                                            <asp:Button ID="btnRejeitar" runat="server" CssClass="btn_rejeitar" CommandName="Rejeitar"
                                                CommandArgument='<%# Eval("CadastroID")%>' Text="X" />
                                            <asp:Button ID="btnExcluir" runat="server" CssClass="btn_excluir" CommandName="excluir"
                                                CommandArgument='<%# Eval("CadastroID")%>' Text="🗑" />
                                            <asp:Button ID="btnAbrir" runat="server" CssClass="css_abir-dados-modal" Text="📂" CommandName="abrirModal"
                                                CommandArgument='<%# Eval("CadastroID")%>' />
                                            <uc:Recebimento runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Estrotura para abrir o modal Embarque  -->

        <div id="modalEmbarque2" class="css__modal-Embarque2">
            <div class="modal__content-Embarque2">
                <h1 class="h1__titulo-modal">Embarque</h1>
                <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalEmbarque2();">x</span>
                <div class="modal__body">
                    <div class="div__group">
                        <asp:Label ID="Label1" runat="server" Text="Nota fiscal"></asp:Label>
                        <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="textBox" placeholder="Nota fiscal" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label2" runat="server" Text="Cliente"></asp:Label>
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="textBox" placeholder="Cliente" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label3" runat="server" Text="Cidade"></asp:Label>
                        <asp:TextBox ID="txtCidade" runat="server" CssClass="textBox" placeholder="Cidade" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="UF"></asp:Label>
                        <asp:TextBox ID="txtUF" runat="server" CssClass="textBox" placeholder="UF" MaxLength="2" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Transportadora"></asp:Label>
                        <asp:TextBox ID="txtTransportadora" runat="server" CssClass="textBox" placeholder="Transportadora" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="CIF / FOB"></asp:Label>
                        <asp:DropDownList ID="ddlFrete" runat="server" CssClass="textBox">
                            <asp:ListItem Text="Selecione" Value="" />
                            <asp:ListItem Text="CIF" />
                            <asp:ListItem Text="FOB" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Motorista"></asp:Label>
                        <asp:TextBox runat="server" ID="txtMotorista" CssClass="textBox" placeholder="Motorista" MaxLength="100" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="RG/CPF" runat="server" />
                        <asp:TextBox runat="server" ID="txtRG" CssClass="textBox" placeholder="RG/CPF" TextMode="Number" MaxLength="2"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Placa" runat="server" />
                        <asp:TextBox runat="server" ID="txtPlaca" CssClass="textBox" placeholder="Placa" MaxLength="7" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Material" runat="server" />
                        <asp:TextBox runat="server" ID="txtMaterial" CssClass="textBox" placeholder="Material" MaxLength="50" />
                    </div>
                    <div>
                        <asp:Label Text="Volumes" runat="server" />
                        <asp:TextBox runat="server" ID="txtVolumes" CssClass="textBox" placeholder="Volumes" TextMode="Number" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Paletas/Bobinas" runat="server" />
                        <asp:DropDownList runat="server" ID="txtPaletasbobinas" CssClass="textBox">
                            <asp:ListItem Text="Selecione" Value="" />
                            <asp:ListItem Text="Paletas" />
                            <asp:ListItem Text="Bobinas" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Peso(kg)" runat="server" />
                        <asp:TextBox runat="server" ID="txtPeso" CssClass="textBox" placeholder="Peso(kg)" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Tipo de veiculo" runat="server" />
                        <asp:DropDownList runat="server" CssClass="textBox" ID="ddlVeiculo">
                            <asp:ListItem Text="Selecione" />
                            <asp:ListItem Text="Fiorino 3/4" />
                            <asp:ListItem Text="Carreta" />
                            <asp:ListItem Text="Truck" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Capacidade de carga" runat="server" />
                        <asp:TextBox runat="server" ID="txtCarga" CssClass="textBox" placeholder="Capacidade de carga" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Data" runat="server" />
                        <asp:TextBox ID="txtData" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
                    </div>
                    <div>
                        <asp:Label Text="Observação" runat="server" />
                        <asp:TextBox runat="server" ID="txtObservacao" CssClass="textBox__obs" TextMode="MultiLine" placeholder="Observação" />
                    </div>
                    <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn-cadastrar" />
                </div>
            </div>
        </div>

        <!-- Estrotura para abrir o modal Recebimento -->

        <div id="meuModal2" class="modal2">
            <div class="modal-content2">
                <h1 class="h1__titulo-modal">Recebimento</h1>
                <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModal2();">x</span>
                <div class="modal__body">
                    <div class="div__group">
                        <asp:Label ID="Label4" runat="server" Text="Nota fiscal"></asp:Label>
                        <asp:TextBox ID="txtNotaFiscal2" runat="server" CssClass="textBox" placeholder="Nota fiscal" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label5" runat="server" Text="Fornecedor"></asp:Label>
                        <asp:TextBox ID="txtFornecedor2" runat="server" CssClass="textBox" placeholder="Fornecedor" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label6" runat="server" Text="Cidade"></asp:Label>
                        <asp:TextBox ID="txtCidade2" runat="server" CssClass="textBox" placeholder="Cidade" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="UF"></asp:Label>
                        <asp:TextBox ID="txtUF2" runat="server" CssClass="textBox" placeholder="UF" MaxLength="2"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Transportadora"></asp:Label>
                        <asp:TextBox ID="txtTransportadora2" runat="server" CssClass="textBox" placeholder="Transportadora" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="CIF / FOB"></asp:Label>
                        <asp:DropDownList ID="ddlFrete2" runat="server" CssClass="textBox">
                            <asp:ListItem Text="Selecione" Value="" />
                            <asp:ListItem Text="CIF" />
                            <asp:ListItem Text="FOB" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Motorista"></asp:Label>
                        <asp:TextBox runat="server" ID="txtMotorista2" CssClass="textBox" placeholder="Motorista" MaxLength="50" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="RG" runat="server" />
                        <asp:TextBox runat="server" ID="txtRG2" CssClass="textBox" placeholder="RG" TextMode="Number" MaxLength="11" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Placa" runat="server" />
                        <asp:TextBox runat="server" ID="txtPlaca2" CssClass="textBox" placeholder="Placa" MaxLength="7" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Material" runat="server" />
                        <asp:TextBox runat="server" ID="txtMaterial2" CssClass="textBox" placeholder="Material" MaxLength="50" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Volumes" runat="server" />
                        <asp:TextBox runat="server" ID="txtVolumes2" CssClass="textBox" placeholder="Volumes" TextMode="Number" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Data" runat="server" />
                        <asp:TextBox ID="txtData2" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
                    </div>
                    <div>
                        <asp:Label Text="Observação" runat="server" />
                        <asp:TextBox runat="server" ID="txtOBS2" CssClass="textBox__obs" TextMode="MultiLine" placeholder="Observação" />
                    </div>
                    <asp:Button ID="btn2" runat="server" Text="Cadastrar" CssClass="btn-cadastrar" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function abrirModal2() {
        document.getElementById("meuModal2").style.display = "block";
    }

    function fecharModal2() {
        document.getElementById("meuModal2").style.display = "none";
    }
    function abrirModalEmbarque2() {
        document.getElementById("modalEmbarque2").style.display = "block";
    }
    function fecharModalEmbarque2() {
        document.getElementById("modalEmbarque2").style.display = "none";
    }

    function abrirModal() {
        document.getElementById("meuModal").style.display = "block";
    }
    function fehcarModal() {
        document; getElementById("meuModal").style.display = "none";
    }
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
    });
    function abrirModalAutorizacao() {
        document.getElementById("modalAutorizacao").style.display = "block";
    }

    function fecharModalAutorizacao() {
        document.getElementById("modalAutorizacao").style.display = "none";
    }
    lucide.createIcons(); // Inicializar os ícones do Lucide

</script>


