# Api de sistema de vendas e entregas 

Api em rest feito em .NET 5 contendo dois endpoints( Auth e Pedidos).

## 🚀 Começando

Realizar o git clone do repositório. 

### 📋 Pré-requisitos

Instalação do .NET 5. (https://dotnet.microsoft.com/download/dotnet/5.0)

Necessário instalar o Visual Studio Commmunity ou Vs Code.

### 🔧 Instalação

Abrir o arquivo : `EcommDashApi.sln`.

Como há apenas um único projeto, o  sln já está configurado para rodá-lo.

Debugar com IISExpress.

Ao rodar, o swagger já mostrará os dois endpoints.

![image](https://user-images.githubusercontent.com/1672132/137138802-6499e1f7-66d4-4dd2-b950-b90845ab7f3c.png)

Realizar o post no endpoint de Authentication com o usuário : Ecommerce e senha : Ecommerce.

![image](https://user-images.githubusercontent.com/1672132/137138962-55af335f-fb22-48c8-b93d-a0b5ade5c4a8.png)

E após será gerado um token de acesso.

Com o token gerado é possível inputar na head de requisição `Authorization: Bearer {token}` no endpoint de Encomendas.





