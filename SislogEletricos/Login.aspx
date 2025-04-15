<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="SislogEletricos.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Style/Login.css" rel="stylesheet" type="text/css" />
    <title>Login</title>
</head>
<body>
    <div class="container">
        <form class="login-form" runat="server">
            <h1>Login</h1>
            <asp:Label ID="Label1" runat="server" Text="Usuário:"></asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server" placeholder="Digite seu usuário"></asp:TextBox>
            <asp:Label ID="label2" runat="server" Text="Senha:"></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" placeholder="Digite sua senha" TextMode="Password"></asp:TextBox>
            <asp:Button CssClass="btn" runat="server" Text="Entrar" OnClick="Unnamed_Click" />
        </form>
    </div>
</body>
</html>
