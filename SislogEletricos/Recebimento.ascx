<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Recebimento.ascx.vb" Inherits="SislogEletricos.Recebimento" %>
<link href="Style/Recebimento.css" rel="stylesheet" type="text/css" />
<script src="https://unpkg.com/lucide@latest/dist/umd/lucide.min.js"></script>

<div id="meuModal" class="modal">
    <div class="modal-content">
        <h1 class="h1__titulo-modal">Recebimento</h1>
        <span EnableViewState="true"   class="span__close" runat="server"  onclick="fecharModal();">x</span>
        <div class="modal__body"> 
            <div class="div__group">
                <asp:Label ID="Label1" runat="server" Text="Nota fiscal"></asp:Label>
                <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="textBox" placeholder="Nota fiscal" TextMode="Number" MaxLength="15"  ></asp:TextBox>
            </div>
            <div class="div__group">
                <asp:Label ID="Label2" runat="server" Text="Fornecedor" ></asp:Label>
                <asp:TextBox ID="txtFornecedor" runat="server" CssClass="textBox" placeholder="Fornecedor"  MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
            </div>
            <div class="div__group">
                <asp:Label ID="Label3" runat="server" Text="Cidade" ></asp:Label>
                <asp:TextBox ID="txtCidade" runat="server" CssClass="textBox" placeholder="Cidade" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
            </div>
            <div class="div__group">
                <asp:Label runat="server" Text="UF"></asp:Label>
                <asp:TextBox ID="txtUF" runat="server" CssClass="textBox" placeholder="UF"  MaxLength="2" onkeyup="this.value = this.value.toUpperCase();" ></asp:TextBox>
            </div>
            <div class="div__group">
                <asp:Label runat="server" text="Transportadora"></asp:Label>
                <asp:TextBox  ID="txtTransportadora" runat="server" CssClass="textBox" placeholder="Transportadora" MaxLength="100" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
            </div>
            <div class="div__group">
                <asp:Label  runat="server" Text="CIF / FOB"></asp:Label>
                <asp:DropDownList id="ddlFrete" runat="server" CssClass="textBox">
                     <asp:ListItem Text="Selecione" Value="" />
                     <asp:ListItem Text="CIF"  />
                     <asp:ListItem Text="FOB" />
                </asp:DropDownList>
            </div>
            <div class="div__group">
                <asp:Label runat="server" Text="Motorista"></asp:Label>
                <asp:TextBox runat="server" id="txtMotorista" CssClass="textBox" placeholder="Motorista" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();" />
            </div>
            <div class="div__group">
                <asp:Label Text="RG/CPF" runat="server" />
                <asp:TextBox runat="server" ID="txtRG" CssClass="textBox" placeholder="RG/CPF" TextMode="Number" MaxLength="11"/>
            </div>
            <div class="div__group">
                <asp:Label Text="Placa" runat="server" />
                <asp:TextBox runat="server" ID="txtPlaca" CssClass="textBox" placeholder="Placa" MaxLength="17" onkeyup="this.value = this.value.toUpperCase();" />
            </div>
            <div class="div__group">
                <asp:Label Text="Material" runat="server" />
                <asp:TextBox runat="server"  ID="txtMaterial" CssClass="textBox" placeholder="Material" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"/>
            </div>
            <div class="div__group">
                <asp:Label Text="Quantidades" runat="server" />
                <asp:TextBox runat="server" ID="txtVolumes" CssClass="textBox" placeholder="Quantidades" TextMode="Number" />
            </div>
            <div class="div__group">
                <asp:Label Text="Data cadastro" runat="server" />
                <asp:TextBox ID="txtData" runat="server"  TextMode="DateTimeLocal" CssClass="textBox"/>  
            </div>
            <div class="div__group">
                <asp:Label Text="Data Recebimento" runat="server" />
                <asp:TextBox ID="dataJanela" runat="server"  TextMode="DateTimeLocal" CssClass="textBox"/>  
            </div>
            <br />
            <div>
                <asp:Label Text="Observação" runat="server" />
                <asp:TextBox runat="server" ID="txtObservacao"  CssClass="textBox__obs" TextMode="MultiLine" placeholder="Observação" />
            </div>
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrarteste" CssClass="btn-cadastrar" OnClick="btnCadastrar_Click"  OnClientClick="this.value='Processando...'; this.disabled=true; document.body.style.cursor='wait';" UseSubmitBehavior="false"  />
        </div>
    </div>
</div>

<script>
    function abrirModal() {
        document.getElementById("meuModal").style.display = "block";
    }

    function fecharModal() {
        document.getElementById("meuModal").style.display = "none";
    }
    lucide.createIcons(); // Inicializar os ícones do Lucide

</script>
