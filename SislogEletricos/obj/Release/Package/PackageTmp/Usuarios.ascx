<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Usuarios.ascx.vb" Inherits="SislogEletricos.Usuarios" %>
<link href="Style/Usuario.css" rel="stylesheet" />
<style>
    .css__modal-Cadastrar {
    display: none;
    position: fixed;
    z-index: 9999;
    left: 0;
    top: -0%;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.4);
}

#modalCadastrar .modal__content-Cadastrar {
    background-color: white;
    margin: 16% auto;
    padding: 20px;
    width: 500px;
    border-radius: 6px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
}

#modalCadastrar .h1__titulo-modal {
    background-color: #ffffff;
    color: black;
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
    border: 1px solid #000000;
}

#modalCadastrar .span__close {
    color: #000000;
    float: right;
    font-size: 20px;
    cursor: pointer;
    z-index: 2;
    position: relative;
    top: -85px;
}

    #modalCadastrar .span__close:hover {
        font-size: 25px;
        color: #918a8a;
    }



.body {
    margin-top: -14px;
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 17px;
    padding: 0px;
}

#modalCadastrar .txtnome {
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 16px;
    font-family: arial, sans-serif;
    padding: 8px 12px;
    width: 124%;
}

#modalCadastrar .txtusuario {
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 16px;
    font-family: arial, sans-serif;
    padding: 8px 12px;
    width: 123px;
    margin-left: 79px;
}

#modalCadastrar .ddlfuncao {
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 16px;
    font-family: arial, sans-serif;
    padding: 8px 12px;
    width: 150px;
    margin-left: 79px;
}

#modalCadastrar .txtemail {
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 16px;
    font-family: arial, sans-serif;
    padding: 8px 12px;
    width: 124%;
}

#modalCadastrar .txtsenha {
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 16px;
    font-family: arial, sans-serif;
    padding: 8px 12px;
    width: 95%;
}

#modalCadastrar .txtconfirmarSenha {
    border: 1px solid #ced4da;
    border-radius: 4px;
    font-size: 16px;
    font-family: arial, sans-serif;
    padding: 8px 0px;
    width: 95%;
    margin-left: 13px;
}

#modalCadastrar .btn_salvar {
    color: #28a745;
    border: 1px solid #28a745;
    width: 100%;
    margin-top: 12px;
}
    #modalCadastrar .btn_salvar:hover {
        background-color: #28a745;
        color: white;
    }
</style>

<div id="modalUsuarios" class="css__modal-Usuarios">
    <div class="modal__content-Usuarios">
        <h1 class="h1__titulo-modal">Usuários</h1>
        <asp:Button Text="Novo" runat="server"  CssClass="btn-novo" OnClick="btnAbrirModal" />
        <span EnableViewState="true"   class="span__close" runat="server"  onclick="fecharModalUsuarios();">x</span>
        <div>
            <asp:GridView runat="server" ID="gvCadastros" AutoGenerateColumns="false" GridLines="None" CssClass="table" OnRowCommand="gvCadastros_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="Usuario" HeaderText="Usuário" />
                    <asp:BoundField DataField="Funcao" HeaderText="Função" />              
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnAbrir" runat="server" CssClass="css_abir-dados-modal" CommandName="Editar"
                                CommandArgument='<%# Eval("Usuario")%>' Text="📝" />                                                
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
<!-- Abertura do modal para cadastrar os usuario -->
<div id="modalCadastrar" class="css__modal-Cadastrar">
    <div class="modal__content-Cadastrar">
        <h1 class="h1__titulo-modal">Usuários</h1>
       <span EnableViewState="true"   class="span__close" runat="server"  onclick="fecharModalCadastrar();">x</span>
        <div class="body">
            <div>
                <asp:TextBox ID="txtNome" runat="server" CssClass="txtnome" placeholder="nome" />
            </div>
            <div>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="txtusuario" placeholder="usuário" />
            </div>
            <div>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtemail" placeholder="email" />
            </div>
            <div>
                <asp:DropDownList ID="ddlFuncao" runat="server" CssClass="ddlfuncao">
                    <asp:ListItem Text="Seleciona" Value=""></asp:ListItem>
                    <asp:ListItem Text="Adiministrador" Value="Adiministrador"></asp:ListItem>
                    <asp:ListItem Text="Liberador" Value="Liberador"></asp:ListItem>                   
                    <asp:ListItem Text="Cadastrar" Value="Cadastrar"></asp:ListItem>
                    <asp:ListItem Text="Portaria" Value="Portaria"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <asp:TextBox ID="txtSenha" runat="server" CssClass="txtsenha" placeholder="senha" TextMode="Password" />
            </div>
            <div>
                <asp:TextBox ID="txtconfirmarSenha" runat="server" CssClass="txtconfirmarSenha" placeholder="confirmar senha" TextMode="Password" />
            </div>
        </div>
        <asp:Button ID="btnsalvar" Text="Salvar" runat="server" CssClass="btn_salvar" OnClick="btnsalvar_Click" />
    </div>
</div>

<script>
    function abrirModalCadastrar() {
        document.getElementById("modalCadastrar").style.display = "block";
    }

    function fecharModalCadastrar() {
        document.getElementById("modalCadastrar").style.display = "none";
    }

    function abrirModalUsuarios() {
        document.getElementById("modalUsuarios").style.display = "block";
    }

    function fecharModalUsuarios() {
        document.getElementById("modalUsuarios").style.display = "none";
    }
    lucide.createIcons(); // Inicializar os ícones do Lucide
</script>
