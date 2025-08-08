# 📌 CHANGELOG

Todas as alterações importantes deste sistema serão documentadas neste arquivo.

📄 Sistema Cablena Unificado
Versão 1.7.0.0 – [08/08/2025]
- Integração dos sistemas Telecom e Elétricos em uma única aplicação.
- Implementação da lógica de seleção de empresa com base em checkbox ou dropdown.
- Configuração dinâmica de conexão com banco de dados conforme empresa selecionada.
- Ajustes nas queries para suportar múltiplos bancos.
- Correções de layout e melhorias na interface.


📋 [1.6.0.0] - 24/06/2025
### 🚀 Funcionalidades Novas
Adicionado:
 - Adicionada nova guia no sistema para cadastro e edição de clientes.
   
---

📋 [1.5.1.3] - 16/06/2025 
Adicionado:
 - Novo botão no sistema que exibe o nome do usuário logado e a versão atual do sistema.
 - Ao clicar nesse botão, o usuário pode também sair do sistema rapidamente.

Implementado:
  - Nova aba de Relatório adicionada ao sistema. (Relatório Tempo de Permanencia)

Melhorias:
  - Na aba Cadastro Embarque, foi adicionado um campo para digitar o código do cliente. Ao inserir o código:
  - O sistema automaticamente exibe o nome do cliente, tempo padrão, cidade e UF.
  - Essa mesma melhoria foi replicada nas abas:
      - Recusado
      - Histórico
      - Autorizado
   - Correção para quando o usuário colocar o codigo do cliente = 0 deixa o campo Cliente, UF e Cidade liberado.
   - Adicionado a observação no relatório.
   - Adicionado um relatório para "RECEBIMENTO".
   - Trava no botão ao tentar exportar o relatório para excel sem gerar.
---

## [1.3.0.0] - 20/05/2025
### 🚀 Funcionalidades Novas
- Criada a nova funcionalidade **"Recusados"**, com uma tela específica para usuários com permissão de cadastro.
- Nova tela para informar o **motivo da recusação** de um cadastro.
- Na aba **Autorizar**, agora é possível **rejeitar** ou **excluir** um cadastro, permitindo o retorno para correção.

### 🛠️ Melhorias
- Aba **Usuários**: adicionada **barra de rolagem** para evitar distorção visual.
- Tela **Histórico de Cadastro**:
  - Adicionada **barra de rolagem**.
  - Mostra agora **data e hora** do cadastro.
  - Registros exibidos do **mais recente para o mais antigo**.

---

## [1.1.0.0] - 29/04/2025
### 🛠️ Melhorias
- Campos `TextBox` das telas **Recebimento** e **Embarque** agora forçam **letras maiúsculas (CAPS LOCK)**.
- Adicionada **trava de duplicidade** ao cadastrar novos registros.
- Sistema agora registra o **nome do usuário** que realizou o cadastro (log).

---

## [1.0.0.1] - 22/04/2025
### 🐞 Correções
- Corrigido problema na **aba Embarque** que impedia a exibição dos dados corretamente.

---

## [1.0.0.0] - 15/04/2025
### 🚀 Lançamento
- Primeira versão oficial do sistema.
