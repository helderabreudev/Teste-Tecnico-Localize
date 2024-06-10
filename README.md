# Teste-Tecnico-Localize

## Passos para Configuração e Utilização da API

1. **Trocar a connection string no `app.settings`**

2. **Rodar a Migration com `UPDATE-DATABASE`**
   - Execute o comando `dotnet ef database update` para aplicar as migrações.

3. **Utilizar o endpoint `auth` para obter o token JWT**
   - Realize uma requisição POST para o endpoint de autenticação com o usuário `teste` e a senha `teste`.

4. **Passar o token JWT no header `Authorization` ou na seção `Authorize` do Swagger**
   - Inclua o token JWT no header `Authorization` com o prefixo `Bearer`.
   - Ou utilize a seção `Authorize` do Swagger para passar o token.