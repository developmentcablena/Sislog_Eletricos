<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Manual.aspx.vb" Inherits="SislogEletricos.Manual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Manual</title>
    <style>
        .link {
            display:flex;
            flex-direction:column;
            grid-gap:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Documentação do Sistema</h1>
            <div class="link">
                <a href="Manual/SISTEMA DE CONTROLE DE CAMINHÕES_Portaria.pdf" class="link">Liberação da Portaria</a>            
                <a href="Manual/SISTEMA DE CONTROLE DE CAMINHÕES_Logistica.pdf" class="link">Cadastro de Solicitação - Logistica</a>
                <a href="Principal.aspx">Voltar</a>
            </div>
        </div>
    </form>
</body>
</html>
