<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="~/CodigoCliente.ascx.vb" Inherits="SislogEletricos.CodigoCliente" %>
<link href="Style/CodigoCliente.css" rel="stylesheet" type="text/css" />

<style>
    .css__modal-CadastrarCliente {
        display: none;
        position: fixed;
        z-index: 9999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
    }

    #modalCadastrarCliente .modal__content-CadastrarCliente {
        background-color: white;
        margin: 16% auto;
        padding: 20px;
        width: 500px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    #modalCadastrarCliente .h1__titulo-modal {
        background-color: #ffffff;
        color: black;
        text-align: left;
        padding: 19px;
        margin: 0;
        width: 100%;
        font-size: 20px;
        font-weight: bold;
        border-radius: 6px 6px 0 0;
        position: relative;
        top: -40px;
        left: -20px;
        border: 1px solid #ddd;
    }

    #modalCadastrarCliente .span__close {
        color: #000000;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #modalCadastrarCliente .span__close:hover {
            font-size: 25px;
            color: #918a8a;
        }

    .body {
        margin-top: -27px;
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 12px;
        padding: 0px;
        justify-content: space-around;
    }

    #modalCadastrarCliente .txtcodigo {
        border: 1px solid #ced4da;
        border-radius: 4px;
        font-size: 16px;
        font-family: arial, sans-serif;
        padding: 8px 12px;
        width: 88%;
    }

    #modalCadastrarCliente .txtcliente {
        border: 1px solid #ced4da;
        border-radius: 4px;
        font-size: 16px;
        font-family: arial, sans-serif;
        padding: 8px 12px;
        width: 201px;
    }

    #modalCadastrarCliente .txttempo {
        border: 1px solid #ced4da;
        border-radius: 4px;
        font-size: 16px;
        font-family: arial, sans-serif;
        padding: 8px 12px;
        width: 88%;
    }

    #modalCadastrarCliente .txtuf {
        border: 1px solid #ced4da;
        border-radius: 4px;
        font-size: 16px;
        font-family: arial, sans-serif;
        padding: 8px 12px;
        width: 85%;
    }

    #modalCadastrarCliente .txtcidade {
        border: 1px solid #ced4da;
        border-radius: 4px;
        font-size: 16px;
        font-family: arial, sans-serif;
        padding: 8px 7px;
        width: 93%;
    }

    #modalCadastrarCliente .btn_salvar {
        color: #28a745;
        border: 1px solid #28a745;
        width: 100%;
        margin-top: 12px;
    }

        #modalCadastrarCliente .btn_salvar:hover {
            background-color: #28a745;
            color: white;
        }
</style>

<div id="modalCodigoCliente" class="css__CodigoCliente">
    <div class="modal__content-CodigoCliente">
        <h1 class="h1__titulo-modal">Código de Cliente</h1>
        <asp:Button ID="btnNovo" Text="Novo" runat="server" CssClass="btn-novo" OnClick="btnNovo_Click" />
        <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalCodigo();">x</span>

        <div class="grid-container-codigo">
            <asp:GridView runat="server" ID="gvCodigoCliente" AutoGenerateColumns="false" GridLines="None" CssClass="grid-table-Codigo" UseAccessibleHeader="true" OnRowCommand="gvCodigoCliente_RowCommand">
                <Columns>
                    <asp:BoundField DataField="CodigoID" HeaderText="CodigoID" />
                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                    <asp:BoundField DataField="ClienteTransportadora" HeaderText="Cliente Transportadora" />
                    <asp:BoundField DataField="TempoPadrao" HeaderText="Tempo Padrão" />
                    <asp:BoundField DataField="UF" HeaderText="UF" />
                    <asp:BoundField DataField="Cidade" HeaderText="Cidade" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" CssClass="css_abir-dados-modal" CommandName="Editar"
                                CommandArgument='<%# Eval("CodigoID")%>' Text="📝" />
                            <asp:Button ID="btnExcluir"  runat="server" CssClass="css_abir-dados-modal" CommandName="Excluir" 
                                CommandArgument='<%# Eval("CodigoID")%>' Text="🗑" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
<!-- Abertura do modal para cadastrar os Clientes -->
<div id="modalCadastrarCliente" class="css__modal-CadastrarCliente">
    <div class="modal__content-CadastrarCliente">
        <h1 class="h1__titulo-modal">Cliente</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalCadastrarCliente();">x</span>
        <div class="body">
            <div>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="txtcodigo" placeholder="Código" TextMode="Number"  />
            </div>
            <div>
                <asp:TextBox ID="txtCliente" runat="server" CssClass="txtcliente" placeholder="Cliente Transportadora" onkeyup="this.value = this.value.toUpperCase();" />
            </div>
            <div>
                <asp:TextBox ID="txtTempo" runat="server" CssClass="txttempo" placeholder="Tempo Padão" onkeyup="formatarHora(this)" MaxLength="5" />
            </div>
            <div>
                <asp:TextBox ID="txtUF" runat="server" CssClass="txtuf" placeholder="UF" onkeyup="this.value = this.value.toUpperCase();" />
            </div>
            <div>
                <asp:TextBox ID="txtCidade" runat="server" CssClass="txtcidade" placeholder="Cidade" onkeyup="this.value = this.value.toUpperCase();" />
            </div>

        </div>
        <asp:Button ID="btnsalvar" Text="Cadastrar" runat="server" CssClass="btn_salvar" OnClick="btnsalvar_Click" />
    </div>
</div>

<script>
    function abrirModalCadastrarCliente() {
        document.getElementById("modalCadastrarCliente").style.display = "block";
    }

    function fecharModalCadastrarCliente() {
        document.getElementById("modalCadastrarCliente").style.display = "none";
    }

    function abrirModalCliente() {
        document.getElementById("modalCodigoCliente").style.display = "block";
    }

    function fecharModalCliente() {
        document.getElementById("modalCodigoCliente").style.display = "none";
    }


    
    function formatarHora(campo) {
        let valor = campo.value.replace(/\D/g, ''); // Remove tudo que não for número

        if (valor.length > 4) {
            valor = valor.substring(0, 4); // Limita a 4 dígitos
        }

        if (valor.length >= 3) {
            let horas = valor.slice(0, valor.length - 2);
            let minutos = valor.slice(-2);
            valor = horas + ':' + minutos;
        }

        campo.value = valor;
    }


</script>


