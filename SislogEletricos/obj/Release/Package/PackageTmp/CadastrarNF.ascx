<%@ control Language="vb" AutoEventWireup="false" CodeBehind="CadastrarNF.ascx.vb" Inherits="SislogEletricos.CadastrarNF" %>

 <style>
    body {
        margin: 0px;
        padding: 0px;
    }

     .css__modal-cadastrar {
        display: none;
        position: fixed;
        z-index: 99999;
        left: 0px;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.3);
        overflow: auto;
    }

     .modal__content-cadastrar {
        background-color: white;
        margin: 16% auto;
        padding: 20px;
        width: 414px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    #modalCadastrar .h1__titulo-modal {
        background-color: #4157b9;
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

    #modalCadastrar .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #modalCadastrar .span__close:hover {
            font-size: 20px;
            color: black;
            background-color: transparent;
        }

    #modalCadastrar .css_versao {
        font-size: 12px;
        font-family: aarial, sans-serif;
    }

    #modalCadastrar .css_cadastrar {
        width: 94%;
        font-size: 16px;
        font-family: Arial, sans-serif;
        margin-left: 6px;
        margin-top: -4px;
        padding: 8px;
        border-radius: 5px;
        border: 1px solid #0c0c0c;
        background-color: #4d4d4d;
        color: #ffffff;
        cursor: pointer;
        margin-bottom: 14px;
    }

        #modalCadastrar .css_cadastrar:hover {
            background-color: #46494d;
            color: white;
        }

    .lblDescricao {
   
    }

    .textbox-DANFE {
        margin-top: 2px;
    border: 1px solid black;
    border-radius: 4px;
    padding: 6px;


    }

    .div-style {
        display: flex;
        flex-direction: column-reverse;
        margin: -15px 0px 15px 0px;
    }


</style>

<div id="modalCadastrar" class="css__modal-cadastrar">
    <div class="modal__content-cadastrar">
        <h1 class="h1__titulo-modal">Cadastrar NF</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fehcarModalCadastrar();">x</span>
        <div id="div" class="div-style">
            <asp:TextBox runat="server" Text="" ID="txtDANFE" CssClass="textbox-DANFE" MaxLength="44" ClientIDMode="Static" TextMode="SingleLine" />
            <asp:Label Text="DANFE" runat="server" ID="danfe" CssClass="lblDescricao" />
        </div>
        <asp:Button ID="CadastrarNF" Text="Cadastrar" runat="server" CssClass="css_cadastrar" OnClick="Btn_close_Click"
            OnClientClick="this.value='Cadastrando...'; this.disabled=true; document.body.style.cursor='wait';" UseSubmitBehavior="false" />
    </div>
</div>

<script type="text/javascript">
        
    (function () {
    const txt = document.getElementById('txtDANFE');
    const btn = document.getElementById('<%= CadastrarNF.ClientID %>');
    let acionado = false;

    function sanitizarNumeros(s) {
      // remove tudo que não é dígito
      return (s || '').replace(/\D+/g, '');
    }

    function tentarAcionar() {
      // Mantém só dígitos no campo
      const purificado = sanitizarNumeros(txt.value);
      if (txt.value !== purificado) {
        txt.value = purificado;
      }

      const len = purificado.length;

      // Limita a 44 dígitos
      if (len > 44) {
        txt.value = purificado.slice(0, 44);
      }

      // Aciona automaticamente ao bater 44
      if (len === 44 && !acionado) {
        acionado = true;     // evita duplo submit
        btn.click();
      } else if (len < 44) {
        acionado = false;    // libera novo acionamento se apagar
      }
    }

    // Eventos cobrem digitação, colagem e entrada por leitor (rápida)
    txt.addEventListener('input', tentarAcionar);
    txt.addEventListener('keyup', tentarAcionar);
    txt.addEventListener('change', tentarAcionar);

    // Se seu leitor enviar Enter no fim, garante o acionamento
    txt.addEventListener('keypress', function (e) {
      if ((e.key === 'Enter' || e.keyCode === 13) && sanitizarNumeros(txt.value).length === 44) {
        btn.click();
      }
    });
  })();
    function abrirModalCadastrar() {
            document.getElementById("modalCadastrar").style.display = "block";
        }
    
     function fehcarModalCadastrar() {
            document.getElementById("modalCadastrar").style.display = "none";
    }

    
function mostrarLoading() {
        document.body.classList.add('loading');
    }

    function removerLoading() {
        document.body.classList.remove('loading');
    }




</script>

