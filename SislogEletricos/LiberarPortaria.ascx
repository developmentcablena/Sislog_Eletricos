<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LiberarPortaria.ascx.vb" Inherits="SislogEletricos.LiberarPortaria" %>
<link href="Style/LiberarPortaria.css" rel="stylesheet" />
<style>
    .css__modal-Liberacao {
    display: none;
    position: fixed;
    z-index: 9999;
    left: 0;
    top: -0%;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.4);
}

.modal__content-Liberacao {
    position: relative;
    top: 23%;
    background-color: white;
    margin: 6% auto;
    padding: 20px;
    width: 265px;
    border-radius: 6px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
}

.h1__titulo-modal-liberacao {
    background-color: #464547;
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

#modalLiberacao .span__close {
    color: white;
    float: right;
    font-size: 20px;
    cursor: pointer;
    z-index: 2;
}

    #modalLiberacao .span__close:hover {
        font-size: 20px;
        color: black;
        background-color: transparent;
    }



.div__body {
    display: grid;
    gap: 12px;
}

#modalLiberacao .css__btnChegada {
    border: 1px solid #17a2b8;
    border-radius: 6px;
    color: #17a2b8;
    text-align: center;
    cursor: pointer;
    font-size: 16px;
}

    #modalLiberacao .css__btnChegada:hover {
        background-color: #17a2b8;
        color: white;
    }



#modalLiberacao .css__btnEntrada {
    border: 1px solid #28a745;
    border-radius: 6px;
    text-align: center;
    cursor: pointer;
    font-size: 16px;
    color: #28a745;
}

    #modalLiberacao .css__btnEntrada:hover {
        background-color: #28a745;
        color: white;
    }


#modalLiberacao .css__btnSaida {
    border: 1px solid #ffc107;
    border-radius: 6px;
    text-align: center;
    cursor: pointer;
    font-size: 16px;
    color: #ffc107
}

    #modalLiberacao .css__btnSaida:hover {
        background-color: #ffc107;
        color: white;
    }

</style>

<div id="modalLiberar" class="css__modal-liberar">
    <div class="modal__content-Liberar">
        <h1 class="h1__titulo-modal">Liberação</h1>
        <asp:Button CssClass="btnAtualizar" Text="🔄" runat="server" OnClick="Unnamed_Click" />
        <span EnableViewState="true"   class="span__close" runat="server"  onclick="fecharModalLiberar();">x</span>
        <div>
            <div class="rolagem">
                        <div>
            <asp:GridView runat="server" ID="gvCadastros" AutoGenerateColumns="false" GridLines="None" CssClass="table" OnRowCommand="gvCadastros_RowCommand">
                <Columns>
                    <asp:BoundField DataField="CadastroID" HeaderText="Nº Cadastro" />
                    <asp:BoundField DataField="TipoCadastro" headertext="Tipo Cadastro" />
                    <asp:BoundField DataField="FornecedorCliente" HeaderText="Fornecedor/Cliente" />
                    <asp:BoundField DataField="Transportadora" HeaderText="Transportadora" />
                    <asp:BoundField DataField="Placa" HeaderText="Placa" />
                    <asp:BoundField DataField="HorarioChegada" HeaderText="Chegada"/>
                    <asp:BoundField DataField="HorarioEntrada" HeaderText="Entrada" />
                    <asp:BoundField DataField="HorarioSaida" HeaderText="Sáida" />
                   
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnAbrir" runat="server" CssClass="css_abir-dados-modal" CommandName="Horarios"
                                CommandArgument='<%# Eval("CadastroID")%>' Text="📂" />                             
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView> 
            </div>
                </div>
        </div>
    </div>
</div>

<!-- Exibição da tela de modal para registrar os horarios! -->
<div id="modalLiberacao" class="css__modal-Liberacao">
    <div class="modal__content-Liberacao">
        <h1 class="h1__titulo-modal-liberacao">Liberar</h1>
        <span EnableViewState="true"   class="span__close" runat="server"  onclick="fecharModalLiberacao();">x</span>
        <div class="div__body">
            <asp:Button ID="chegadaID" Text="Chegada" runat="server"  CssClass="css__btnChegada" OnClick="btnChegada"/>
            <asp:Button ID="entradaID" Text="Entrada" runat="server"  CssClass="css__btnEntrada" OnClick="btnEntrada"/>
            <asp:Button  ID="saidaID" Text="Saída" runat="server"  CssClass="css__btnSaida" OnClick="btnSaida"/>
        </div>
    </div>
</div>
<script>
    /* Abrir modal de registro de horarios*/
    function abrirModalLiberacao() {
        document.getElementById("modalLiberacao").style.display = "block";
    }

    function fecharModalLiberacao() {
        document.getElementById("modalLiberacao").style.display = "none";
    }

   

   
    function abrirModalLiberar() {
        document.getElementById("modalLiberar").style.display = "block";
    }

    function fecharModalLiberar() {
        document.getElementById("modalLiberar").style.display = "none";
    }
    lucide.createIcons(); // Inicializar os ícones do Lucide
</script>
