# **MinimalApi com Autenticação JWT e Gerenciamento de Recursos**

## **Descrição**
Este projeto é uma **Minimal API** desenvolvida com **.NET Core** que implementa autenticação baseada em **JWT (JSON Web Token)** e permite o gerenciamento de recursos como **administradores** e **veículos**. Ele utiliza práticas modernas de desenvolvimento backend, incluindo autorização baseada em perfis, validações personalizadas e integração com banco de dados relacional.

A API foi projetada para atender sistemas que exigem segurança, escalabilidade e extensibilidade. Ideal para aplicações que precisam de APIs RESTful para operações de CRUD com autenticação segura.

---

## **Funcionalidades**
1. **Autenticação e Autorização**:
   - JWT Token para autenticação.
   - Suporte a perfis de usuários (ex.: `Adm`, `Editor`).
   - Controle de acesso baseado em roles.

2. **Gerenciamento de Administradores**:
   - Login e geração de token JWT.
   - Listagem, busca, criação e exclusão de administradores.
   - Endpoints protegidos por autorização (somente usuários com perfil "Adm" têm acesso).

3. **Gerenciamento de Veículos**:
   - Endpoints para cadastro, edição, remoção e listagem de veículos.
   - Validação de dados (ex.: ano do veículo não pode ser anterior a 1950).
   - Controle de acesso baseado em roles (ex.: apenas "Adm" pode deletar veículos).

4. **Swagger UI**:
   - Documentação interativa gerada automaticamente para testar os endpoints.
   - Integração com autenticação JWT para testar endpoints protegidos.

5. **Validações de Dados**:
   - Respostas detalhadas para erros de validação de campos obrigatórios.

6. **Conexão com Banco de Dados**:
   - Integração com MySQL usando **Entity Framework Core**.
   - Suporte para migrações de banco de dados.

---

## **Tecnologias Utilizadas**
- **.NET Core** (Minimal API)
- **C#**
- **Entity Framework Core** (ORM)
- **Autenticação JWT**
- **Swagger** para documentação interativa
- **MySQL** como banco de dados relacional
- **CORS** para controle de acesso
- **Dependency Injection** para desacoplamento de serviços

---

## **Como Rodar o Projeto Localmente**
### **Pré-requisitos**
1. **.NET 7 SDK** ou superior.
2. Banco de dados **MySQL** configurado.
3. Ferramentas como **Visual Studio** ou **Visual Studio Code**.

### **Passos**
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   ```
2. Configure o arquivo `appsettings.json` com as informações de conexão do banco de dados e a chave JWT:
   ```json
   {
       "ConnectionStrings": {
           "MySql": "Server=localhost;Database=MinimalApiDb;User=root;Password=sua_senha"
       },
       "Jwt": {
           "Key": "sua-chave-secreta"
       }
   }
   ```

3. Aplique as migrações para o banco de dados:
   ```bash
   dotnet ef database update
   ```

4. Rode a aplicação:
   ```bash
   dotnet run
   ```

5. Acesse o Swagger na URL:
   ```
   http://localhost:5000
   ```

---

## **Exemplo de Requisição**
### **Endpoint: Login de Administrador**
**POST** `/administradores/login`

**Body:**
```json
{
    "email": "admin@exemplo.com",
    "senha": "senha123"
}
```

**Resposta (200 OK):**
```json
{
    "email": "admin@exemplo.com",
    "perfil": "Adm",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

## **Contribuições**
- Sinta-se à vontade para abrir **issues** ou enviar **pull requests** com melhorias, correções ou sugestões!
