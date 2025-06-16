<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="~/Sair.ascx.vb" Inherits="SislogEletricos.Sair" %>

<style>
    body {
        margin: 0px;
        padding: 0px;
    }

    .css__modal-sair {
        display: none;
        position: fixed;
        z-index: 99999;
        left: 0px;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.1);
        overflow: auto;
    }

    .modal__content-sair {
        background-color: white;
        margin: 16% auto;
        padding: 20px;
        width: 279px;
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
        font-size: 19px;
        font-weight: bold;
        border-radius: 6px 6px 0 0;
        position: relative;
        top: -40px;
        left: -20px;
        font-family: sans-serif;
    }

    #modalSair .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #modalSair .span__close:hover {
            font-size: 20px;
            color: black;
            background-color: transparent;
        }

    #modalSair .css_versao {
        font-size: 12px;
        font-family: aarial, sans-serif;
    }

    #modalSair .css_sair {
        width: 94%;
        font-size: 16px;
        font-family: Arial, sans-serif;
        margin-left: 6px;
        margin-top: -4px;
        padding: 8px;
        border-radius: 5px;
        border: 1px solid #dc3545;
        background-color: white;
        color: #dc3545;
        cursor: pointer;
        margin-bottom: 14px;
    }

        #modalSair .css_sair:hover {
            background-color: #dc3545;
            color: white
        }
</style>


<div id="modalSair" class="css__modal-sair">
    <div class="modal__content-sair">
        <h1 class="h1__titulo-modal">Olá, <%= Session("Usuario") %>!</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fehcarModalSair();">x</span>
        <asp:Button ID="btn_close" Text="Sair do sistema" runat="server" CssClass="css_sair" OnClick="Btn_close_Click" />
        <asp:Label Text="Versão 1.5.1.3" runat="server" CssClass="css_versao" />
    </div>
</div>


<script>
     function fehcarModalSair() {
            document.getElementById("modalSair").style.display = "none";
        }
</script>
